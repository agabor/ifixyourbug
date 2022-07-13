using System.Text.Json;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace IFYB.Controllers;

[ApiController]
[Route("api/pay")]
public class PaymentController : BaseController
{
    private readonly IConfiguration config;

    public PaymentController(ApplicationDbContext dbContext, IConfiguration config) : base(dbContext)
    {
        this.config = config;
    }

    [HttpGet]
    [Route("{paymentToken}")]
    [Produces(typeof(OrderDto))]
    public IActionResult CheckPaymentToken(string paymentToken)
    {
      var order = dbContext.Orders.FirstOrDefault(o => o.PaymentToken == paymentToken);
      if (order == null)
          return NotFound();
      return Ok(order.ToDto());
    }

    [HttpPost]
    [Route("{paymentToken}")]
    [Produces(typeof(UrlDto))]
    public IActionResult Pay(string paymentToken)
    {
      var order = dbContext.Orders.FirstOrDefault(o => o.PaymentToken == paymentToken);
      if (order == null)
          return NotFound();
      if (order.State != OrderState.Accepted)
        return BadRequest();
      dbContext.Entry(order).Reference(o => o.Client).Load();
      var domain = config["BaseUrl"];
      var options = new SessionCreateOptions
      {
          CustomerEmail = order.Client!.Email,
          LineItems = new List<SessionLineItemOptions>
          {
            new SessionLineItemOptions
            {
              Price = config["StripePriceId"],
              Quantity = 1,
            },
          },
          Mode = "payment",
          BillingAddressCollection = "auto",
          TaxIdCollection = new SessionTaxIdCollectionOptions {
              Enabled = true
          },
          SuccessUrl = $"{domain}/checkout-success/{paymentToken}",
          CancelUrl = $"{domain}/checkout-failed/{paymentToken}",
      };
      var service = new SessionService();
      Session session = service.Create(options);
      order.StripeId = session.Id;
      dbContext.SaveChanges();
      
      return Ok(new UrlDto(session.Url));
    }

    [HttpPost]
    [Route("hook")]
    public async Task<IActionResult> OnPayment()
    {
      var jsonString = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
      try
      {
          var stripeEvent = EventUtility.ConstructEvent(jsonString,
              Request.Headers["Stripe-Signature"], config["StripeHookKey"]);

          if (stripeEvent.Type == Events.CheckoutSessionCompleted)
          {
              var sessionObject = stripeEvent.Data.Object as Stripe.Checkout.Session;
              if (sessionObject == null)
                return BadRequest();
              var order = dbContext.Orders.FirstOrDefault(p => p.StripeId == sessionObject.Id);
              if (order == null)
                return NotFound();
              var customerDetails = sessionObject.CustomerDetails;
              var address = customerDetails.Address;
              order.City = address.City;
              order.Country = address.Country;
              order.Line1 = address.Line1;
              order.Line2 = address.Line2;
              order.PostalCode = address.PostalCode;
              order.AddressState = address.State;
              order.CustomerName = customerDetails.Name;
              var taxIds = customerDetails.TaxIds;
              if (taxIds.Any()) {
                order.TaxIdType = taxIds.First().Type;
                order.TaxId = taxIds.First().Value;
              }
              order.State = OrderState.Payed;
              order.PaymentToken = null;
              dbContext.SaveChanges();
          }

          return Ok();
      }
      catch (StripeException)
      {
          return BadRequest();
      }
    }
}
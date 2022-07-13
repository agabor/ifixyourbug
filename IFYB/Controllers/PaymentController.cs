using System.Text.Json;
using IFYB.Entities;
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
    public IActionResult Pay(string paymentToken)
    {
      var order = dbContext.Orders.FirstOrDefault(o => o.PaymentToken == paymentToken);
      if (order == null)
          return NotFound();
      if (order.State != OrderState.Accepted)
        return BadRequest();
      dbContext.Entry(order).Reference(o => o.Client).Load();
      var domain = "http://localhost:5000";
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
          SuccessUrl = domain + "/success.html",
          CancelUrl = domain + "/cancel.html",
      };
      var service = new SessionService();
      Session session = service.Create(options);
      order.StripeId = session.Id;
      dbContext.SaveChanges();

      string jsonString = JsonSerializer.Serialize(session);

      Console.WriteLine(jsonString);
      

      Response.Headers.Add("Location", session.Url);
      return new StatusCodeResult(303);
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
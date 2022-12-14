using System.Text.Json;
using System.Threading.Channels;
using IFYB.Entities;
using IFYB.Models;
using IFYB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace IFYB.Controllers;

[ApiController]
[Route("api/pay")]
public class PaymentController : BaseController
{
    private readonly AppOptions appOptions;
    private readonly StripeOptions stripeOptions;
    private readonly Channel<Entities.Order> billingChannel;
    private readonly ErrorHandlerService errorHandlerService;

    public PaymentController(ApplicationDbContext dbContext, IOptions<AppOptions> appOptions, IOptions<StripeOptions> stripeOptions, Channel<Entities.Order> billingChannel, ErrorHandlerService errorHandlerService) : base(dbContext)
    {
        this.appOptions = appOptions.Value;
        this.stripeOptions = stripeOptions.Value;
        this.billingChannel = billingChannel;
        this.errorHandlerService = errorHandlerService;
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
      
      var options = new SessionCreateOptions
      {
          CustomerEmail = order.Client!.Email,
          Expand = new List<string> {"customer", "customer.tax"},
          AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
          LineItems = new List<SessionLineItemOptions>
          {
            new SessionLineItemOptions
            {
              Price = order.PriceId,
              Quantity = 1,
            },
          },
          Mode = "payment",
          BillingAddressCollection = "required",
          TaxIdCollection = new SessionTaxIdCollectionOptions {
              Enabled = true
          },
          SuccessUrl = $"{appOptions.BaseUrl}/checkout-success/{paymentToken}",
          CancelUrl = $"{appOptions.BaseUrl}/checkout/{paymentToken}",
      };
      var service = new SessionService();
      Session session = service.Create(options);
      order.StripeSessionId = session.Id;
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
              Request.Headers["Stripe-Signature"], stripeOptions.HookKey);

          if (stripeEvent.Type == Events.CheckoutSessionCompleted)
          {
              var sessionObject = stripeEvent.Data.Object as Stripe.Checkout.Session;
              if (sessionObject == null)
                return BadRequest();
              var order = dbContext.Orders.FirstOrDefault(p => p.StripeSessionId == sessionObject.Id);
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
              order.Currency = sessionObject.Currency;
              order.State = OrderState.Payed;
              order.PaymentToken = null;
              order.AmountTotal = sessionObject.AmountTotal;
              order.AmountSubtotal = sessionObject.AmountSubtotal;
              order.AmountTax = sessionObject.TotalDetails.AmountTax;
              order.PayedAt = DateTime.UtcNow;
              dbContext.Entry(order).Reference(o => o.Client).Load();
              order.StripeCustomerId = sessionObject.CustomerId;
              dbContext.SaveChanges();


              billingChannel.Writer.TryWrite(order);
          }

          return Ok();
      }
      catch (StripeException e)
      {
          errorHandlerService.OnException(e, jsonString);
          return BadRequest();
      }
    }
}
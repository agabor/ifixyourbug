using System.Text.Json;
using IFYB.Entities;
using Microsoft.AspNetCore.Mvc;
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

      Console.WriteLine(jsonString);
      var json = JsonDocument.Parse(jsonString).RootElement;
      if (json.GetProperty("type").GetString() == "checkout.session.completed")
      {
        var sessionObject = json.GetProperty("data").GetProperty("object");
        var id = sessionObject.GetProperty("id").GetString();
        var order = dbContext.Orders.FirstOrDefault(p => p.StripeId == id);
        if (order == null)
          return NotFound();
        var customerDetails = sessionObject.GetProperty("customer_details");
        var address = customerDetails.GetProperty("address");
        order.City = address.GetProperty("city").GetString();
        order.Country = address.GetProperty("country").GetString();
        order.Line1 = address.GetProperty("line1").GetString();
        order.Line2 = address.GetProperty("line2").GetString();
        order.PostalCode = address.GetProperty("postal_code").GetString();
        order.AddressState = address.GetProperty("state").GetString();
        order.CustomerName = customerDetails.GetProperty("name").GetString();
        var taxIds = customerDetails.GetProperty("tax_ids");
        if (taxIds.GetArrayLength() > 0) {
          order.TaxIdType = taxIds[0].GetProperty("type").GetString();
          order.TaxId = taxIds[0].GetProperty("value").GetString();
        }
        var paymentMethodTypes = customerDetails.GetProperty("payment_method_types");
        if (paymentMethodTypes.GetArrayLength() > 0) {
          order.PaymentMethod = paymentMethodTypes[0].GetString();
        }
        order.State = OrderState.Payed;
        dbContext.SaveChanges();
      }

      return Ok();
    }
}
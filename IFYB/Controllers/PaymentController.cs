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
            dbContext.Entry(order).Reference(o => o.Client).Load();
            var domain = "http://localhost:5000";
            var options = new SessionCreateOptions
            {
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

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
    }
}
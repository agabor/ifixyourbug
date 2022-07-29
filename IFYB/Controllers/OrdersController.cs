using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;
using Scriban;
using IFYB.Services;
using Microsoft.Extensions.Options;

namespace IFYB.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Policy = Policies.ClientOnly)]
public class OrdersController : BaseController
{
    public EmailService EmailService { get; }
    private readonly StripeOptions stripeOptions;
    private readonly OfferDto offer;
    public OrdersController(ApplicationDbContext dbContext, EmailService emailService, IOptions<StripeOptions> stripeOptions, OfferDto offer) : base(dbContext)
    {
        EmailService = emailService;
        this.stripeOptions = stripeOptions.Value;
        this.offer = offer;
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<OrderDto>))]
    public IActionResult ListOrders() {
        var client = GetClient();
        if (client == null)
            return Forbid();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        return base.Ok(client.Orders!.Select(o => o.ToDto()).ToList());
    }

    [HttpGet]
    [Produces(typeof(OrderDto))]
    [Route("{orderId}")]
    public IActionResult GetOrder(int orderId) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = client.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        return base.Ok(order.ToDto());
    }

    [HttpGet]
    [Produces(typeof(OrderDto))]
    [Route("by-number/{number}")]
    public IActionResult GetOrderByNumber(string number) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = client.Orders!.FirstOrDefault(o => o.Number == $"#{number}");
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        return base.Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(MessageDto))]
    [Route("{orderId}")]
    public IActionResult AddMessage([FromBody] Message message, int orderId) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = client.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        message.ClientId = client.Id;
        message.OrderId = order.Id;
        message.DateTime = DateTime.UtcNow;
        message.FromClient = true;
        order.Messages!.Add(message);
        dbContext.SaveChanges();
        return Ok(message.ToDto());
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult AddOrder([FromBody] OrderDto dto) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = Order.FromDto(dto);
        order.Id = 0;
        order.ClientId = client.Id;
        string actualDate = DateTime.Now.ToString("yyMMdd");
        if(client.Orders.Count > 0) {
            var lastElement = client.Orders.Last();
            string lastNumberDate = lastElement.Number.Substring(1, 6);
            string lastNumber = lastElement.Number.Substring(7, 3);
            if(actualDate == lastNumberDate) {
                order.Number = "#" + actualDate + (int.Parse(lastNumber)+1).ToString("D3");
            } else {
                order.Number = "#" + actualDate + "001";
            }
        } else {
            order.Number = "#" + actualDate + "001";
        }
        order.EurPriceId = stripeOptions.EurPriceId;
        order.UsdPriceId = stripeOptions.UsdPriceId;
        order.EurPrice = offer.EurPrice;
        order.UsdPrice = offer.UsdPrice;
        client.Orders!.Add(order);
        dbContext.SaveChanges();
        EmailService.SendEmail(client.Email, "OrderSubmit", order, new { client.Name });
        return Ok(new IdDto(order.Id));
    }
}

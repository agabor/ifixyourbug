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
    private readonly  EmailDispatchService emailDispatchService;
    private readonly StripeOptions stripeOptions;
    private readonly OfferDto offer;
    public OrdersController(ApplicationDbContext dbContext, EmailDispatchService emailDispatchService, IOptions<StripeOptions> stripeOptions, OfferDto offer) : base(dbContext)
    {
        this.emailDispatchService = emailDispatchService;
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
    [Route("{number}")]
    public IActionResult GetOrderByNumber(int number) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = client.Orders!.Single(o => o.Number == number);
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        return base.Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(MessageDto))]
    [Route("{number}")]
    public IActionResult AddMessage([FromBody] Message message, int number) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = client.Orders!.Single(o => o.Number == number);
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        message.ClientId = client.Id;
        message.OrderId = order.Id;
        message.DateTime = DateTime.UtcNow;
        message.FromClient = true;
        order.Messages!.Add(message);
        dbContext.SaveChanges();
        var admins = dbContext.Admins;
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail(admin.Email, "OrderMessageToAdmin", order, new { client.Name }, true);
        }
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
        var today = (DateTime.UtcNow - DateTime.UnixEpoch).Days;
        var ordersToday = dbContext.Orders.Where(o => o.CreationDay == today).Count();
        order.Number = int.Parse(DateTime.UtcNow.ToString("yyMMdd") + (ordersToday+1).ToString("D3"));
        order.EurPriceId = stripeOptions.EurPriceId;
        order.UsdPriceId = stripeOptions.UsdPriceId;
        order.EurPrice = offer.EurPrice;
        order.UsdPrice = offer.UsdPrice;
        client.Orders!.Add(order);
        emailDispatchService.DispatchEmail(client.Email, "OrderSubmit", order, new { client.Name });
        var admins = dbContext.Admins.ToList();
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail("livia.orsos@codesharp.hu", "OrderSubmitToAdmin", order, new { client.Name }, true);
        }
        dbContext.SaveChanges();
        return Ok(new IdDto(order.Id));
    }

    [HttpPost]
    [Produces(typeof(OrderState))]
    [Route("{number}/update")]
    public IActionResult UpdateOrder([FromBody] OrderDto dto, int number) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = client.Orders!.FirstOrDefault(o => o.Number == number);
        if (order == null)
            return NotFound();
        order.Framework = dto.Framework;
        order.Version = dto.Version;
        order.ApplicationUrl = dto.ApplicationUrl;
        order.SpecificPlatform = dto.SpecificPlatform;
        order.SpecificPlatformVersion = dto.SpecificPlatformVersion;
        order.ThirdPartyTool = dto.ThirdPartyTool;
        order.BugDescription = dto.BugDescription;
        order.State = OrderState.Submitted;
        order.GitAccessId = dto.GitAccessId;
        emailDispatchService.DispatchEmail(client.Email, "OrderUpdate", order, new { client.Name });
        var admins = dbContext.Admins.ToList();
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail("livia.orsos@codesharp.hu", "OrderUpdateToAdmin", order, new { client.Name }, true);
        }
        dbContext.SaveChanges();
        return Ok(order.State);
    }
}

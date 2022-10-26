using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;
using IFYB.Services;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace IFYB.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Policy = Policies.ClientOnly)]
public class OrdersController : BaseController
{
    private readonly EmailDispatchService emailDispatchService;
    private readonly StripeOptions stripeOptions;
    private readonly Settings settings;
    private readonly GitOptions gitOptions;

    public OrdersController(ApplicationDbContext dbContext, EmailDispatchService emailDispatchService, IOptions<StripeOptions> stripeOptions, Settings settings, IOptions<GitOptions> gitOptions) : base(dbContext)
    {
        this.emailDispatchService = emailDispatchService;
        this.stripeOptions = stripeOptions.Value;
        this.settings = settings;
        this.gitOptions = gitOptions.Value;
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
        var order = client.Orders!.FirstOrDefault(o => o.Number == number);
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
        var admins = dbContext.Admins.ToList();
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail(admin.Id, admin.Email, "OrderMessageToAdmin", order, new { client.Name }, true);
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
        order.EurPrice = settings.EurPrice;
        order.UsdPrice = settings.UsdPrice;
        string pattern = "(src=\".?.?/?userimages/)(.*?)\"";
        order.BugDescription = Regex.Replace(order.BugDescription, pattern, m => $"src=\"/userimages/{m.Groups[2].Value}\"");

        client.Orders!.Add(order);
        dbContext.SaveChanges();
        MatchCollection matches = Regex.Matches(order.BugDescription, pattern);
        dbContext.Entry(order).Collection(o => o.Images!).Load();
        foreach (Match match in matches)
        {
            var image = dbContext.Images!.FirstOrDefault(i => i.Location == match.Groups[2].Value);
            if (image == null)
                continue;
            image.OrderId = order.Id;
            order.Images!.Add(image);
        }
        dbContext.SaveChanges();
        var gitAccess = dbContext.GitAccesses.First(a => a.Id == order.GitAccessId);
        string? gitUser = null;
        string? saas = null;
        if (gitAccess.AccessMode == GitAccessMode.Invite) 
        {
            gitUser = "gabor@ifixyourbug.com";
            saas = "None";
            var url = gitAccess.Url.ToLower();
            foreach (var service in gitOptions.Services)
            {
                if (url.Contains(service.Domain))
                {
                    gitUser = service.User;
                    saas = service.Name;
                }
            }
        }
        emailDispatchService.DispatchEmail(client.Id, client.Email, "OrderSubmit", order, new { Name = client.Name, GitUser = gitUser, Saas = saas, AccessMode = (int)gitAccess.AccessMode, SshKey = settings.SshKey });
        var admins = dbContext.Admins.ToList();
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail(admin.Id, admin.Email, "OrderSubmitToAdmin", order, new { client.Name }, true);
        }
        dbContext.SaveChanges();
        return Ok(new IdDto(order.Id));
    }

    [HttpPost]
    [Produces(typeof(OrderDto))]
    [Route("{number}/update")]
    public IActionResult UpdateOrder([FromBody] OrderDto dto, int number) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = client.Orders!.FirstOrDefault(o => o.Number == number);
        if (order == null)
            return NotFound();
        order.ApplicationUrl = dto.ApplicationUrl;
        order.BugDescription = dto.BugDescription;
        order.State = OrderState.Submitted;
        order.GitAccessId = dto.GitAccessId;
        string pattern = "(src=\".?.?/?userimages/)(.*?)\"";
        order.BugDescription = Regex.Replace(order.BugDescription, pattern, m => $"src=\"/userimages/{m.Groups[2].Value}\"");
        MatchCollection matches = Regex.Matches(order.BugDescription, pattern);
        dbContext.Entry(order).Collection(o => o.Images!).Load();
        foreach (Match match in matches)
        {
            var image = dbContext.Images!.FirstOrDefault(i => i.Location == match.Groups[2].Value);
            if (image == null)
                continue;
            image.OrderId = order.Id;
            order.Images!.Add(image);
        }
        emailDispatchService.DispatchEmail(client.Id, client.Email, "OrderUpdate", order, new { client.Name });
        var admins = dbContext.Admins.ToList();
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail(admin.Id, admin.Email, "OrderUpdateToAdmin", order, new { client.Name }, true);
        }
        dbContext.SaveChanges();
        return base.Ok(order.ToDto());
    }
}

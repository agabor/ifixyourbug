using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Scriban;
using IFYB.Services;
using Microsoft.Extensions.Options;

namespace IFYB.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = Policies.AdminOnly)]
public class AdminController : ControllerBase
{
    private readonly AppOptions appOptions;

    private ApplicationDbContext dbContext { get; }
    public EmailService EmailService { get; }

    public AdminController(ApplicationDbContext dbContext, EmailService emailService, IOptions<AppOptions> appOptions) 
    {
        this.dbContext = dbContext;
        this.EmailService = emailService;
        this.appOptions = appOptions.Value;
    }
    protected Client? GetClientById(int id)
    {
        return dbContext.Clients.Where(u => u.Id == id).FirstOrDefault();
    }

    [HttpGet]
    [Route("contact-messages")]
    [Produces(typeof(ContactMessageDto))]
    public IActionResult GetContactMessages()
    {
        return Ok(dbContext.Messages.Include(m => m.Client).Where(m => m.OrderId == null).Select(m => new ContactMessageDto(m.Client!.Name!, m.Client.Email, m.Text, m.DateTime)));
    }

    [HttpGet]
    [Route("contact-messages/{clientId}")]
    [Produces(typeof(ContactMessageDto))]
    public IActionResult GetClientContactMessages(int clientId)
    {
        return Ok(dbContext.Messages.Include(m => m.Client).Where(m => m.OrderId == null && m.ClientId == clientId).Select(m => new ContactMessageDto(m.Client!.Name!, m.Client.Email, m.Text, m.DateTime)));
    }

    [HttpGet]
    [Route("orders")]
    [Produces(typeof(IEnumerable<OrderDto>))]
    public IActionResult ListOrders() {
        return base.Ok(dbContext.Orders!.Select(o => o.ToDto()).ToList());
    }

    [HttpGet]
    [Route("clients")]
    [Produces(typeof(IEnumerable<Client>))]
    public IActionResult ListClients() {
        return base.Ok(dbContext.Clients!.Select(c => c).ToList());
    }

    [HttpGet]
    [Route("clients/{id}")]
    [Produces(typeof(ClientDto))]
    public IActionResult GetClient(int id) {
        var client = dbContext.Clients!.FirstOrDefault(c => c.Id == id);
        if (client == null || string.IsNullOrWhiteSpace(client.Name))
            return NotFound();
        return Ok(new ClientDto(client.Name, client.Email));
    }

    [HttpGet]
    [Produces(typeof(OrderDto))]
    [Route("orders/{orderId}")]
    public IActionResult GetOrder(int orderId) {
        var order = dbContext.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        return base.Ok(order.ToDto());
    }

    [HttpGet]
    [Produces(typeof(OrderDto))]
    [Route("orders/by-number/{number}")]
    public IActionResult GetOrderByNumber(string number) {
        var order = dbContext.Orders!.FirstOrDefault(o => o.Number == $"#{number}");
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        return base.Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(MessageDto))]
    [Route("orders/{orderId}")]
    public IActionResult AddMessage([FromBody] Message message, int orderId) {
        var order = dbContext.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        message.OrderId = order.Id;
        message.DateTime = DateTime.UtcNow;
        message.FromClient = false;
        order.Messages!.Add(message);
        dbContext.SaveChanges();
        Client? client = GetClientById(order.ClientId);
        if(client != null) {
            string link = $"{appOptions.BaseUrl}/my-orders/{order.Number.Remove(0,1)}";
            string subject = $"An admin sent you a message!";
            string text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderMessage.sbn")).Render(new { Name = client.Name, Id = order.Number, Link = link });
            string html = Template.Parse(System.IO.File.ReadAllText("Email/OrderMessage.sbn")).Render(new { Name = client.Name, Id = order.Number, Link = link });
            EmailService.SendEmail(client.Email, subject, text, html);
        }
        return Ok(message.ToDto());
    }

    [HttpPost]
    [Produces(typeof(OrderDto))]
    [Route("orders/{orderId}/state")]
    public IActionResult SetOrderState([FromBody] OrderState state, int orderId)
    {
        var order = dbContext.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        order.State = state;

        if (state == OrderState.Accepted)
        {
            order.PaymentToken = Guid.NewGuid().ToString().Replace("-", "");
        }
        dbContext.SaveChanges();
        EmailService.SendOrderStateEmail(order);
        return Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(OrderDto))]
    [Route("orders/{orderId}/state-with-msg")]
    public IActionResult SetOrderStateWithMessage([FromBody] OrderStateWithMessageDto data, int orderId) {
        var order = dbContext.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        order.State = data.State;

        if (data.State == OrderState.Accepted)
        {
            order.PaymentToken = Guid.NewGuid().ToString().Replace("-", "");
        }
        dbContext.SaveChanges();
        EmailService.SendOrderStateEmail(order, data.Message.Text);
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        data.Message.OrderId = order.Id;
        data.Message.DateTime = DateTime.UtcNow;
        data.Message.FromClient = false;
        order.Messages!.Add(data.Message);
        dbContext.SaveChanges();
        return Ok(order.ToDto());
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<GitAccessDto>))]
    [Route("git-accesses/{accessId}")]
    public IActionResult GetGitAccesses(int accessId) {
        var access = dbContext.GitAccesses!.FirstOrDefault(o => o.Id == accessId);
        if (access == null)
            return NotFound();
        return base.Ok(access.ToDto());
    }
}

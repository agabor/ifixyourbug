using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Scriban;
using IFYB.Services;

namespace IFYB.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = Policies.AdminOnly)]
public class AdminController : ControllerBase
{

    private ApplicationDbContext dbContext { get; }
    public EmailService EmailService { get; }

    public AdminController(ApplicationDbContext dbContext, EmailService emailService) 
    {
        this.dbContext = dbContext;
        this.EmailService = emailService;
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
    [Produces(typeof(OrderDto))]
    [Route("orders/{orderId}")]
    public IActionResult GetOrder(int orderId) {
        var order = dbContext.Orders!.FirstOrDefault(o => o.Id == orderId);
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
            string link = $"https://ifyb.com/my-orders/{order.Number}";
            string subject = $"An admin sent you a message!";
            string text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderMessage.sbn")).Render(new { Name = client.Name, Id = order.Number });
            string html = Template.Parse(System.IO.File.ReadAllText("Email/OrderMessage.sbn")).Render(new { Name = client.Name, Id = order.Number });
            EmailService.SendEmail(client.Email, subject, text, html);
        }
        return Ok(message.ToDto());
    }

    [HttpPost]
    [Produces(typeof(OrderDto))]
    [Route("orders/{orderId}/state")]
    public IActionResult SetOrderState([FromBody] OrderState state, int orderId)
    {
        dbContext.Database.EnsureCreated();
        var order = dbContext.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        order.State = state;
        Client? client = GetClientById(order.ClientId);
        if (client == null)
            return BadRequest();
        string link = $"https://ifyb.com/my-orders/{order.Number}";
        string subject = "";
        string text = "";
        string html = "";
        if (state == OrderState.Accepted)
        {
            order.PaymentToken = Guid.NewGuid().ToString().Replace("-", "");
            subject = $"We will process your order!";
            text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderAccept.sbn")).Render(new { Name = client.Name, PaymentToken = order.PaymentToken });
            html = Template.Parse(System.IO.File.ReadAllText("Email/OrderAccept.sbn")).Render(new { Name = client.Name, PaymentToken = order.PaymentToken });
        }
        else if (state == OrderState.Rejected)
        {
            subject = $"We rejected your order!";
            text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderReject.sbn")).Render(new { Name = client.Name, Link = link });
            html = Template.Parse(System.IO.File.ReadAllText("Email/OrderReject.sbn")).Render(new { Name = client.Name, Link = link });
        }
        else if (state == OrderState.Completed)
        {
            subject = $"We've fixed your bug!";
            text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderComplete.sbn")).Render(new { Name = client.Name, Link = link });
            html = Template.Parse(System.IO.File.ReadAllText("Email/OrderComplete.sbn")).Render(new { Name = client.Name, Link = link });
        }
        else if (state == OrderState.Refundable)
        {
            subject = $"We can't fix your bug!";
            text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderRefund.sbn")).Render(new { Name = client.Name, Link = link });
            html = Template.Parse(System.IO.File.ReadAllText("Email/OrderRefund.sbn")).Render(new { Name = client.Name, Link = link });
        }
        dbContext.SaveChanges();

        if (subject != "")
        {
            EmailService.SendEmail(client.Email, subject, text, html);
        }
        return Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(OrderDto))]
    [Route("orders/{orderId}/state-with-msg")]
    public IActionResult SetOrderStateWithMessage([FromBody] OrderStateWithMessageDto data, int orderId) {
        dbContext.Database.EnsureCreated();
        var order = dbContext.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        order.State = data.State;
        dbContext.SaveChanges();
        Client? client = GetClientById(order.ClientId);
        if(client != null) {
            string link = $"https://ifyb.com/my-orders/{order.Number}";
            string subject = "";
            string text = "";
            string html = "";
            if(data.State == OrderState.Accepted) {
                subject = $"We will process your order!";
                text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderAccept.sbn")).Render(new { Name = client.Name, Message = data.Message.Text });
                html = Template.Parse(System.IO.File.ReadAllText("Email/OrderAccept.sbn")).Render(new { Name = client.Name, Message = data.Message.Text });
            } else if(data.State == OrderState.Rejected) {
                subject = $"We rejected your order!";
                text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderReject.sbn")).Render(new { Name = client.Name, Link = link, Message = data.Message.Text });
                html = Template.Parse(System.IO.File.ReadAllText("Email/OrderReject.sbn")).Render(new { Name = client.Name, Link = link, Message = data.Message.Text });
            } else if(data.State == OrderState.Completed) {
                subject = $"We've fixed your bug!";
                text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderComplete.sbn")).Render(new { Name = client.Name, Link = link, Message = data.Message.Text });
                html = Template.Parse(System.IO.File.ReadAllText("Email/OrderComplete.sbn")).Render(new { Name = client.Name, Link = link, Message = data.Message.Text });
            } else if(data.State == OrderState.Refundable) {
                subject = $"We can't fix your bug!";
                text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderRefund.sbn")).Render(new { Name = client.Name, Link = link, Message = data.Message.Text });
                html = Template.Parse(System.IO.File.ReadAllText("Email/OrderRefund.sbn")).Render(new { Name = client.Name, Link = link, Message = data.Message.Text });
            }
            if(subject != "") {
                EmailService.SendEmail(client.Email, subject, text, html);
                dbContext.Entry(order).Collection(o => o.Messages!).Load();
                AddMessage(data.Message, orderId);
            } else {
                return BadRequest();
            }
        }
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

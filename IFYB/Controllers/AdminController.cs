using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;
using IFYB.Filters;
using Microsoft.EntityFrameworkCore;
using Scriban;
using IFYB.Services;

namespace IFYB.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize]
[AdminFilter]
public class AdminController : ControllerBase
{
    private ApplicationDbContext dbContext { get; }
    public EmailService EmailService { get; }

    public AdminController(ApplicationDbContext dbContext, EmailService emailService) 
    {
        this.dbContext = dbContext;
        this.EmailService = emailService;
    }


    [HttpGet]
    [Route("contact-messages")]
    [Produces(typeof(ContactMessageDto))]
    public IActionResult GetContactMessages()
    {
        return Ok(dbContext.Messages.Include(m => m.Client).Where(m => m.OrderId == null).Select(m => new ContactMessageDto(m.Client!.Name!, m.Client.Email, m.Text)));
    }

    [HttpGet]
    [Route("orders")]
    [Produces(typeof(IEnumerable<OrderDto>))]
    public IActionResult ListOrders() {
        return base.Ok(dbContext.Orders!.Select(o => o.ToDto()).ToList());
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
        return Ok(message.ToDto());
    }


    [HttpPost]
    [Produces(typeof(OrderDto))]
    [Route("orders/{orderId}/state")]
    public IActionResult SetOrderState([FromBody] OrderState state, int orderId) {
        dbContext.Database.EnsureCreated();
        var order = dbContext.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        order.State = state;
        dbContext.SaveChanges();
        Client? client = order.Client;
        if(client != null) {
            string link = $"https://ifyb.com/my-orders/{order.Number}";
            if(state == OrderState.Accepted){
                string subject = $"We will process your order!";
                string text = $"Dear {client.Name},\nWe’ve checked your order and we want to work with you. One of us will reach you out soon.\nWe accepted your order; our team is started to work on the solution for your bug.\nIf you have further questions, you can contact us.";
                string html = Template.Parse(System.IO.File.ReadAllText("Email/OrderAccept.sbn")).Render(new { Name = client.Name });
                EmailService.SendEmail(client.Email, subject, text, html);
            } else if(state == OrderState.Rejected){
                string subject = $"We rejected your order!";
                string text = $"Dear {client.Name},\nWe're sorry, but we can't accept your order.\nThe reason is:\nIf you have further questions, you can contact us.";
                string html = Template.Parse(System.IO.File.ReadAllText("Email/OrderReject.sbn")).Render(new { Name = client.Name, Link = link });
                EmailService.SendEmail(client.Email, subject, text, html);
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

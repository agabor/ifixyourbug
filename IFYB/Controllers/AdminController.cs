using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using IFYB.Services;
using Microsoft.Extensions.Options;

namespace IFYB.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = Policies.AdminOnly)]
public class AdminController : ControllerBase
{
    private readonly AppOptions appOptions;
    private readonly ApplicationDbContext dbContext;
    private readonly EmailDispatchService emailDispatchService;

    public AdminController(ApplicationDbContext dbContext, EmailDispatchService emailService, IOptions<AppOptions> appOptions) 
    {
        this.dbContext = dbContext;
        this.emailDispatchService = emailService;
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
    [Route("contact-emails/{email}")]
    [Produces(typeof(ContactMessageDto))]
    public IActionResult GetClientContactEmails(string email)
    {
        return Ok(dbContext.Emails.Where(e => e.ToEmail == email).Select(e => new ContactEmailDto(e.Subject, e.Text)));
    }

    [Route("stackoverflow-requests")]
    [Produces(typeof(StackoverflowRequestDto))]
    public IActionResult GetStackoverflowRequests()
    {
        return Ok(dbContext.StackOverflowRequests.Include(r => r.Client).Select(r => new StackoverflowRequestDto(r.Number, r.Client!.Name!, r.Client.Email, r.DateTime, r.Url, r.Text, r.Solved)));
    }

    [HttpGet]
    [Produces(typeof(StackoverflowRequestDto))]
    [Route("stackoverflow-requests/{number}")]
    public IActionResult GetStackoverflowRequestByNumber(int number) {
        var request = dbContext.StackOverflowRequests!.Single(s => s.Number == number);
        if (request == null)
            return NotFound();
        dbContext.Entry(request).Reference(r => r.Client!).Load();
        return base.Ok(request.ToDto());
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
    [Route("orders/{number}")]
    public IActionResult GetOrderByNumber(int number) {
        var order = dbContext.Orders!.Single(o => o.Number == number);
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        return base.Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(MessageDto))]
    [Route("orders/{number}")]
    public IActionResult AddMessage([FromBody] Message message, int number) {
        var order = dbContext.Orders!.Single(o => o.Number == number);
        if (order == null)
            return NotFound();
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        message.OrderId = order.Id;
        message.ClientId = order.ClientId;
        message.DateTime = DateTime.UtcNow;
        message.FromClient = false;
        order.Messages!.Add(message);
        dbContext.SaveChanges();
        Client? client = GetClientById(order.ClientId);
        if(client != null) {
            emailDispatchService.DispatchEmail(client.Id, client.Email, "OrderMessage", order, new { Name = client.Name, Message = message.Text.ToHtml() });
        }
        return Ok(message.ToDto());
    }

    [HttpPost]
    [Produces(typeof(OrderDto))]
    [Route("orders/{number}/state")]
    public IActionResult SetOrderState([FromBody] OrderState state, int number)
    {
        var order = dbContext.Orders!.Single(o => o.Number == number);
        if (order == null)
            return NotFound();
        order.State = state;

        if (state == OrderState.Accepted)
        {
            order.PaymentToken = Guid.NewGuid().ToString().Replace("-", "");
        }
        dbContext.SaveChanges();
        emailDispatchService.DispatchOrderStateEmail(order);
        return Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(OrderDto))]
    [Route("orders/{number}/state-with-msg")]
    public IActionResult SetOrderStateWithMessage([FromBody] OrderStateWithMessageDto data, int number) {
        var order = dbContext.Orders!.Single(o => o.Number == number);
        if (order == null)
            return NotFound();
        order.State = data.State;

        if (data.State == OrderState.Accepted)
        {
            order.PaymentToken = Guid.NewGuid().ToString().Replace("-", "");
        }
        dbContext.SaveChanges();
        emailDispatchService.DispatchOrderStateEmail(order, data.Message.Text);
        dbContext.Entry(order).Collection(o => o.Messages!).Load();
        data.Message.ClientId = order.ClientId;
        data.Message.OrderId = order.Id;
        data.Message.DateTime = DateTime.UtcNow;
        data.Message.FromClient = false;
        order.Messages!.Add(data.Message);
        dbContext.SaveChanges();
        return Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(StackoverflowRequestDto))]
    [Route("stackoverflow-requests/{number}/solved-with-msg")]
    public IActionResult SetStackoverflowRequestSolvedWithMessage([FromBody] StackoverflowRequestSolvedWithMessageDto data, int number) {
        var request = dbContext.StackOverflowRequests!.Single(s => s.Number == number);
        if (request == null)
            return NotFound();
        request.Solved = data.Solved;
        dbContext.Entry(request).Reference(r => r.Client!).Load();
        if(request.Client != null) {
            emailDispatchService.DispatchEmail(request.Client.Id, request.Client.Email, "StackOverflowRequestSolved", null, new { Name = request.Client.Name, Message = data.Message.ToHtml(), Url = request.Url });
        }
        dbContext.SaveChanges();
        return Ok(request.ToDto());
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

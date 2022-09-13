
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Mvc;
using Scriban;
using IFYB.Services;

namespace IFYB.Controllers;

[ApiController]
[Route("api/contact")]
public class ContactController : BaseController
{
    private readonly EmailDispatchService emailDispatchService;
    public ContactController(ApplicationDbContext dbContext, EmailDispatchService emailDispatchService) : base(dbContext)
    {
        this.emailDispatchService = emailDispatchService;
    }

    [HttpPost]
    public IActionResult SaveMessage([FromBody] ContactMessageDto dto)
    {
        var client = GetClient();
        if (client == null) {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest();
            client = dbContext.Clients.FirstOrDefault(c => c.Email == dto.Email);
            if (client == null) {
                client = dbContext.Clients.Add(new Client(dto.Email) {
                    Name = dto.Name
                }).Entity;
            } else if (string.IsNullOrWhiteSpace(client.Name)) {
                client.Name = dto.Name;
            }
        }
        dbContext.Messages.Add(new Message(dto.Text) {
            ClientId = client.Id,
            Client = client,
            DateTime = DateTime.UtcNow
        });
        if(client != null) {
            emailDispatchService.DispatchEmail(client.Email, "ContactMessage", null, new { Name = client.Name });
        }
        var admins = dbContext.Admins.ToList();
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail(admin.Email, "ContactMessageToAdmin", null, new { Name = client?.Name != null ? client.Name : "unknown user" }, true);
        }
        dbContext.SaveChanges();
        return Ok();
    }
}
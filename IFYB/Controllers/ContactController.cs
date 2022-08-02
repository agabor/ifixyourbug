
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
    public EmailCreationService EmailService { get; }
    public ContactController(ApplicationDbContext dbContext, EmailCreationService emailService) : base(dbContext)
    {
        EmailService = emailService;
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
            EmailService.CreateEmail(client.Email, "ContactMessage", null, new { Name = client.Name });
        }
        dbContext.SaveChanges();
        return Ok();
    }
}
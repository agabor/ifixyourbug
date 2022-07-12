
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
    public EmailService EmailService { get; }
    public ContactController(ApplicationDbContext dbContext, EmailService emailService) : base(dbContext)
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
            string subject = $"An admin sent you a message!";
            string text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/ContactMessage.sbn")).Render(new { Name = client.Name });
            string html = Template.Parse(System.IO.File.ReadAllText("Email/ContactMessage.sbn")).Render(new { Name = client.Name });
            EmailService.SendEmail(client.Email, subject, text, html);
        }
        dbContext.SaveChanges();
        return Ok();
    }
}
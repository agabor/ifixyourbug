
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Mvc;
using Scriban;
using IFYB.Services;

namespace IFYB.Controllers;

[ApiController]
[Route("api/stackoverflow")]
public class StackoverflowController : BaseController
{
    private readonly EmailDispatchService emailDispatchService;
    public StackoverflowController(ApplicationDbContext dbContext, EmailDispatchService emailDispatchService) : base(dbContext)
    {
        this.emailDispatchService = emailDispatchService;
    }

    [HttpPost]
    public IActionResult SaveRequest([FromBody] StackoverflowRequestDto dto)
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
        dbContext.StackoverflowRequests.Add(new StackoverflowRequest(dto.Url, dto.Text) {
            ClientId = client.Id,
            Client = client,
            DateTime = DateTime.UtcNow
        });
        if(client != null) {
            emailDispatchService.DispatchEmail(client.Email, "StackoverflowRequest", null, new { Name = client.Name });
        }
        var admins = dbContext.Admins.ToList();
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail(admin.Email, "StackoverflowRequestToAdmin", null, new { Name = client?.Name != null ? client.Name : "unknown user" }, true);
        }
        dbContext.SaveChanges();
        return Ok();
    }
}
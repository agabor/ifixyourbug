
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
        var today = (DateTime.UtcNow - DateTime.UnixEpoch).Days;
        var reuqestsToday = dbContext.StackOverflowRequests.Where(r => r.CreationDay == today).Count();
        dto.Number = int.Parse(DateTime.UtcNow.ToString("yyMMdd") + (reuqestsToday+1).ToString("D3"));
        dbContext.StackOverflowRequests.Add(new StackOverflowRequest(dto.Number, dto.Url, dto.Text) {
            ClientId = client.Id,
            Client = client,
            DateTime = DateTime.UtcNow
        });
        if(client != null) {
            emailDispatchService.DispatchEmail(client.Id, client.Email, "StackOverflowRequest", null, new { Name = client.Name, Url = dto.Url });
        }
        var admins = dbContext.Admins.ToList();
        foreach(var admin in admins) {
            emailDispatchService.DispatchEmail(admin.Id, admin.Email, "StackOverflowRequestToAdmin", null, new { Name = client?.Name ?? "unknown user", Url = dto.Url }, true);
        }
        dbContext.SaveChanges();
        return Ok();
    }
}
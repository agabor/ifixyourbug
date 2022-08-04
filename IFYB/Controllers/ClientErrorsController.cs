using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using IFYB.Entities;

namespace IFYB.Controllers;

[ApiController]
[Route("api/errors")]
public class ClientErrorsController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public ClientErrorsController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    [Route("")]
    public IActionResult LogError([FromBody] JsonElement data) {
        dbContext.ClientErrors.Add(new ClientError{
            DateTime = DateTime.UtcNow,
            UserAgent = Request.Headers["User-Agent"].FirstOrDefault()!,
            Data = data.ToString()
        });
        dbContext.SaveChanges();
        return Ok();
    }
}

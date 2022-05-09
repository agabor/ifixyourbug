using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;

namespace IFYB.Controllers;

[ApiController]
[Route("clients")]
public class ClientsController : ControllerBase
{
    ApplicationDBContext dbContext { get; }
    public ClientsController(ApplicationDBContext applicationDBContext) {
        dbContext = applicationDBContext;
    }


    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult StartSession([FromBody] EmailDto dto) {
        dbContext.Database.EnsureCreated();
        var client = dbContext.Clients.FirstOrDefault(c => c.Email == dto.Email);
        if (client == null) {
            client = new Client(dto.Email);
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
        }
        return Ok(new IdDto(client.Id));
    }

    [HttpGet]
    [Route("{clientId}/name")]
    [Produces(typeof(NameDto))]
    public IActionResult GetName(int clientId)
    {
        var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null || string.IsNullOrWhiteSpace(client.Name))
            return NotFound();
        return Ok(new NameDto(client.Name));
    }

    [HttpPost]
    [Route("{clientId}/name")]
    public IActionResult SetName(int clientId, NameDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest();
        var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        client.Name = dto.Name;
        dbContext.SaveChanges();
        return Ok();
    }
}

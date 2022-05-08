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

    [HttpGet]
    [Produces(typeof(IEnumerable<ClientDto>))]
    public IActionResult ListClients() {
        dbContext.Database.EnsureCreated();
        return Ok(dbContext.Clients);
    }

    [HttpGet]
    [Route("{clientId}")]
    [Produces(typeof(ClientDto))]
    public IActionResult GetClient(int clientId) {
        dbContext.Database.EnsureCreated();
        Client? client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        return base.Ok(client.ToDto());
    }


    [HttpPost]
    public IActionResult AddClient([FromBody] ClientDto client) {
        dbContext.Database.EnsureCreated();
        dbContext.Clients.Add(Client.FromDto(client));
        dbContext.SaveChanges();
        return Ok();
    }
}

using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;

namespace IFYB.Controllers;

[ApiController]
[Route("clients")]
public class ClientsController : ControllerBase
{
    ApplicationDBContext ApplicationDBContext { get; }
    public ClientsController(ApplicationDBContext applicationDBContext) {
        ApplicationDBContext = applicationDBContext;
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<ClientDto>))]
    public IActionResult ListClients() {
        ApplicationDBContext.Database.EnsureCreated();
        return Ok(ApplicationDBContext.Clients);
    }

    [HttpGet]
    [Route("{clientId}")]
    [Produces(typeof(ClientDto))]
    public IActionResult GetClient(int clientId) {
        ApplicationDBContext.Database.EnsureCreated();
        Client? client = ApplicationDBContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        return base.Ok(client.ToDto());
    }


    [HttpPost]
    public IActionResult AddClient([FromBody] ClientDto client) {
        ApplicationDBContext.Database.EnsureCreated();
        ApplicationDBContext.Clients.Add(Client.FromDto(client));
        ApplicationDBContext.SaveChanges();
        return Ok();
    }
}

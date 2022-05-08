using Microsoft.AspNetCore.Mvc;

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
    public IActionResult ListClients() {
        ApplicationDBContext.Database.EnsureCreated();
        return Ok(ApplicationDBContext.Clients);
    }

    [HttpPost]
    public IActionResult AddClient([FromBody] Client client) {
        ApplicationDBContext.Database.EnsureCreated();
        ApplicationDBContext.Clients.Add(client);
        ApplicationDBContext.SaveChanges();
        return Ok();
    }
}

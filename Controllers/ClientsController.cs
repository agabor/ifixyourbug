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

    [HttpGet]
    [Route("{clientId}")]
    public IActionResult GetClient(int clientId) {
        ApplicationDBContext.Database.EnsureCreated();
        Client? client = ApplicationDBContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        return base.Ok(client);
    }


    [HttpPost]
    public IActionResult AddClient([FromBody] Client client) {
        ApplicationDBContext.Database.EnsureCreated();
        ApplicationDBContext.Clients.Add(client);
        ApplicationDBContext.SaveChanges();
        return Ok();
    }
}

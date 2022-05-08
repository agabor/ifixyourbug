using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IFYB.Controllers;

[ApiController]
[Route("clients/{clientId}/orders")]
public class OrdersController : ControllerBase
{
    ApplicationDBContext dbContext { get; }
    public OrdersController(ApplicationDBContext applicationDBContext) {
        dbContext = applicationDBContext;
    }

    [HttpGet]
    public IActionResult ListOrders(int clientId) {
        dbContext.Database.EnsureCreated();
        var client = dbContext.Clients.Include(c => c.Orders!).ThenInclude(o => o.GitAccess).FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        return base.Ok(client.Orders);
    }

    [HttpPost]
    public IActionResult AddOrder(int clientId, [FromBody] Order order) {
        dbContext.Database.EnsureCreated();
        var client = dbContext.Clients.Include(c => c.Orders!).FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        client.Orders!.Add(order);
        dbContext.SaveChanges();
        return Ok();
    }
}

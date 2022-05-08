using Microsoft.AspNetCore.Mvc;

namespace IFYB.Controllers;

[ApiController]
[Route("clients")]
public class OrdersController : ControllerBase
{
    ApplicationDBContext dbContext { get; }
    public OrdersController(ApplicationDBContext applicationDBContext) {
        dbContext = applicationDBContext;
    }

    [HttpGet]
    [Route("{clientId}")]
    public IActionResult ListOrders(int clientId) {
        dbContext.Database.EnsureCreated();
        var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders!).Load();
        return base.Ok(client.Orders);
    }

    [HttpPost]
    public IActionResult AddOrder([FromBody] Order order) {
        dbContext.Database.EnsureCreated();
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
        return Ok();
    }
}

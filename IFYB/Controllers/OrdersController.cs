using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;

namespace IFYB.Controllers;

[ApiController]
[Route("orders")]
[Authorize]
public class OrdersController : BaseController
{
    public OrdersController(ApplicationDbContext dbContext) : base(dbContext)
     {
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<OrderDto>))]
    public IActionResult ListOrders() {
        dbContext.Database.EnsureCreated();
        var client = CurrentClient;
        if (client == null)
            return NotFound();
        return base.Ok(client.Orders!.Select(o => o.ToDto()).ToList());
    }

    [HttpGet]
    [Produces(typeof(OrderDto))]
    [Route("{orderId}")]
    public IActionResult GetOrder(int orderId) {
        dbContext.Database.EnsureCreated();
        var client = CurrentClient;
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = client.Orders!.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return NotFound();
        return base.Ok(order.ToDto());
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult AddOrder([FromBody] OrderDto dto) {
        dbContext.Database.EnsureCreated();
        var client = CurrentClient;
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.Orders).Load();
        var order = Order.FromDto(dto);
        client.Orders!.Add(order);
        dbContext.SaveChanges();
        return Ok(new IdDto(order.Id));
    }
}

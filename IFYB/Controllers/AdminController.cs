using Microsoft.AspNetCore.Mvc;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;
using IFYB.Filters;
using Microsoft.EntityFrameworkCore;

namespace IFYB.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize]
[AdminFilter]
public class AdminController : ControllerBase
{
    private ApplicationDbContext dbContext { get; }

    public AdminController(ApplicationDbContext dbContext) 
    {
        this.dbContext = dbContext;
    }


    [HttpGet]
    [Route("contact-messages")]
    [Produces(typeof(ContactMessageDto))]
    public IActionResult GetContactMessages()
    {
        return Ok(dbContext.Messages.Include(m => m.Client).Where(m => m.OrderId == null).Select(m => new ContactMessageDto(m.Client!.Name!, m.Client.Email, m.Text)));
    }

    [HttpGet]
    [Route("orders")]
    [Produces(typeof(IEnumerable<OrderDto>))]
    public IActionResult ListOrders() {
        dbContext.Database.EnsureCreated();
        return base.Ok(dbContext.Orders!.Select(o => o.ToDto()).ToList());
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<GitAccessDto>))]
    [Route("git-accesses/{accessId}")]
    public IActionResult GetGitAccesses(int accessId) {
        dbContext.Database.EnsureCreated();
        var access = dbContext.GitAccesses!.FirstOrDefault(o => o.Id == accessId);
        if (access == null)
            return NotFound();
        return base.Ok(access.ToDto());
    }
}

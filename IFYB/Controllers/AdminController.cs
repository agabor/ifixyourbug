using Microsoft.AspNetCore.Mvc;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;
using IFYB.Filters;
using Microsoft.EntityFrameworkCore;

namespace IFYB.Controllers;

[ApiController]
[Route("admin")]
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
}
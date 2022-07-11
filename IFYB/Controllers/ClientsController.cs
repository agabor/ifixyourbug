using Microsoft.AspNetCore.Mvc;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;

namespace IFYB.Controllers;

[ApiController]
[Route("api/clients")]
[Authorize(Policy = Policies.ClientOnly)]
public class ClientsController : BaseController
{
    public IConfiguration Configuration { get; }

    public ClientsController(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        Configuration = configuration;
    }


    [HttpGet]
    [Route("name")]
    [Produces(typeof(NameDto))]
    public IActionResult GetName()
    {
        var client = GetClient();
        if (client == null || string.IsNullOrWhiteSpace(client.Name))
            return NotFound();
        return Ok(new NameDto(client.Name));
    }

    [HttpPost]
    [Route("name")]
    public IActionResult SetName(NameDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest();
        var client = GetClient();
        if (client == null)
            return NotFound();
        client.Name = dto.Name;
        dbContext.SaveChanges();
        return Ok();
    }
}

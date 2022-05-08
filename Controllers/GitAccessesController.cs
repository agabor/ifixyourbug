using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IFYB.Entities;
using IFYB.Models;

namespace IFYB.Controllers;

[ApiController]
[Route("clients/{clientId}/git-accesses")]
public class GitAccessesController : ControllerBase
{
    ApplicationDBContext dbContext { get; }
    public GitAccessesController(ApplicationDBContext applicationDBContext) {
        dbContext = applicationDBContext;
    }


    [HttpGet]
    [Produces(typeof(IEnumerable<GitAccessDto>))]
    public IActionResult ListGitAccesses(int clientId) {
        dbContext.Database.EnsureCreated();
        var client = dbContext.Clients.Include(c => c.GitAccesses!).FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        return base.Ok(client.GitAccesses!.Select(o => o.ToDto()).ToList());
    }

    [HttpPost]
    public IActionResult AddOrder(int clientId, [FromBody] GitAccessDto access) {
        dbContext.Database.EnsureCreated();
        var client = dbContext.Clients.Include(c => c.GitAccesses!).FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return NotFound();
        client.GitAccesses!.Add(GitAccess.FromDto(access));
        dbContext.SaveChanges();
        return Ok();
    }

}
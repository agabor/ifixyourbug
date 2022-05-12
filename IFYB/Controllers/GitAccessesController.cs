using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;

namespace IFYB.Controllers;

[ApiController]
[Route("git-accesses")]
[Authorize]
public class GitAccessesController : BaseController
{
    public GitAccessesController(ApplicationDbContext dbContext) : base(dbContext) {
    }


    [HttpGet]
    [Produces(typeof(IEnumerable<GitAccessDto>))]
    public IActionResult ListGitAccesses() {
        dbContext.Database.EnsureCreated();
        var client = GetClient();
        if (client == null)
            return NotFound();
        return base.Ok(client.GitAccesses!.Select(o => o.ToDto()).ToList());
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult AddGitAccesses([FromBody] GitAccessDto access) {
        dbContext.Database.EnsureCreated();
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.GitAccesses).Load();
        var gitAccess = GitAccess.FromDto(access);
        client.GitAccesses!.Add(gitAccess);
        dbContext.SaveChanges();
        return Ok(new IdDto(gitAccess.Id));
    }

}
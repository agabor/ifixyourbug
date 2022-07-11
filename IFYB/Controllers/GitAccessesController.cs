using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Authorization;

namespace IFYB.Controllers;

[ApiController]
[Route("api/git-accesses")]
[Authorize(Policy = Policies.ClientOnly)]
public class GitAccessesController : BaseController
{
    public GitAccessesController(ApplicationDbContext dbContext) : base(dbContext) {
    }


    [HttpGet]
    [Produces(typeof(IEnumerable<GitAccessDto>))]
    public IActionResult ListGitAccesses() {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.GitAccesses).Load();
        return base.Ok(client.GitAccesses!.Select(o => o.ToDto()).ToList());
    }


    [HttpGet]
    [Produces(typeof(IEnumerable<GitAccessDto>))]
    [Route("{accessId}")]
    public IActionResult GetGitAccesses(int accessId) {
        var client = GetClient();
        if (client == null)
            return NotFound();
        dbContext.Entry(client).Collection(c => c.GitAccesses).Load();
        var access = client.GitAccesses!.FirstOrDefault(o => o.Id == accessId);
        if (access == null)
            return NotFound();
        return base.Ok(access.ToDto());
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult AddGitAccesses([FromBody] GitAccessDto access) {
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
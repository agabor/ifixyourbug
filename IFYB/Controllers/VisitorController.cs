using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IFYB.Controllers;

[ApiController]
[Route("api/visitor")]
public class VisitorController : BaseController
{
    private readonly AppOptions appOptions;
    public VisitorController(ApplicationDbContext dbContext, IOptions<AppOptions> appOptions) : base(dbContext)
    {
        this.appOptions = appOptions.Value;
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult SaveVisitor(VisitorDto dto) {
        var visitor = Visitor.FromDto(dto);
        visitor.DateTime = DateTime.UtcNow;
        visitor.UserAgent = Request.Headers["User-Agent"].FirstOrDefault()!;
        dbContext.Visitors.Add(visitor);
        dbContext.SaveChanges();
        return Ok(visitor.Id);
    }
}
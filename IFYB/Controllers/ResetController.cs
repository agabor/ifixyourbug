using Microsoft.AspNetCore.Mvc;

namespace IFYB.Controllers;

#if DEBUG

[ApiController]
[Route("reset")]
public class ResetController : ControllerBase
{
    ApplicationDbContext dbContext { get; }
    public ResetController(ApplicationDbContext applicationDBContext) {
        dbContext = applicationDBContext;
    }

    [HttpGet]
    public IActionResult Reset() {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        return Ok();
    }
}

#endif
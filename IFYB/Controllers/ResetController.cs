using Microsoft.AspNetCore.Mvc;

namespace IFYB.Controllers;

#if DEBUG

[ApiController]
[Route("reset")]
public class ResetController : ControllerBase
{
    ApplicationDBContext dbContext { get; }
    public ResetController(ApplicationDBContext applicationDBContext) {
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
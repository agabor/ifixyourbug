using IFYB.Models;
using Microsoft.AspNetCore.Mvc;

namespace IFYB.Controllers;

[ApiController]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    Settings settings;
    public SettingsController(Settings settings) {
        this.settings = settings;
    }

    [HttpGet]
    [Produces(typeof(Settings))]
    public IActionResult GetOffer() {
        return Ok(settings);
    }
}

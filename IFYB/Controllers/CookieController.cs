using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IFYB.Controllers;

[ApiController]
[Route("api/cookie")]
public class CookieController : BaseController
{
    private readonly AppOptions appOptions;
    public CookieController(ApplicationDbContext dbContext, IOptions<AppOptions> appOptions) : base(dbContext)
    {
        this.appOptions = appOptions.Value;
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult saveCookie(CookieDto dto) {
        var cookie = Cookie.FromDto(dto);
        cookie.Id = 0;
        cookie.DateTime = DateTime.UtcNow;
        cookie.UserAgent = Request.Headers["User-Agent"].FirstOrDefault()!;
        dbContext.Cookies.Add(cookie);
        dbContext.SaveChanges();
        return Ok(cookie.Id);
    }
}
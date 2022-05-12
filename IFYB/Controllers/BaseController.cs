using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using System.Security.Claims;

namespace IFYB.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly ApplicationDbContext dbContext;

    protected BaseController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    protected Client? GetClient()
    {
        var email = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
            return null;
        return dbContext.Clients.Where(u => u.Email == email).FirstOrDefault();
    }
}

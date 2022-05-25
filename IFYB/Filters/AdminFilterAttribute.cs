
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IFYB.Filters;

public class AdminFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var value = filterContext.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (value != "admin")
            throw new UnauthorizedAccessException();
    }
}

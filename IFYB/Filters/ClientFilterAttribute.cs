
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IFYB.Filters;

public class ClientFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var value = filterContext.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (string.IsNullOrWhiteSpace(value))
            throw new UnauthorizedAccessException();
    }
}
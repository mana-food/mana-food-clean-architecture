using ManaFood.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManaFood.WebAPI.Filters;

public class CustomAuthorizeAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var jwtService = (IJwtService) context.HttpContext.RequestServices.GetService(typeof(IJwtService))!;
        var principal = jwtService?.ValidateToken(token);

        if (principal == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        context.HttpContext.User = principal;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Nenhuma ação necessária após a execução
    }
}
using ManaFood.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using ManaFood.Domain.Entities;

namespace ManaFood.WebAPI.Filters;

public class CustomAuthorizeAttribute : Attribute, IActionFilter
{
    private readonly string[] _roles;
    
    public CustomAuthorizeAttribute()
    {
        _roles = [];
    }

    // Permite receber UserType como parâmetro
    public CustomAuthorizeAttribute(params UserType[] userTypes)
    {
        _roles = userTypes.Select(ut => ut.ToString()).ToArray();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var jwtService = (IJwtService) context.HttpContext.RequestServices.GetService(typeof(IJwtService))!;
        var principal = jwtService?.ValidateToken(token);

        // Valida o token antes de acessar qualquer informação de roles
        if (principal == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        context.HttpContext.User = principal;

        // Só acessa roles se o token for válido
        if (_roles != null && _roles.Length > 0)
        {
            var userRoles = principal.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            // Compara ignorando maiúsculas/minúsculas
            bool hasRole = userRoles.Any(userRole =>
                _roles.Any(role => string.Equals(role, userRole, StringComparison.OrdinalIgnoreCase))
            );

            if (!hasRole)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Nenhuma ação necessária após a execução
    }
}
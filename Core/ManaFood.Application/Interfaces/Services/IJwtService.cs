using System.Security.Claims;

namespace ManaFood.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(Guid idUser, string emailUser);
    ClaimsPrincipal ValidateToken(string token);
}
using System.Security.Claims;
using ManaFood.Domain.Entities;

namespace ManaFood.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(Guid idUser, string emailUser, UserType userType);
    ClaimsPrincipal ValidateToken(string token);
}
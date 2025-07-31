using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManaFood.Application.Interfaces.Services;
using ManaFood.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ManaFood.Infrastructure.Auth;

public class JwtService(
    string secretKey,
    int expirationMinutes,
    string issuer,
    string audience,
    ITokenBlacklistService tokenBlacklistService)
    : IJwtService
{
    public string GenerateToken(Guid idUser, string emailUser, UserType userType)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, idUser.ToString()),
            new Claim(ClaimTypes.Email, emailUser),
            new Claim(ClaimTypes.Role, userType.ToString()),
            new Claim("created_at", DateTime.UtcNow.ToString("o"))
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        if (tokenBlacklistService.IsBlacklisted(token))
            return null;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
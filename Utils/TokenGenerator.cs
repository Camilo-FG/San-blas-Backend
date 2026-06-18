using Microsoft.IdentityModel.Tokens;
using SanblasBackend.Models;
using SanblasBackend.Models.EntitiesUsuarios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using SanblasBackend.Utils;

namespace SanblasBackend.Utils;

public static class TokenGenerator
{
    public static string GenerateToken(User user, JwtSettings jwtSettings) 
    {
        var claims = new[]
        {
            new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim (ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, UserRoles.ToJwtRole(user.Role))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: cred
        );        

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

using Diploma.Application.Interfaces.Security;
using Diploma.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Diploma.Infrastructure.Services.Security;

public class JwtGenerator(
    IOptions<JwtConfiguration> configuration,
    JwtSecurityTokenHandler handler,
    SymmetricSecurityKey securityKey) : IJwtGenerator
{
    private const int JWT_VALID_IN_MINUTES = 60 * 24 * 7;

    private readonly SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);


    public JwtResult Generate(Guid personId)
    {
        var activeAt = DateTimeOffset.Now;
        var expiresAt = activeAt.AddMinutes(JWT_VALID_IN_MINUTES);

        var claims = GenerateClaims(personId.ToString(), ["User"]);

        var token = new JwtSecurityToken(
            issuer: configuration.Value.Issuer,
            audience: configuration.Value.Audience,
            signingCredentials: signingCredentials,

            claims: claims,
            notBefore: activeAt.DateTime,
            expires: expiresAt.DateTime
        );

        var stringToken = handler.WriteToken(token);

        return new JwtResult
        {
            Jwt = stringToken,
            ExpiresAt = expiresAt,
        };
    }

    private static IEnumerable<Claim> GenerateClaims(string nameIdentifier, IEnumerable<string>? roles = null)
    {
        var claims = new List<Claim>
        {
            // Protect Before Replay attack
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, nameIdentifier)
        };

        if (roles != null)
        {
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }
        return claims;
    }
}
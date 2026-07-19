using Diploma.Application.Interfaces.Security;
using Diploma.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Diploma.Infrastructure.Services.Security;

public class JwtValidator(
    IOptions<JwtConfiguration> configuration,
    JwtSecurityTokenHandler handler,
    SymmetricSecurityKey signingKey) : IJwtValidator
{
    public bool IsValid(string token, JwtValidtion validtion = JwtValidtion.All)
    {
        var validationParamiters = new TokenValidationParameters
        {
            ValidateLifetime = validtion.HasFlag(JwtValidtion.Lifetime),

            ValidateIssuer = validtion.HasFlag(JwtValidtion.Issuer),
            ValidateAudience = validtion.HasFlag(JwtValidtion.Audience),
            ValidateIssuerSigningKey = validtion.HasFlag(JwtValidtion.IssuerSigningKey),

            ValidIssuer = configuration.Value.Issuer,
            ValidAudience = configuration.Value.Audience,
            IssuerSigningKey = signingKey,
        };

        try
        {
            handler.ValidateToken(token, validationParamiters, out _);
            return true;
        }
        catch (SecurityTokenException) // Token wygasł, nieprawidłowy podpis itp.
        {
            return false;
        }
        catch (ArgumentException) // Zły format tokena
        {
            return false;
        }
        catch
        {
            return false;
        }
    }
}
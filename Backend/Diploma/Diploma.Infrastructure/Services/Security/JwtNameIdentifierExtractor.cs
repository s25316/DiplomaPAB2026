using Base.Exceptions;
using Diploma.Application.Interfaces.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Diploma.Infrastructure.Services.Security;

public class JwtNameIdentifierExtractor(JwtSecurityTokenHandler handler) : IJwtNameIdentifierExtractor
{
    public Guid Extract(string token)
    {
        var nameIdentifier = handler
            .ReadJwtToken(token)
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;

        ArgumentNullException.ThrowIfNullOrWhiteSpace(nameIdentifier);
        return Guid.TryParse(nameIdentifier, out var guid)
            ? guid
            : throw new ServiceException.Other($"{nameof(ClaimTypes.NameIdentifier)} must be a Guid.");
    }
}
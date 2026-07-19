using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Diploma.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetNameIdentifier(this ClaimsPrincipal user, [NotNullWhen(true)] out Guid? nameIdentifier)
    {
        nameIdentifier = null;

        var nameIdentifierClaim = user
            .Claims
            .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier);

        if (nameIdentifierClaim is null)
            return false;

        var value = nameIdentifierClaim.Value;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!Guid.TryParse(value, out var guid))
            return false;

        nameIdentifier = guid;
        return true;
    }
}
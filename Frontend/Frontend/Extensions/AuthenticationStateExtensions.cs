using Microsoft.AspNetCore.Components.Authorization;

namespace Frontend.Extensions;

public static class AuthenticationStateExtensions
{
    public static Guid? GetPersonId(
        this AuthenticationState state
    )
    {
        var id = state.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(id))
            return null;

        if (!Guid.TryParse(id, out var guid))
            return null;

        return guid;
    }
}
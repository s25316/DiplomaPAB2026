using Microsoft.AspNetCore.Components.Authorization;

namespace Frontend.Extensions;

public static class AuthenticationStateExtensions
{
    public static string? GetPersonId(
        this AuthenticationState state
    ) => state.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
}
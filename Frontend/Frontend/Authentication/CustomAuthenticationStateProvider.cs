using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Frontend.Authentication;

public class CustomAuthenticationStateProvider(
    IUserSessionService sessionService
    ) : AuthenticationStateProvider
{
    private static readonly AuthenticationState Anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));
    private static readonly JwtSecurityTokenHandler jwtHandler = new();

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var jwtToken = await sessionService.GetJwtTokenAsync();

        if (jwtToken is null)
            return Anonymous;

        if (DateTimeOffset.Now >= jwtToken.ExpiresAt)
        {
            await sessionService.ClearJwtTokenAsync();
            return Anonymous;
        }

        try
        {
            var token = jwtHandler.ReadJwtToken(jwtToken.Value);
            var identity = new ClaimsIdentity(token.Claims, "JwtAuth");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch
        {
            await sessionService.ClearJwtTokenAsync();
            return Anonymous;
        }
    }

    public async Task NotifyUserLoggedInAsync()
    {
        var authState = await GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task NotifyUserLoggedOutAsync()
    {
        await sessionService.ClearJwtTokenAsync();
        await sessionService.ClearRefreshTokenAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
    }
}
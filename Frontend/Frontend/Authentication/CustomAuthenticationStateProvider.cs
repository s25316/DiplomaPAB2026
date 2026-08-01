using Diploma.Models.Persons.Authentication;
using Frontend.Configurations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Frontend.Authentication;

public class CustomAuthenticationStateProvider(
    IOptions<BackendHostConfiguration> options,
    IHttpClientFactory factory,
    IUserSessionService sessionService
    ) : AuthenticationStateProvider
{
    private const int JWT_TOKEN_TIMEOUT_IN_SECONDS = 10;
    private const string REFRESH_TOKEN_URL = "api/person/profile/refreshToken";

    private static readonly AuthenticationState Anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));
    private static readonly JwtSecurityTokenHandler jwtHandler = new();

    public async Task<bool> IsAuthenticatedAsync()
    {
        var authState = await GetAuthenticationStateAsync();
        var user = authState.User;
        return user.Identity is not null && user.Identity.IsAuthenticated;
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

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var jwtToken = await sessionService.GetJwtTokenAsync();
        var refreshToken = await sessionService.GetRefreshTokenAsync();

        if (jwtToken is null || refreshToken is null)
            return Anonymous;

        if (jwtToken.ExpiresAt < DateTimeOffset.Now.AddSeconds(JWT_TOKEN_TIMEOUT_IN_SECONDS))
        {
            await RefreshTokenAsync(jwtToken, refreshToken);
        }

        jwtToken = await sessionService.GetJwtTokenAsync();
        refreshToken = await sessionService.GetRefreshTokenAsync();

        if (jwtToken is null || refreshToken is null)
            return Anonymous;

        if (DateTimeOffset.Now >= jwtToken.ExpiresAt)
        {
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
            return Anonymous;
        }
    }

    private async Task RefreshTokenAsync(
        SessionData jwtToken,
        SessionData refreshToken)
    {
        using var client = factory.CreateClient();
        client.BaseAddress = new Uri(options.Value.Uri);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.Value);

        var model = new RefreshTokenRequest
        {
            RefreshToken = refreshToken.Value,
        };

        using var response = await client.PostAsJsonAsync(REFRESH_TOKEN_URL, model);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<RefreshTokenResult.Success>();
            if (result is not null)
            {
                await sessionService.SetJwtTokenAsync(new SessionData
                {
                    Value = result.JwtToken,
                    ExpiresAt = result.JwtTokenExpiresAt,
                });
                await sessionService.SetRefreshTokenAsync(new SessionData
                {
                    Value = result.RefreshToken,
                    ExpiresAt = result.RefreshTokenTokenExpiresAt,
                });
            }
        }
        else
        {
            await sessionService.ClearJwtTokenAsync();
            await sessionService.ClearRefreshTokenAsync();
        }
    }
}
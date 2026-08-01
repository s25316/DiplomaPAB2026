using Frontend.Authentication;
using Frontend.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Frontend.Services;

public interface IBackendHttpClientFactory
{
    Task<HttpClient> CreateUnAuthorizedClientAsync();
    Task<HttpClient?> CreateAuthorizedClientAsync();
}
public class BackendHttpClientFactory(
    IOptions<BackendHostConfiguration> options,
    IHttpClientFactory factory,
    IUserSessionService sessionService,
    CustomAuthenticationStateProvider stateProvider
    ) : IBackendHttpClientFactory
{
    public async Task<HttpClient> CreateUnAuthorizedClientAsync()
    {
        var client = factory.CreateClient();
        client.BaseAddress = new Uri(options.Value.Uri);
        return client;
    }

    public async Task<HttpClient?> CreateAuthorizedClientAsync()
    {
        var isAuthenticated = await stateProvider.IsAuthenticatedAsync();
        if (!isAuthenticated)
            return null;

        var jwtToken = await sessionService.GetJwtTokenAsync();
        var refreshToken = await sessionService.GetRefreshTokenAsync();

        if (jwtToken is null || refreshToken is null)
            return null;

        var client = await CreateUnAuthorizedClientAsync();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.Value);

        return client;
    }/*

    public async Task<HttpClient?> CreateAuthorizedClientAsync()
    {
        var jwtToken = await sessionService.GetJwtTokenAsync();
        var refreshToken = await sessionService.GetRefreshTokenAsync();

        if (jwtToken is null || refreshToken is null)
            return null;

        if (refreshToken.ExpiresAt < DateTimeOffset.Now)
        {
            await stateProvider.NotifyUserLoggedOutAsync();
            return null;
        }

        var client = await CreateUnAuthorizedClientAsync();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.Value);

        if (jwtToken.ExpiresAt < DateTimeOffset.Now.AddSeconds(JWT_TOKEN_TIMEOUT_IN_SECONDS))
        {
            await RefreshTokenAsync(client, refreshToken);
        }

        return client;
    }

    private async Task RefreshTokenAsync(
        HttpClient client,
        SessionData refreshToken)
    {

        var model = new RefreshTokenRequest
        {
            RefreshToken = refreshToken.Value,
        };

        using var response = await client.PostAsJsonAsync(REFRESH_TOKEN_URL, model);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<RefreshTokenResult.Success>();
            if (result != null)
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

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.JwtToken);
                await stateProvider.NotifyUserLoggedInAsync();
            }
        }
        else
        {
            await stateProvider.NotifyUserLoggedOutAsync();
        }
    }*/
}
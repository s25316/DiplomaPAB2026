using Frontend.Authentication;
using Frontend.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Frontend.Services;

public interface IBackendHttpClientFactory
{
    Task<HttpClient> CreateClientAsync();
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

    public async Task<HttpClient> CreateClientAsync()
    {
        var client = await CreateUnAuthorizedClientAsync();
        var isAuthenticated = await stateProvider.IsAuthenticatedAsync();
        if (!isAuthenticated)
            return client;

        var jwtToken = await sessionService.GetJwtTokenAsync();
        var refreshToken = await sessionService.GetRefreshTokenAsync();

        if (jwtToken is null || refreshToken is null)
            return client;

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.Value);
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
    }
}
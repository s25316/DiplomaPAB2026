using Frontend.Services;

namespace Frontend.Authentication;

public sealed class SessionData
{
    public required string Value { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
}

public interface IUserSessionService
{
    Task<SessionData?> GetJwtTokenAsync();
    Task SetJwtTokenAsync(SessionData item);
    Task ClearJwtTokenAsync();

    Task<SessionData?> GetRefreshTokenAsync();
    Task SetRefreshTokenAsync(SessionData item);
    Task ClearRefreshTokenAsync();
}

public class UserSessionService(ILocalStorage localStorage) : IUserSessionService
{
    private const string JWT_TOKEN = nameof(JWT_TOKEN);
    private const string REFRESH_TOKEN = nameof(REFRESH_TOKEN);


    public async Task<SessionData?> GetJwtTokenAsync() => await localStorage.GetItemAsync<SessionData>(JWT_TOKEN);
    public async Task SetJwtTokenAsync(SessionData item) => await localStorage.SetItemAsync(JWT_TOKEN, item);
    public async Task ClearJwtTokenAsync() => await localStorage.RemoveItemAsync(JWT_TOKEN);

    public async Task<SessionData?> GetRefreshTokenAsync() => await localStorage.GetItemAsync<SessionData>(REFRESH_TOKEN);
    public async Task SetRefreshTokenAsync(SessionData item) => await localStorage.SetItemAsync(REFRESH_TOKEN, item);
    public async Task ClearRefreshTokenAsync() => await localStorage.RemoveItemAsync(REFRESH_TOKEN);
}
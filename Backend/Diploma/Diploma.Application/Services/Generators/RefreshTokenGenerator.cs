using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Services.Generators;

public sealed record RefreshTokenResult
{
    public required string RefreshToken { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
}

public interface IRefreshTokenGenerator
{
    RefreshTokenResult Generate();
}

public class RefreshTokenGenerator(IStringGenerator generator) : IRefreshTokenGenerator
{
    private const int BYTES_SIZE = 1024;
    private const int EXPIRES_AFTER_DAYS = 7;

    public RefreshTokenResult Generate() => new()
    {
        RefreshToken = generator.GenerateBase64String(BYTES_SIZE),
        ExpiresAt = DateTimeOffset.Now.AddDays(EXPIRES_AFTER_DAYS),
    };
}
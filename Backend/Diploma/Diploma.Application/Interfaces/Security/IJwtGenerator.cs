namespace Diploma.Application.Interfaces.Security;

public sealed record JwtResult
{
    public required string Jwt { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
}

public interface IJwtGenerator
{
    JwtResult Generate(Guid personId);
}
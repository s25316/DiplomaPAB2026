namespace Diploma.Infrastructure.Configurations;

public sealed class JwtConfiguration
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Secret { get; init; }
}
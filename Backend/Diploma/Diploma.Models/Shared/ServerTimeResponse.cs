namespace Diploma.Models.Shared;

public sealed record ServerTimeResponse
{
    public DateTimeOffset CurrentDateTime { get; init; } = DateTimeOffset.Now;
}
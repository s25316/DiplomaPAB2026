using Diploma.Domain.Persons.Aggregates;
using System.Diagnostics.CodeAnalysis;

namespace Diploma.Application.Persons.Authentication.Projections.RefreshTokens;

public sealed record PersonRefreshTokenProjection
{
    public required Guid PersonRefreshTokenId { get; init; }
    public required PersonId PersonId { get; init; }
    public required string RefreshToken { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
    public required DateTimeOffset? LogOutAt { get; init; }


    [MemberNotNullWhen(true, nameof(LogOutAt))]
    public bool HasLogOut => LogOutAt.HasValue;
}
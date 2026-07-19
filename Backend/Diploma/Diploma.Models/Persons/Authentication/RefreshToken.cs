using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Persons.Authentication;

public sealed record RefreshTokenRequest
{
    [Required]
    public required string RefreshToken { get; init; }
}

public abstract record RefreshTokenResult
{
    public sealed record Success : RefreshTokenResult
    {
        public required string JwtToken { get; init; }
        public required DateTimeOffset JwtTokenExpiresAt { get; init; }
        public required string RefreshToken { get; init; }
        public required DateTimeOffset RefreshTokenTokenExpiresAt { get; init; }
    }
    public sealed record Failure : RefreshTokenResult;
}
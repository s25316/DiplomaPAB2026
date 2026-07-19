using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Persons.Authentication;

public sealed record PersonLoginInRequest
{
    [Required]
    [EmailAddress]
    public required string Login { get; init; }

    [Required]
    public required string Password { get; init; }
}

public abstract record PersonLoginInResult
{
    public sealed record Success : PersonLoginInResult
    {
        public required string JwtToken { get; init; }
        public required DateTimeOffset JwtTokenExpiresAt { get; init; }
        public required string RefreshToken { get; init; }
        public required DateTimeOffset RefreshTokenTokenExpiresAt { get; init; }
    }
    public sealed record Failure : PersonLoginInResult;
}
using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Persons.Authentication;

public sealed record PersonLogOutRequest
{
    [Required]
    public required string RefreshToken { get; init; }
}

public abstract record PersonLogOutResult
{
    public sealed record Success : PersonLogOutResult;
    public sealed record Failure : PersonLogOutResult;
}
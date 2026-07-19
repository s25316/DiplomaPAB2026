using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Persons.Lifecycle;

public sealed record PersonActivateRequest
{
    [Required]
    public required string Code { get; init; }
}

public abstract record PersonActivateResult
{
    public sealed record Success : PersonActivateResult;
    public sealed record Failure : PersonActivateResult;
}
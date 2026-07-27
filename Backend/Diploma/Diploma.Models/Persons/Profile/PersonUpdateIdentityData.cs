using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Persons.Profile;

public sealed record PersonUpdateIdentityDataRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Surname { get; init; }
}

public abstract record PersonUpdateIdentityDataResult
{
    public sealed record Success : PersonUpdateIdentityDataResult;
    public sealed record Failure : PersonUpdateIdentityDataResult;
}
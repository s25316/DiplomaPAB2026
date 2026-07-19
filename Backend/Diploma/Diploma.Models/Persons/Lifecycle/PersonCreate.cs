using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Persons.Lifecycle;

public sealed record PersonCreateRequest
{
    [Required]
    [EmailAddress]
    public required string Login { get; init; }

    [Required]
    public required string Password { get; init; }
}

public abstract record PersonCreateResult
{
    public sealed record Success : PersonCreateResult
    {
        public required Guid OperationId { get; init; }
    }

    public abstract record Failure : PersonCreateResult
    {
        public sealed record LoginTaken(string Login) : Failure;
    }
}
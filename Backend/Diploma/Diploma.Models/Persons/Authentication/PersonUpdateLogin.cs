using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Persons.Authentication;

public sealed class PersonUpdateLoginRequest
{
    [Required]
    public required string Code { get; init; }

    [Required]
    [EmailAddress]
    public required string Login { get; init; }
}

public abstract record PersonUpdateLoginResult
{
    public sealed record Initiation : PersonUpdateLoginResult
    {
        public required Guid OperationId { get; init; }
    }

    public sealed record Success : PersonUpdateLoginResult;
    public abstract record Failure : PersonUpdateLoginResult
    {
        public sealed record General() : Failure;
        public sealed record LoginTaken(string Login) : Failure;
        public sealed record LoginExist() : Failure;
    }
}
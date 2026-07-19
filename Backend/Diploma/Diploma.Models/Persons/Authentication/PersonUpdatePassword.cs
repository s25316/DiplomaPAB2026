using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Persons.Authentication;

public sealed class PersonUpdatePasswordRecoveryInitiationRequest
{
    [Required]
    [EmailAddress]
    public required string Login { get; init; }
}

public sealed class PersonUpdatePasswordRecoveryRequest
{
    [Required]
    public required string Code { get; init; }

    [Required]
    public required string Password { get; init; }
}

public sealed class PersonUpdatePasswordRequest
{
    [Required]
    public required string OldPassword { get; init; }

    [Required]
    public required string NewPassword { get; init; }
}

public abstract record PersonUpdatePasswordResult
{
    public sealed record Initiation : PersonUpdatePasswordResult
    {
        public required Guid OperationId { get; init; }
    }

    public sealed record Success : PersonUpdatePasswordResult;
    public abstract record Failure : PersonUpdatePasswordResult
    {
        public sealed record General : Failure;
        public sealed record PasswordExist : Failure;
    }
}
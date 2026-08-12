using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.ProjectRoles;

public sealed record ProjectRoleCreateRequest
{
    [Required]
    public required string Title { get; init; }

    [Required]
    public required string Description { get; init; }
}

public abstract record ProjectRoleCreateResult
{
    public sealed record Success : ProjectRoleCreateResult;
    public abstract record Failure : ProjectRoleCreateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record OverMaxLimit(int MaxLimit) : Failure;
    };
}

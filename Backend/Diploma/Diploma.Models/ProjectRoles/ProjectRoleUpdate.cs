using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.ProjectRoles;

public sealed record ProjectRoleUpdateRequest
{
    [Required]
    public required string Title { get; init; }

    [Required]
    public required string Description { get; init; }

    [Required]
    public required bool IsAvailableRecruitment { get; init; }
}

public abstract record ProjectRoleUpdateResult
{
    public sealed record Success : ProjectRoleUpdateResult;
    public abstract record Failure : ProjectRoleUpdateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}
using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.ProjectRoleDisciplines;

public sealed class ProjectRoleDisciplineCreateRequest
{
    [Required]
    public required string DisciplineCode { get; init; }
}

public abstract record ProjectRoleDisciplineCreateResult
{
    public sealed record Success : ProjectRoleDisciplineCreateResult;
    public abstract record Failure : ProjectRoleDisciplineCreateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record OverMaxLimit(int MaxLimit) : Failure;
    };
}
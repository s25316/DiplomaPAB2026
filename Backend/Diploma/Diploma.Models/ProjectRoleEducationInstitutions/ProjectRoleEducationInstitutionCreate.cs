using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.ProjectRoleEducationInstitutions;

public abstract record ProjectRoleEducationInstitutionCreateRequest
{
    [Required]
    public required Guid EductioninstitutionId { get; init; }
}

public abstract record ProjectRoleEducationInstitutionCreateResult
{
    public sealed record Success : ProjectRoleEducationInstitutionCreateResult;
    public abstract record Failure : ProjectRoleEducationInstitutionCreateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record OverMaxLimit(int MaxLimit) : Failure;
    };
}
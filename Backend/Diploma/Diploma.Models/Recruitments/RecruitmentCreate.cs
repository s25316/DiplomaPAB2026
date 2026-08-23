using Microsoft.AspNetCore.Http;

namespace Diploma.Models.Recruitments;

public sealed class RecruitmentCreateRequest
{
    public required IList<Guid> ProjectRoleIds { get; set; }
    public required string Message { get; init; }
    public required IFormFile File { get; init; }
}

public abstract record RecruitmentCreateResult
{
    public sealed record Success : RecruitmentCreateResult;
    public abstract record Failure : RecruitmentCreateResult
    {
        public sealed record ProfileIsEmpty : Failure;
        public sealed record NotFound : Failure;
        public sealed record IsExistRecruitment : Failure;
        public sealed record NotSameProject : Failure;
        public sealed record EmptyProjectRoles : Failure;
        public sealed record NotAvailableRecruitment : Failure;
    };
}
using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Recruitments;

public sealed class RecruitmentUpdateRequest
{
    [Range(1, 3)]
    public required int StatusId { get; init; }
}

public abstract record RecruitmentUpdateResult
{
    public sealed record Success : RecruitmentUpdateResult;
    public abstract record Failure : RecruitmentUpdateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Conflict : Failure;
        public sealed record Forbidden : Failure;
    };
}
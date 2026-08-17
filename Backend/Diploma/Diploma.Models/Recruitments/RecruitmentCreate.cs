namespace Diploma.Models.Recruitments;

internal class RecruitmentCreate
{
    public required int StatusId { get; init; }
    public required string Message { get; init; }
}

public abstract record RecruitmentCreateResult
{
    public sealed record Success : RecruitmentCreateResult;
    public abstract record Failure : RecruitmentCreateResult
    {
        public sealed record ProfileIsEmpty : Failure;
        public sealed record NotFound : Failure;
        public sealed record Conflict : Failure;
    };
}
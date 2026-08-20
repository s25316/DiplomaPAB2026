using Microsoft.AspNetCore.Http;

namespace Diploma.Models.RecruitmentMessages;

public sealed class RecruitmentMessageCreateRequest
{
    public required string Message { get; init; }
    public required IFormFile? File { get; init; }
}

public abstract record RecruitmentMessageCreateResult
{
    public abstract record Failure : RecruitmentMessageCreateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
    public sealed record Success : RecruitmentMessageCreateResult;
}
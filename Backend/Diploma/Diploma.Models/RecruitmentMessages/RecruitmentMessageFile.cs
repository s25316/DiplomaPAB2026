using Microsoft.AspNetCore.Http;

namespace Diploma.Models.RecruitmentMessages;

public abstract record RecruitmentMessageFileResult
{
    public abstract record Failure : RecruitmentMessageFileResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(IFormFile File) : RecruitmentMessageFileResult;
}
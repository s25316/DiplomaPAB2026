using Diploma.Models.Shared;

namespace Diploma.Models.RecruitmentMessages;

public sealed class RecruitmentMessageQueryParameters : BaseQueryParameters;

public abstract record RecruitmentMessageQueryResult
{
    public abstract record Failure : RecruitmentMessageQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(Response<RecruitmentMessageDto> Response) : RecruitmentMessageQueryResult;
}

public class RecruitmentMessageDto
{
    public required Guid RecruitmentMessageId { get; init; }
    public required Guid PersonId { get; init; }
    public required string Message { get; init; }
    public required string? File { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
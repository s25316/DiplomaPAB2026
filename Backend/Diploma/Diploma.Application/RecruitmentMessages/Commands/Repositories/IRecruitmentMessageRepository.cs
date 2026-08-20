using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;

namespace Diploma.Application.RecruitmentMessages.Commands.Repositories;

public sealed record RecruitmentMessageInput
{
    public required RecruitmentId RecruitmentId { get; init; }
    public required PersonId PersonId { get; init; }
    public required string Message { get; init; }
    public required string? File { get; init; }
}

public sealed record RecruitmentMessageItem
{
    public required Guid MessageId { get; init; }
    public required RecruitmentId RecruitmentId { get; init; }
    public required ProjectId ProjectId { get; init; }
    public required PersonId PersonId { get; init; }
    public required string Message { get; init; }
    public required string? File { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}

public interface IRecruitmentMessageRepository
{
    Task<OptionalResult<RecruitmentMessageItem>> GetAsync(
        Guid messageId,
        CancellationToken cancellationToken = default);

    Task<Guid> CreateAsync(
        RecruitmentMessageInput input,
        CancellationToken cancellationToken = default);
}
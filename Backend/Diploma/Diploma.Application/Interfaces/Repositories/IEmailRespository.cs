using Diploma.Domain.ValueObjects;

namespace Diploma.Application.Interfaces.Repositories;

public sealed record EmailRespositoryInput
{
    public required Guid OperationId { get; init; }
    public required Email Email { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset? DeliveredAt { get; init; }
}

public interface IEmailRespository
{
    Task CreateAsync(
        EmailRespositoryInput input,
        CancellationToken cancellationToken = default);
}
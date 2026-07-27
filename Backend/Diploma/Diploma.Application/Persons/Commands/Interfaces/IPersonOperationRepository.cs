using Diploma.Domain.Base.Results;
using Diploma.Models.Shared;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using System.Diagnostics.CodeAnalysis;

namespace Diploma.Application.Persons.Commands.Interfaces;

public abstract record PersonOperationInput
{
    public sealed record Creating : PersonOperationInput
    {
        public required Guid PersonId { get; set; }
        public required string? Value { get; init; } = null;
        public required DateTimeOffset CreatedAt { get; init; }
        public required DateTimeOffset ExpiresAt { get; init; }
        public required Verification Verification { get; init; }
        public required PersonOperation PersonOperation { get; init; }
    }

    public sealed record Filters : PersonOperationInput
    {
        public required Guid PersonId { get; init; }
        public IEnumerable<PersonOperation> PersonOperations { get; init; } = [];

        public Order Order { get; init; } = Order.Descending;
        public QueryParametersPagination Pagination { get; init; } = new QueryParametersPagination();
    }
}

public sealed record PersonOperationId(Guid Value);
public sealed record PersonOperationItem
{
    public required PersonOperationId PersonOperationId { get; init; }
    public required Guid PersonId { get; set; }
    public required string? Value { get; init; } = null;
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
    public required DateTimeOffset? ActivatedAt { get; init; }
    public required Verification Verification { get; init; }
    public required PersonOperation PersonOperation { get; init; }


    [MemberNotNullWhen(true, nameof(ActivatedAt))]
    public bool HasActivated => ActivatedAt.HasValue;

    public bool HasExpired => DateTimeOffset.Now > ExpiresAt;
}

public interface IPersonOperationRepository
{
    Task<Response<PersonOperationItem>> GetAsync(
        PersonOperationInput.Filters filters,
        CancellationToken cancellationToken = default);

    Task<OptionalResult<PersonOperationItem>> GetAsync(
        Guid personOperationId,
        CancellationToken cancellationToken = default);

    Task<PersonOperationId> CreateAsync(
        PersonOperationInput.Creating input,
        CancellationToken cancellationToken = default);

    Task ActivateAsync(
        PersonOperationId operationId,
        CancellationToken cancellationToken = default);
}
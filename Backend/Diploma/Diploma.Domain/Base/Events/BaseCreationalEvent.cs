using Diploma.Domain.Base.Aggregates;

namespace Diploma.Domain.Base.Events;

public abstract record BaseCreationalEvent<TEntityId> : IDomainEvent
    where TEntityId : BaseEntityId
{
    public required Func<TEntityId?> GetTEntityId { get; init; }
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.Now;

    public TEntityId? EntityId => GetTEntityId();
    public bool HasEntityId => GetTEntityId() is not null;
}
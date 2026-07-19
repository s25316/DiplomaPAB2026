using Diploma.Domain.Base.Aggregates;

namespace Diploma.Domain.Base.Events;

public abstract record BaseEvent<TEntityId> : IDomainEvent
    where TEntityId : BaseEntityId
{
    public required TEntityId EntityId { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
}
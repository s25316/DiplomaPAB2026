using Diploma.Domain.Base.Events;
using System.Diagnostics.CodeAnalysis;

namespace Diploma.Domain.Base.Aggregates;

public abstract class BaseEntity<TEntityId>
    where TEntityId : BaseEntityId
{
    private TEntityId? id = null;
    private readonly List<IDomainEvent> events = [];


    public TEntityId? Id
    {
        get => id;
        set => id ??= value;
    }

    public bool HasEnabledEvents { get; set; } = true;
    public IReadOnlyList<IDomainEvent> Events => events;


    [MemberNotNullWhen(true, nameof(Id))]
    public bool HasId => Id != null;
    public bool HasEvents => Events.Any();


    protected void AddEvent<T>(T @event)
        where T : BaseEvent<TEntityId>
    {
        if (!HasEnabledEvents)
            return;

        events.Add(@event);
    }

    protected void AddCreationalEvent<T>(T @event)
        where T : BaseCreationalEvent<TEntityId>
    {
        if (!HasEnabledEvents)
            return;

        events.Add(@event);
    }

    public void ClearEvents() => events.Clear();
}
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ValueObjects;

namespace Diploma.Domain.Persons.Events.Lifecycle;

public record PersonRemovedEvent : BaseEvent<PersonId>
{
    public required Email Login { get; init; }
}
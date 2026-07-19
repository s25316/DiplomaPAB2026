using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ValueObjects;

namespace Diploma.Domain.Persons.Events.Authentication;

public record PersonUpdateLoginEvent : BaseEvent<PersonId>
{
    public required Email OldLogin { get; init; }
    public required Email NewLogin { get; init; }
}
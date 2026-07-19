using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.Persons.Events.Profile;

public record PersonUpdateProfileDataEvent : BaseEvent<PersonId>
{
    public required string? Title { get; init; }
    public required string? Summary { get; init; }
}
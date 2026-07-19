using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.Persons.Events.Profile;

public sealed record PersonUpdateIdentityDataEvent : BaseEvent<PersonId>
{
    public required string Name { get; init; }
    public required string Surname { get; init; }
}
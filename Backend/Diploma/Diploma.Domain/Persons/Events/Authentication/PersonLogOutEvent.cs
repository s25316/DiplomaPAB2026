using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ValueObjects;

namespace Diploma.Domain.Persons.Events.Authentication;

public sealed record PersonLogOutEvent : BaseEvent<PersonId>
{
    public required Email Login { get; init; }
    public required Guid PersonRefreshTokenId { get; init; }
}
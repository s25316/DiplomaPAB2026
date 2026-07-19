using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ValueObjects;

namespace Diploma.Domain.Persons.Events.Authentication;

public abstract record PersonLoginInUnSuccessReason
{
    public sealed record ProfileIsNotActivated() : PersonLoginInUnSuccessReason;
    public sealed record ProfileRemoved() : PersonLoginInUnSuccessReason;
    public sealed record InvalidPassword() : PersonLoginInUnSuccessReason;
}

public sealed record PersonLoginInUnSuccessEvent : BaseEvent<PersonId>
{
    public required Email Login { get; init; }
    public required PersonLoginInUnSuccessReason Reason { get; init; }
}
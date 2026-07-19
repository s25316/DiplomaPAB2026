namespace Diploma.Domain.Base.Aggregates;

public abstract record BaseEntityId;
public abstract record BaseEntityId<T> : BaseEntityId
    where T : notnull
{
    public required T Value { get; init; }
}
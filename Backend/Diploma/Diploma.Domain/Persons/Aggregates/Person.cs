using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Diploma.Domain.Persons.Aggregates;

public record PersonId : BaseEntityId<Guid>
{
    public static implicit operator Guid(PersonId value) => value.Value;
    public static implicit operator PersonId(Guid value) => new() { Value = value };
}
public partial class Person : BaseEntity<PersonId>
{
    public Email Login { get; protected set; } = null!;
    public string Password { get; protected set; } = null!;
    public string Salt { get; protected set; } = null!;

    public string? Name { get; protected set; } = null;
    public string? Surname { get; protected set; } = null;

    public string? Title { get; protected set; } = null;
    public string? Summary { get; protected set; } = null;

    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset? ActivatedAt { get; protected set; } = null;
    public DateTimeOffset? RemovedAt { get; protected set; } = null;
    public DateTimeOffset? AnonymizedAt { get; protected set; } = null;

    [MemberNotNullWhen(true, nameof(ActivatedAt))]
    public bool HasActive => HasActivated && !HasRemoved;

    [MemberNotNullWhen(true, nameof(ActivatedAt))]
    public bool HasActivated => ActivatedAt.HasValue;

    [MemberNotNullWhen(true, nameof(ActivatedAt), nameof(RemovedAt))]
    public bool HasRemoved => RemovedAt.HasValue;

    [MemberNotNullWhen(true, nameof(ActivatedAt), nameof(RemovedAt), nameof(AnonymizedAt))]
    public bool HasAnonymized => AnonymizedAt.HasValue && DateTimeOffset.Now > AnonymizedAt;

    [MemberNotNullWhen(true, nameof(Name), nameof(Surname))]
    public bool HasIdentityData => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname);
}
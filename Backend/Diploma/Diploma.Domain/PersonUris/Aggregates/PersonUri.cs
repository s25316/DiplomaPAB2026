using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.PersonUris.Aggregates;

public sealed record PersonUriId : BaseEntityId<Guid>
{
    public static implicit operator Guid(PersonUriId value) => value.Value;
    public static implicit operator PersonUriId(Guid value) => new() { Value = value };
}
public partial class PersonUri : BaseEntity<PersonUriId>
{
    public PersonId PersonId { get; protected set; } = null!;
    public PersonUriId LastSnapshotId { get; protected set; } = null!;
    public Uri Uri { get; protected set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;



    public static PersonUri Create(
        PersonId personId,
        Uri uri,
        string name,
        string description)
    {
        var personUri = new PersonUri();

        personUri.PersonId = personId;
        personUri.Uri = uri;
        personUri.Name = name;
        personUri.Description = description;

        return personUri;
    }
}
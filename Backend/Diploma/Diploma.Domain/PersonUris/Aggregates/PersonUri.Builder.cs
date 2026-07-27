using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.PersonUris.Aggregates;

public partial class PersonUri : BaseEntity<PersonUriId>
{
    public class Builder : BaseEntityBulder<PersonUri, PersonUriId>
    {
        public Builder WithId(PersonUriId item)
        {
            With(i => i.Id = item);
            return this;
        }
        public Builder WithLastSnapshotId(PersonUriId item)
        {
            With(i => i.LastSnapshotId = item);
            return this;
        }

        public Builder WithPersonId(PersonId item)
        {
            With(i => i.PersonId = item);
            return this;
        }

        public Builder WithUri(Uri item)
        {
            With(i => i.Uri = item);
            return this;
        }

        public Builder WithName(string item)
        {
            With(i => i.Name = item);
            return this;
        }

        public Builder WithDescription(string item)
        {
            With(i => i.Description = item);
            return this;
        }
    }
}
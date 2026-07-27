using Base.Models.ValueObjects.Regony;
using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.PersonEmployments.Aggregates;

public partial class PersonEmployment : BaseEntity<PersonEmploymentId>
{
    public class Builder : BaseEntityBulder<PersonEmployment, PersonEmploymentId>
    {
        public Builder WithId(PersonEmploymentId value)
        {
            With(i => i.Id = value);
            return this;
        }

        public Builder WithLastSnapshotId(PersonEmploymentId value)
        {
            With(i => i.LastSnapshotId = value);
            return this;
        }

        public Builder WithPersonId(PersonId value)
        {
            With(i => i.PersonId = value);
            return this;
        }

        public Builder WithRegon(Regon value)
        {
            With(i => i.Regon = value);
            return this;
        }

        public Builder WithPosition(string value)
        {
            With(i => i.Position = value);
            return this;
        }

        public Builder WithDescription(string value)
        {
            With(i => i.Description = value);
            return this;
        }

        public Builder WithFrom(DateOnly value)
        {
            With(i => i.From = value);
            return this;
        }

        public Builder WithTo(DateOnly? value)
        {
            With(i => i.To = value);
            return this;
        }
    }
}
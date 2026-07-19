using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.ValueObjects;

namespace Diploma.Domain.Persons.Aggregates;

public partial class Person : BaseEntity<PersonId>
{
    public class Builder : BaseEntityBulder<Person, PersonId>
    {
        public Builder WithId(PersonId value)
        {
            With(i => i.Id = value);
            return this;
        }

        public Builder WithLogin(Email value)
        {
            With(i => i.Login = value);
            return this;
        }

        public Builder WithPassword(string value)
        {
            With(i => i.Password = value);
            return this;
        }

        public Builder WithSalt(string value)
        {
            With(i => i.Salt = value);
            return this;
        }

        public Builder WithName(string? value)
        {
            With(i => i.Name = value);
            return this;
        }

        public Builder WithSurname(string? value)
        {
            With(i => i.Surname = value);
            return this;
        }

        public Builder WithTitle(string? value)
        {
            With(i => i.Title = value);
            return this;
        }

        public Builder WithSummary(string? value)
        {
            With(i => i.Summary = value);
            return this;
        }

        public Builder WithCreatedAt(DateTimeOffset value)
        {
            With(i => i.CreatedAt = value);
            return this;
        }

        public Builder WithActivatedAt(DateTimeOffset? value)
        {
            With(i => i.ActivatedAt = value);
            return this;
        }

        public Builder WithRemovedAt(DateTimeOffset? value)
        {
            With(i => i.RemovedAt = value);
            return this;
        }

        public Builder WithAnonymizedAt(DateTimeOffset? value)
        {
            With(i => i.AnonymizedAt = value);
            return this;
        }
    }
}
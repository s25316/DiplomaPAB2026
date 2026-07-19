using Diploma.Domain.Base.Results;
using Diploma.Domain.ValueObjects;

namespace Diploma.Domain.Persons.Aggregates;

public abstract record PersonResult
{
    public abstract record Creating : PersonResult
    {
        public sealed record Success() : Creating;
        public abstract record Failure() : Creating
        {
            public sealed record LoginTaken(Email Login) : Failure;
        }
    }
    public abstract record Updating : PersonResult
    {
        public sealed record Success() : Updating;
        public abstract record Failure() : Updating
        {
            public sealed record LoginTaken(Email Login) : Failure;
            public sealed record NotFound() : Failure;
        }
    }
}

public interface IPersonRepository
{
    Task<OptionalResult<Person>> GetAsync(Email login, CancellationToken cancellationToken = default);
    Task<OptionalResult<Person>> GetAsync(PersonId id, CancellationToken cancellationToken = default);
    Task<PersonResult.Creating> CreateAsync(Person entity, CancellationToken cancellationToken = default);
    Task<PersonResult.Updating> UpdateAsync(Person entity, CancellationToken cancellationToken = default);
}
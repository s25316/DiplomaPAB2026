using Diploma.Database;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using DatabasePerson = Diploma.Database.Models.Persons.Person;

namespace Diploma.Infrastructure.Persons;

public class PersonRepository(DiplomaDbContext context) : IPersonRepository
{
    private sealed record DatabaseData
    {
        public required DatabasePerson Entity { get; init; }
        public required PersonIdentity? Identity { get; init; }
        public required PersonProfile? Profile { get; init; }
    }

    private static readonly OptionalResult<Person> NotFound = OptionalResult.NotFound<Person>();


    public async Task<OptionalResult<Person>> GetAsync(Email login, CancellationToken cancellationToken = default)
    {
        var databaseData = await context
            .People
            .AsNoTracking()
            .Where(i => i.Login == login.Value)
            .Select(e => new DatabaseData
            {
                Entity = e,
                Identity = context
                    .PersonIdentities
                    .Include(i => i.PersonEvent)
                    .OrderByDescending(i => i.PersonEvent.CreatedAt)
                    .FirstOrDefault(i => i.PersonEvent.PersonId == e.PersonId),
                Profile = context
                    .PersonProfiles
                    .Include(i => i.PersonEvent)
                    .OrderByDescending(i => i.PersonEvent.CreatedAt)
                    .FirstOrDefault(i => i.PersonEvent.PersonId == e.PersonId),
            }).FirstOrDefaultAsync(cancellationToken);

        return Map(databaseData);
    }

    public async Task<OptionalResult<Person>> GetAsync(PersonId id, CancellationToken cancellationToken = default)
    {
        var databaseData = await context
            .People
            .AsNoTracking()
            .Where(i => i.PersonId == id.Value)
            .Select(i => new DatabaseData
            {
                Entity = i,
                Identity = context
                    .PersonIdentities
                    .Include(i => i.PersonEvent)
                    .OrderByDescending(i => i.PersonEvent.CreatedAt)
                    .FirstOrDefault(i => i.PersonEvent.PersonId == id.Value),
                Profile = context
                    .PersonProfiles
                    .Include(i => i.PersonEvent)
                    .OrderByDescending(i => i.PersonEvent.CreatedAt)
                    .FirstOrDefault(i => i.PersonEvent.PersonId == id.Value),
            }).FirstOrDefaultAsync(cancellationToken);

        return Map(databaseData);
    }

    private static OptionalResult<Person> Map(DatabaseData? databaseData)
    {

        if (databaseData is null)
            return NotFound;

        var builder = new Person.Builder()
            .WithId(databaseData.Entity.PersonId)
            .WithCreatedAt(databaseData.Entity.CreatedAt)
            .WithActivatedAt(databaseData.Entity.ActivatedAt)
            .WithRemovedAt(databaseData.Entity.RemovedAt)
            .WithAnonymizedAt(databaseData.Entity.RemovedAt);

        if (!string.IsNullOrWhiteSpace(databaseData.Entity.Login))
            builder.WithLogin(new Email(databaseData.Entity.Login));

        if (!string.IsNullOrWhiteSpace(databaseData.Entity.Password))
            builder.WithPassword(databaseData.Entity.Password);

        if (!string.IsNullOrWhiteSpace(databaseData.Entity.Salt))
            builder.WithSalt(databaseData.Entity.Salt);

        builder
            .WithName(databaseData.Identity?.Name)
            .WithSurname(databaseData.Identity?.Surname)
            .WithTitle(databaseData.Profile?.Title)
            .WithSummary(databaseData.Profile?.Summary);

        return OptionalResult.Success(builder.Build());
    }


    public async Task<PersonResult.Creating> CreateAsync(
        Person entity,
        CancellationToken cancellationToken = default)
    {
        var login = entity.Login.Value;

        var personWithSameLogin = await context
            .People
            .CountAsync(i => i.Login == login, cancellationToken);

        if (personWithSameLogin > 0)
        {
            return new PersonResult.Creating.Failure.LoginTaken(entity.Login);
        }

        var person = new DatabasePerson
        {
            Login = login,
            Password = entity.Password,
            Salt = entity.Salt,
            CreatedAt = entity.CreatedAt,
        };
        await context.People.AddAsync(person, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        entity.Id = new PersonId
        {
            Value = person.PersonId,
        };
        return new PersonResult.Creating.Success();
    }

    public async Task<PersonResult.Updating> UpdateAsync(Person entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity.Id);
        var databasePerson = await context
            .People
            .FirstOrDefaultAsync(i => i.PersonId == entity.Id.Value, cancellationToken);

        if (databasePerson == null)
            return new PersonResult.Updating.Failure.NotFound();

        if (entity.Login.Value != databasePerson.Login)
        {
            var sameLoginCount = await context
                .People
                .Where(i => i.PersonId != databasePerson.PersonId)
                .Where(i => i.Login == entity.Login.Value)
                .CountAsync(cancellationToken);

            if (sameLoginCount > 0)
                return new PersonResult.Updating.Failure.LoginTaken(entity.Login);
        }

        databasePerson.Login = entity.Login.Value;
        databasePerson.Password = entity.Password;
        databasePerson.Salt = entity.Salt;
        databasePerson.CreatedAt = entity.CreatedAt;
        databasePerson.ActivatedAt = entity.ActivatedAt;
        databasePerson.RemovedAt = entity.RemovedAt;
        databasePerson.AnonymizedAt = entity.AnonymizedAt;

        await context.SaveChangesAsync(cancellationToken);
        return new PersonResult.Updating.Success();
    }
}
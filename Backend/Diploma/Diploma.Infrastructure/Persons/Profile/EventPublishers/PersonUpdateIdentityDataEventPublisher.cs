using Diploma.Database;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Profile;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.Profile.EventPublishers;

public class PersonUpdateIdentityDataEventPublisher(DiplomaDbContext context) : IEventPublisher<PersonUpdateIdentityDataEvent>
{
    public async Task PublishAsync(PersonUpdateIdentityDataEvent @event, CancellationToken cancellationToken = default)
    {
        var databaseEvent = new DatabasePersonEvent
        {
            CreatedAt = @event.CreatedAt,
            PersonEventTypeId = SharedPersonEvent.UpdateIdentityData.Id,
            PersonId = @event.EntityId.Value,
        };
        var personIdentity = new PersonIdentity
        {
            PersonEvent = databaseEvent,
            Name = @event.Name,
            Surname = @event.Surname,
        };

        await context.PersonEvents.AddAsync(databaseEvent, cancellationToken);
        await context.PersonIdentities.AddAsync(personIdentity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
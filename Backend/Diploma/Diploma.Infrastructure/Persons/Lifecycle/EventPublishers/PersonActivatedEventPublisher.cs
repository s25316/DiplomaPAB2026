using Diploma.Database;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Lifecycle;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.Lifecycle.EventPublishers;

public class PersonActivatedEventPublisher(DiplomaDbContext context) : IEventPublisher<PersonActivatedEvent>
{
    public async Task PublishAsync(PersonActivatedEvent @event, CancellationToken cancellationToken = default)
    {
        var databaseEvent = new DatabasePersonEvent
        {
            CreatedAt = @event.CreatedAt,
            PersonEventTypeId = SharedPersonEvent.Activated.Id,
            PersonId = @event.EntityId.Value,
        };

        await context.PersonEvents.AddAsync(databaseEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
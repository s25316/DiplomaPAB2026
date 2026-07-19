using Diploma.Database;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Lifecycle;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.Lifecycle.EventPublishers;

public class PersonRemovedEventPublisher(DiplomaDbContext context) : IEventPublisher<PersonRemovedEvent>
{
    public async Task PublishAsync(PersonRemovedEvent @event, CancellationToken cancellationToken)
    {
        var databaseEvent = new DatabasePersonEvent
        {
            CreatedAt = @event.CreatedAt,
            PersonEventTypeId = SharedPersonEvent.Removed.Id,
            PersonId = @event.EntityId.Value,
        };

        await context.PersonEvents.AddAsync(databaseEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
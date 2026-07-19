using Diploma.Database;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Lifecycle;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.Lifecycle.EventPublishers;

internal class PersonAnonymizedEventPublisher(DiplomaDbContext context) : IEventPublisher<PersonAnonymizedEvent>
{
    public async Task PublishAsync(PersonAnonymizedEvent @event, CancellationToken cancellationToken = default)
    {
        var databaseEvent = new DatabasePersonEvent
        {
            CreatedAt = @event.CreatedAt,
            PersonEventTypeId = SharedPersonEvent.Anonymized.Id,
            PersonId = @event.EntityId.Value,
        };

        await context.PersonEvents.AddAsync(databaseEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
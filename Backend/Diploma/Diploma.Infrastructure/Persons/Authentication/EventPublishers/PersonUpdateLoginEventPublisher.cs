using Diploma.Database;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Authentication;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.Authentication.EventPublishers;

public class PersonUpdateLoginEventPublisher(
    DiplomaDbContext context
    ) : IEventPublisher<PersonUpdateLoginEvent>
{
    public async Task PublishAsync(PersonUpdateLoginEvent @event, CancellationToken cancellationToken = default)
    {
        var databaseEvent = new DatabasePersonEvent
        {
            CreatedAt = @event.CreatedAt,
            PersonEventTypeId = SharedPersonEvent.UpdateLogin.Id,
            PersonId = @event.EntityId.Value,
        };

        await context.PersonEvents.AddAsync(databaseEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
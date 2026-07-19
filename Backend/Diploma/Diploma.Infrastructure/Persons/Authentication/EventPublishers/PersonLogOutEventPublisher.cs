using Diploma.Database;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Authentication;
using Microsoft.EntityFrameworkCore;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.Authentication.EventPublishers;

public class PersonLogOutEventPublisher(
    DiplomaDbContext context
    ) : IEventPublisher<PersonLogOutEvent>
{
    public async Task PublishAsync(PersonLogOutEvent @event, CancellationToken cancellationToken = default)
    {
        var databaseEvent = new DatabasePersonEvent
        {
            CreatedAt = @event.CreatedAt,
            PersonEventTypeId = SharedPersonEvent.LogOut.Id,
            PersonId = @event.EntityId.Value,
        };

        var refreshToken = await context
            .PersonRefreshTokens
            .FirstAsync(
                i => i.PersonRefreshTokenId == @event.PersonRefreshTokenId,
                cancellationToken
            );

        refreshToken.LogOutEvent = databaseEvent;

        await context.PersonEvents.AddAsync(databaseEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
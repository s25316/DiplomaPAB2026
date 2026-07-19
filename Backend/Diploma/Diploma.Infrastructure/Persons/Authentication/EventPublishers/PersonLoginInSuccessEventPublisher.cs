using Diploma.Database;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Authentication;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.Authentication.EventPublishers;

public class PersonLoginInSuccessEventPublisher(
    DiplomaDbContext context
    ) : IEventPublisher<PersonLoginInSuccessEvent>
{
    public async Task PublishAsync(PersonLoginInSuccessEvent @event, CancellationToken cancellationToken = default)
    {
        var databaseEvent = new DatabasePersonEvent
        {
            CreatedAt = @event.CreatedAt,
            PersonEventTypeId = SharedPersonEvent.LogInSucess.Id,
            PersonId = @event.EntityId.Value,
        };

        var refreshToken = new PersonRefreshToken
        {
            LoginInEvent = databaseEvent,
            RefreshToken = @event.RefreshToken,
            ExpiresAt = @event.ExpiresAt,
        };

        await context.PersonEvents.AddAsync(databaseEvent, cancellationToken);
        await context.PersonRefreshTokens.AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
using Diploma.Database;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Profile;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.Profile.EventPublishers;

public class PersonUpdateProfileDataEventPublisher(DiplomaDbContext context) : IEventPublisher<PersonUpdateProfileDataEvent>
{
    public async Task PublishAsync(PersonUpdateProfileDataEvent @event, CancellationToken cancellationToken = default)
    {
        var databaseEvent = new DatabasePersonEvent
        {
            CreatedAt = @event.CreatedAt,
            PersonEventTypeId = SharedPersonEvent.UpdateProfileData.Id,
            PersonId = @event.EntityId.Value,
        };
        var personProfile = new PersonProfile
        {
            PersonEvent = databaseEvent,
            Title = @event.Title,
            Summary = @event.Summary,
        };

        await context.PersonEvents.AddAsync(databaseEvent, cancellationToken);
        await context.PersonProfiles.AddAsync(personProfile, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
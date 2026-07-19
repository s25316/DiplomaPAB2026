using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Profile;
using MediatR;

namespace Diploma.Application.Persons.Profile.Events;

public class PersonUpdateIdentityDataEventHandler(
    IEventPublisher<PersonUpdateIdentityDataEvent> publisher
    ) : INotificationHandler<PersonUpdateIdentityDataEvent>
{
    public async Task Handle(PersonUpdateIdentityDataEvent notification, CancellationToken cancellationToken)
    {
        await publisher.PublishAsync(notification, cancellationToken);
    }
}
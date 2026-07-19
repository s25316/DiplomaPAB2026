using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Profile;
using MediatR;

namespace Diploma.Application.Persons.Profile.Events;

public class PersonUpdateProfileDataEventHandler(
    IEventPublisher<PersonUpdateProfileDataEvent> publisher
    ) : INotificationHandler<PersonUpdateProfileDataEvent>
{
    public async Task Handle(PersonUpdateProfileDataEvent notification, CancellationToken cancellationToken)
    {
        await publisher.PublishAsync(notification, cancellationToken);
    }
}
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Lifecycle;
using MediatR;

namespace Diploma.Application.Persons.Commands.Lifecycle.Events;

public class PersonCretedEventHandler(
    IEventPublisher<PersonCretedEvent> publisher
    ) : INotificationHandler<PersonCretedEvent>
{
    public async Task Handle(PersonCretedEvent notification, CancellationToken cancellationToken)
    {
        await publisher.PublishAsync(notification, cancellationToken);
    }
}
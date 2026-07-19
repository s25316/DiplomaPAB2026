using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Lifecycle;
using MediatR;

namespace Diploma.Application.Persons.Lifecycle.Events;

public class PersonRemovedEventHandler(
    IEventPublisher<PersonRemovedEvent> publisher
    ) : INotificationHandler<PersonRemovedEvent>
{
    public async Task Handle(PersonRemovedEvent notification, CancellationToken cancellationToken)
    {
        await publisher.PublishAsync(notification, cancellationToken);
    }
}
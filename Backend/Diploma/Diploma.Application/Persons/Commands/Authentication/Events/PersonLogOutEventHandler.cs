using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Authentication;
using MediatR;

namespace Diploma.Application.Persons.Commands.Authentication.Events;

public class PersonLogOutEventHandler(
    IEventPublisher<PersonLogOutEvent> publisher
    ) : INotificationHandler<PersonLogOutEvent>
{
    public async Task Handle(PersonLogOutEvent notification, CancellationToken cancellationToken)
    {
        await publisher.PublishAsync(notification, cancellationToken);
    }
}
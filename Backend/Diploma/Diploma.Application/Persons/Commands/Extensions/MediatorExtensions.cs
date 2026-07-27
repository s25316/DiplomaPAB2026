using Diploma.Domain.Persons.Aggregates;
using MediatR;

namespace Diploma.Application.Persons.Commands.Extensions;

public static class MediatorExtensions
{
    public static async Task PublishEventsAsync(
        this IMediator mediator,
        Person person,
        CancellationToken cancellationToken = default)
    {
        foreach (var @event in person.Events)
        {
            await mediator.Publish(@event, cancellationToken);
        }
        person.ClearEvents();
    }
}
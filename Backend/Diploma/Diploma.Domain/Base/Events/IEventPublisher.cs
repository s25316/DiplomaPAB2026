namespace Diploma.Domain.Base.Events;

public interface IEventPublisher<in TEvent>
    where TEvent : class, IDomainEvent
{
    Task PublishAsync(
        TEvent @event,
        CancellationToken cancellationToken = default);
}
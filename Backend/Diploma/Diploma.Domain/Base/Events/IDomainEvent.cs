using MediatR;

namespace Diploma.Domain.Base.Events;

public interface IDomainEvent : INotification
{
    DateTimeOffset CreatedAt { get; }
}
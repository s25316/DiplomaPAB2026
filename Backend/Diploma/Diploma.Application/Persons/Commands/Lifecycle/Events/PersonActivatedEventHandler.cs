using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;
using Diploma.Application.Services;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Lifecycle;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Commands.Lifecycle.Events;

public class PersonActivatedEventHandler(
    IEmailService emailService,
    IEventPublisher<PersonActivatedEvent> publisher,
    IPersonOperationRepository operationRepository,
    IPersonActivatedMessageGenerator messageGenerator
    ) : INotificationHandler<PersonActivatedEvent>
{
    public async Task Handle(PersonActivatedEvent notification, CancellationToken cancellationToken)
    {
        var operationId = await CreateOperationAsync(notification, cancellationToken);
        var message = PrepareMessage();
        await SendMessageAsync(operationId, notification, message, cancellationToken);
        await publisher.PublishAsync(notification, cancellationToken);
    }

    private async Task<PersonOperationId> CreateOperationAsync(
        PersonActivatedEvent notification,
        CancellationToken cancellationToken
    ) => await operationRepository.CreateAsync(new PersonOperationInput.Creating
    {
        PersonId = notification.EntityId.Value,
        Value = null,
        CreatedAt = notification.CreatedAt,
        ExpiresAt = notification.CreatedAt,
        Verification = Verification.None,
        PersonOperation = PersonOperation.ProfileActivated,
    }, cancellationToken);

    private MessageResult PrepareMessage() => messageGenerator.Generate(new PersonActivatedMessageInput());

    public async Task SendMessageAsync(
        PersonOperationId operationId,
        PersonActivatedEvent notification,
        MessageResult message,
        CancellationToken cancellationToken
    ) => await emailService.CreateAndSendAsync(new EmailServiceInput
    {
        PersonOperationId = operationId,
        Email = notification.Login,
        Subject = message.Subject,
        Body = message.Body,
        CreatedAt = notification.CreatedAt,
    }, cancellationToken);
}
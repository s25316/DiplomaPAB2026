using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Interfaces;
using Diploma.Application.Persons.Lifecycle.MessageGenerators;
using Diploma.Application.Services;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Lifecycle;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Lifecycle.Events;

public class PersonRestoredEventHandler(
    IEmailService emailService,
    IEventPublisher<PersonRestoredEvent> publisher,
    IPersonOperationRepository operationRepository,
    IPersonRestoreMessageGenerator messageGenerator
    ) : INotificationHandler<PersonRestoredEvent>
{
    public async Task Handle(PersonRestoredEvent notification, CancellationToken cancellationToken)
    {
        var operationId = await CreateOperationAsync(notification, cancellationToken);
        var message = PrepareMessage();
        await SendMessageAsync(operationId, notification, message, cancellationToken);
        await publisher.PublishAsync(notification, cancellationToken);
    }

    private async Task<PersonOperationId> CreateOperationAsync(
        PersonRestoredEvent notification,
        CancellationToken cancellationToken
    ) => await operationRepository.CreateAsync(new PersonOperationInput.Creating
    {
        PersonId = notification.EntityId.Value,
        Value = null,
        CreatedAt = notification.CreatedAt,
        ExpiresAt = notification.CreatedAt,
        Verification = Verification.None,
        PersonOperation = PersonOperation.ProfileRestored,
    }, cancellationToken);

    private MessageResult PrepareMessage() => messageGenerator.Generate(new());

    public async Task SendMessageAsync(
        PersonOperationId operationId,
        PersonRestoredEvent notification,
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
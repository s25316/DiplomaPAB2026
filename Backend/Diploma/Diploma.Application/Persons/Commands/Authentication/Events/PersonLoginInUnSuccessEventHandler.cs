using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Authentication.MessageGenerators;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Application.Services;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Persons.Events.Authentication;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Commands.Authentication.Events;

public class PersonLoginInUnSuccessEventHandler(
    IEmailService emailService,
    IEventPublisher<PersonLoginInUnSuccessEvent> publisher,
    IPersonOperationRepository operationRepository,
    IPersonLoginInUnSuccessMessageGenerator messageGenerator
    ) : INotificationHandler<PersonLoginInUnSuccessEvent>
{
    public async Task Handle(PersonLoginInUnSuccessEvent notification, CancellationToken cancellationToken)
    {
        var operationId = await CreateOperationAsync(notification, cancellationToken);
        var message = PrepareMessage(notification.Reason);
        await SendMessageAsync(operationId, notification, message, cancellationToken);
        await publisher.PublishAsync(notification, cancellationToken);
    }

    private async Task<PersonOperationId> CreateOperationAsync(
        PersonLoginInUnSuccessEvent notification,
        CancellationToken cancellationToken
    ) => await operationRepository.CreateAsync(new PersonOperationInput.Creating
    {
        PersonId = notification.EntityId.Value,
        Value = null,
        CreatedAt = notification.CreatedAt,
        ExpiresAt = notification.CreatedAt,
        Verification = Verification.None,
        PersonOperation = PersonOperation.LogInUnsucess,
    }, cancellationToken);

    private MessageResult PrepareMessage(PersonLoginInUnSuccessReason reason) => messageGenerator.Generate(new PersonLoginInUnSuccessMessageInput
    {
        Reason = reason,
    });

    public async Task SendMessageAsync(
        PersonOperationId operationId,
        PersonLoginInUnSuccessEvent notification,
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
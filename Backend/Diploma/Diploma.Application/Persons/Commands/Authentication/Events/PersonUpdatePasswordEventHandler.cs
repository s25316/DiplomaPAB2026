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

public class PersonUpdatePasswordEventHandler(
    IEmailService emailService,
    IEventPublisher<PersonUpdatePasswordEvent> publisher,
    IPersonOperationRepository operationRepository,
    IPersonUpdatedPasswordMessageGenerator messageGenerator
    ) : INotificationHandler<PersonUpdatePasswordEvent>
{
    public async Task Handle(PersonUpdatePasswordEvent notification, CancellationToken cancellationToken)
    {
        var operationId = await CreateOperationAsync(notification, cancellationToken);
        var message = PrepareMessage();
        await SendMessageAsync(operationId, notification, message, cancellationToken);
        await publisher.PublishAsync(notification, cancellationToken);
    }

    private async Task<PersonOperationId> CreateOperationAsync(
        PersonUpdatePasswordEvent notification,
        CancellationToken cancellationToken
    ) => await operationRepository.CreateAsync(new PersonOperationInput.Creating
    {
        PersonId = notification.EntityId.Value,
        Value = null,
        CreatedAt = notification.CreatedAt,
        ExpiresAt = notification.CreatedAt,
        Verification = Verification.None,
        PersonOperation = PersonOperation.UpdatedPassword,
    }, cancellationToken);

    private MessageResult PrepareMessage() => messageGenerator.Generate(new());

    public async Task SendMessageAsync(
        PersonOperationId operationId,
        PersonUpdatePasswordEvent notification,
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
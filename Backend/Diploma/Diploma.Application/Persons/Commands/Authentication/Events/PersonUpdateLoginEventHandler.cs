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

public class PersonUpdateLoginEventHandler(
    IEmailService emailService,
    IEventPublisher<PersonUpdateLoginEvent> publisher,
    IPersonOperationRepository operationRepository,
    IPersonUpdatedLoginMessageGenerator messageGenerator
    ) : INotificationHandler<PersonUpdateLoginEvent>
{
    public async Task Handle(PersonUpdateLoginEvent notification, CancellationToken cancellationToken)
    {
        var operationId = await CreateOperationAsync(notification, cancellationToken);
        var message = PrepareMessage(notification);
        await SendMessageAsync(operationId, notification, message, cancellationToken);
        await publisher.PublishAsync(notification, cancellationToken);
    }

    private async Task<PersonOperationId> CreateOperationAsync(
        PersonUpdateLoginEvent notification,
        CancellationToken cancellationToken
    ) => await operationRepository.CreateAsync(new PersonOperationInput.Creating
    {
        PersonId = notification.EntityId.Value,
        Value = null,
        CreatedAt = notification.CreatedAt,
        ExpiresAt = notification.CreatedAt,
        Verification = Verification.None,
        PersonOperation = PersonOperation.UpdatedLogin,
    }, cancellationToken);

    private MessageResult PrepareMessage(PersonUpdateLoginEvent notification) => messageGenerator.Generate(new()
    {
        OldLogin = notification.OldLogin.Value,
        NewLogin = notification.NewLogin.Value,
    });

    public async Task SendMessageAsync(
        PersonOperationId operationId,
        PersonUpdateLoginEvent notification,
        MessageResult message,
        CancellationToken cancellationToken)
    {
        await emailService.CreateAndSendAsync(new EmailServiceInput
        {
            PersonOperationId = operationId,
            Email = notification.OldLogin,
            Subject = message.Subject,
            Body = message.Body,
            CreatedAt = notification.CreatedAt,
        }, cancellationToken);

        await emailService.CreateAndSendAsync(new EmailServiceInput
        {
            PersonOperationId = operationId,
            Email = notification.NewLogin,
            Subject = message.Subject,
            Body = message.Body,
            CreatedAt = notification.CreatedAt,
        }, cancellationToken);
    }
}
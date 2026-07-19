using Diploma.Application.Interfaces.Database;
using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Authentication.MessageGenerators;
using Diploma.Application.Persons.Interfaces;
using Diploma.Application.Services;
using Diploma.Application.Services.Generators;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ValueObjects;
using Diploma.Models.Persons.Authentication;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Authentication.UseCases;

public class PersonUpdatePasswordRecoveryInitiationHandler(
    IUnitOfWorkFactory unitOfWorkFactory,

    ICodeGenerator codeGenerator,
    IPersonUpdatePasswordRecoveryInitiationMessageGenerator messageGenerator,

    IEmailService emailService,
    IPersonRepository repository,
    IPersonOperationRepository operationRepository
    ) : IRequestHandler<PersonUpdatePasswordRecoveryInitiationHandler.Request, PersonUpdatePasswordResult>
{
    public sealed record Request : IRequest<PersonUpdatePasswordResult>
    {
        public required PersonUpdatePasswordRecoveryInitiationRequest Model { get; init; }
    }


    private const int CODE_VALID_IN_MITUES = 5;
    private static readonly PersonUpdatePasswordResult.Failure.General Failure = new();


    public async Task<PersonUpdatePasswordResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await repository.GetAsync(new Email(request.Model.Login), cancellationToken);

        if (!personResult.HasValue)
            return Failure;

        var person = personResult.Value;

        if (!person.HasActive)
            return Failure;

        var code = codeGenerator.Generate();
        var operationId = await CreateOperationAsync(code, person, cancellationToken);
        var message = PrepareMessage(code, operationId);

        await SendMessageAsync(person, message, operationId, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new PersonUpdatePasswordResult.Initiation
        {
            OperationId = operationId.Value,
        };
    }

    private async Task<PersonOperationId> CreateOperationAsync(
        string code,
        Person person,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(person.Id);
        var now = DateTimeOffset.Now;
        return await operationRepository.CreateAsync(new PersonOperationInput.Creating
        {
            PersonId = person.Id.Value,
            Value = code,
            CreatedAt = now,
            ExpiresAt = now.AddMinutes(CODE_VALID_IN_MITUES),
            Verification = Verification.Code,
            PersonOperation = PersonOperation.InitiateUpdatingPassword,
        }, cancellationToken);
    }

    private MessageResult PrepareMessage(
        string code,
        PersonOperationId operationId
    ) => messageGenerator.Generate(new()
    {
        Code = code,
        OperationId = operationId.Value,
    });

    private async Task SendMessageAsync(
        Person person,
        MessageResult message,
        PersonOperationId operationId,
        CancellationToken cancellationToken
    ) => await emailService.CreateAndSendAsync(new EmailServiceInput
    {
        PersonOperationId = operationId,
        Email = person.Login,
        Subject = message.Subject,
        Body = message.Body,
    }, cancellationToken);
}
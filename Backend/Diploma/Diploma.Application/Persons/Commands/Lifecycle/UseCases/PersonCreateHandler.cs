using Diploma.Application.Interfaces.Database;
using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Interfaces.Security;
using Diploma.Application.Persons.Commands.Extensions;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;
using Diploma.Application.Services;
using Diploma.Application.Services.Generators;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ValueObjects;
using Diploma.Models.Persons.Lifecycle;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Commands.Lifecycle.UseCases;

public class PersonCreateHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,

    ICodeGenerator codeGenerator,
    IPersonCreateAndActivationMessageGenerator messageGenerator,
    IPasswordHasherService passwordHasherService,

    IEmailService emailService,
    IPersonRepository repository,
    IPersonOperationRepository operationRepository
    ) : IRequestHandler<PersonCreateHandler.Request, PersonCreateResult>
{
    public sealed record Request : IRequest<PersonCreateResult>
    {
        public required PersonCreateRequest Model { get; init; }
    }


    public async Task<PersonCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);

        var person = PreparePerson(request);
        var creatingResult = await repository.CreateAsync(person, cancellationToken);

        switch (creatingResult)
        {
            case PersonResult.Creating.Failure.LoginTaken loginTaken:
                return new PersonCreateResult.Failure.LoginTaken(loginTaken.Login.Value);

            case PersonResult.Creating.Success:
                break;

            default:
                throw new NotImplementedException($"Unknown type of {nameof(PersonCreateResult)}: {creatingResult.GetType()}");
        }

        await mediator.PublishEventsAsync(person, cancellationToken);
        var operationId = await CreateOperationAndGetOperationIdAsync(person, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new PersonCreateResult.Success
        {
            OperationId = operationId.Value,
        };
    }

    private Person PreparePerson(Request request)
    {
        var passwordData = passwordHasherService.Hash(request.Model.Password);
        var login = new Email(request.Model.Login);

        return Person.Create(login, passwordData.HashedPassword, passwordData.Salt);
    }

    private async Task<PersonOperationId> CreateOperationAndGetOperationIdAsync(
        Person person,
        CancellationToken cancellationToken)
    {
        var code = codeGenerator.Generate();
        var operationId = await CreateOperationAsync(code, person, cancellationToken);
        var message = PrepareMessage(code, operationId);
        await SendMessageAsync(person, message, operationId, cancellationToken);
        return operationId;
    }

    private async Task<PersonOperationId> CreateOperationAsync(
        string code,
        Person person,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(person.Id);
        return await operationRepository.CreateAsync(new PersonOperationInput.Creating
        {
            PersonId = person.Id.Value,
            Value = code,
            CreatedAt = person.CreatedAt,
            ExpiresAt = DateTimeOffset.MaxValue,
            Verification = Verification.Code,
            PersonOperation = PersonOperation.ProfileCreatedAndActivation,
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

    public async Task SendMessageAsync(
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
        CreatedAt = person.CreatedAt,
    }, cancellationToken);
}
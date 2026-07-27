using Diploma.Application.Interfaces.Database;
using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Authentication.MessageGenerators;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Application.Services;
using Diploma.Application.Services.Generators;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Authentication;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Commands.Authentication.UseCases;

public class PersonUpdateLoginInitiationHandler(
    IUnitOfWorkFactory unitOfWorkFactory,

    ICodeGenerator codeGenerator,
    IPersonUpdateLoginInitiationMessageGenerator messageGenerator,

    IEmailService emailService,
    IPersonRepository repository,
    IPersonOperationRepository operationRepository
    ) : IRequestHandler<PersonUpdateLoginInitiationHandler.Request, PersonUpdateLoginResult>
{
    public sealed record Request : IRequest<PersonUpdateLoginResult>
    {
        public required Guid PersonId { get; init; }
    }

    private const int CODE_VALID_IN_MITUES = 5;
    private static readonly PersonUpdateLoginResult.Failure.General Failure = new();


    public async Task<PersonUpdateLoginResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var person = await GetPersonAsync(request.PersonId, cancellationToken);

        if (!person.HasActive)
            return Failure;

        var code = codeGenerator.Generate();
        var operationId = await CreateOperationAsync(code, person, cancellationToken);
        var message = PrepareMessage(code, operationId);

        await SendMessageAsync(person, message, operationId, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new PersonUpdateLoginResult.Initiation
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
            PersonOperation = PersonOperation.InitiateUpdatingLogin,
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

    private async Task<Person> GetPersonAsync(Guid personId, CancellationToken cancellationToken)
    {
        var personResult = await repository.GetAsync(personId, cancellationToken);
        return personResult.Value ?? throw new InvalidOperationException($"Unable get {typeof(Person)} by Id : {personId}."); ;
    }
}
using Diploma.Application.Interfaces.Database;
using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Extensions;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;
using Diploma.Application.Services;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Lifecycle;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Commands.Lifecycle.UseCases;

public class PersonRemoveHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,

    IPersonRemoveMessageGenerator messageGenerator,

    IEmailService emailService,
    IPersonRepository repository,
    IPersonOperationRepository operationRepository
    ) : IRequestHandler<PersonRemoveHandler.Request, PersonRemoveResult>
{
    public sealed record Request : IRequest<PersonRemoveResult>
    {
        public required Guid PersonId { get; init; }
    }


    private const int OPERATION_VALID_IN_DAYS = 30;
    private static readonly PersonRemoveResult.Success Success = new();
    private static readonly PersonRemoveResult.Failure Failure = new();


    public async Task<PersonRemoveResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var person = await GetPersonAsync(request.PersonId, cancellationToken);

        if (!person.HasActive)
            return Failure;

        var createdAt = DateTimeOffset.Now;
        var expiresAt = createdAt.AddDays(OPERATION_VALID_IN_DAYS);

        var operationId = await CreateOperationAsync(person, createdAt, expiresAt, cancellationToken);
        var message = PrepareMessage(operationId, expiresAt);

        person.Remove(createdAt, expiresAt);
        var updatingResult = await repository.UpdateAsync(person, cancellationToken);

        switch (updatingResult)
        {
            case PersonResult.Updating.Success:
                break;

            default:
                throw new NotImplementedException($"Unknown type of {nameof(PersonResult.Updating)}: {updatingResult.GetType()}");
        }

        await mediator.PublishEventsAsync(person, cancellationToken);
        await SendMessageAsync(person, message, operationId, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return Success;
    }

    private async Task<PersonOperationId> CreateOperationAsync(
        Person person,
        DateTimeOffset createdAt,
        DateTimeOffset expiresAt,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(person.Id);


        return await operationRepository.CreateAsync(new PersonOperationInput.Creating
        {
            PersonId = person.Id.Value,
            Value = null,
            CreatedAt = createdAt,
            ExpiresAt = expiresAt,
            Verification = Verification.MagicLink,
            PersonOperation = PersonOperation.ProfileRemovedAndSendRestoringLink,
        }, cancellationToken);
    }

    private MessageResult PrepareMessage(
        PersonOperationId operationId,
        DateTimeOffset expiresAt
    ) => messageGenerator.Generate(new()
    {
        OperationId = operationId.Value,
        ExpiresAt = expiresAt,
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
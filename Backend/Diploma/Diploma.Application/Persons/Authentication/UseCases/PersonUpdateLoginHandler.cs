using Diploma.Application.Interfaces.Database;
using Diploma.Application.Persons.Extensions;
using Diploma.Application.Persons.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ValueObjects;
using Diploma.Models.Persons.Authentication;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Authentication.UseCases;

public class PersonUpdateLoginHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IPersonOperationRepository personOperationRepository
    ) : IRequestHandler<PersonUpdateLoginHandler.Request, PersonUpdateLoginResult>
{
    public sealed record Request : IRequest<PersonUpdateLoginResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid OperationId { get; init; }
        public required PersonUpdateLoginRequest Model { get; init; }
    }


    private static PersonUpdateLoginResult.Failure.General Failure => new();
    private static PersonUpdateLoginResult.Success Success => new();


    public async Task<PersonUpdateLoginResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);

        var operationResult = await personOperationRepository.GetAsync(
            request.OperationId,
            cancellationToken
        );

        if (!operationResult.HasValue)
            return Failure;

        var operation = operationResult.Value;

        if (operation.HasActivated)
            return Success;

        if (operation.HasExpired)
            return Failure;

        if (operation.PersonOperation != PersonOperation.InitiateUpdatingLogin)
            return Failure;

        if (operation.Verification != Verification.Code)
            return Failure;

        if (operation.Value != request.Model.Code)
            return Failure;

        if (operation.PersonId != request.PersonId)
            return Failure;

        await personOperationRepository.ActivateAsync(operation.PersonOperationId, cancellationToken);

        var personId = request.PersonId;
        var newLogin = new Email(request.Model.Login);

        var person = await GetPersonAsync(personId, cancellationToken);

        if (person.Login == newLogin)
            return new PersonUpdateLoginResult.Failure.LoginExist();

        person.UpdateLogin(newLogin);
        var updatingResult = await personRepository.UpdateAsync(person, cancellationToken);

        switch (updatingResult)
        {
            case PersonResult.Updating.Success:
                break;

            case PersonResult.Updating.Failure.LoginTaken loginTaken:
                return new PersonUpdateLoginResult.Failure.LoginTaken(loginTaken.Login.Value);

            default:
                throw new NotImplementedException($"Unknown type of {nameof(PersonResult.Updating)}: {updatingResult.GetType()}");
        }

        await mediator.PublishEventsAsync(person, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return Success;
    }

    private async Task<Person> GetPersonAsync(Guid personId, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(personId, cancellationToken);
        return personResult.Value ?? throw new InvalidOperationException($"Unable get {typeof(Person)} by Id : {personId}."); ;
    }
}
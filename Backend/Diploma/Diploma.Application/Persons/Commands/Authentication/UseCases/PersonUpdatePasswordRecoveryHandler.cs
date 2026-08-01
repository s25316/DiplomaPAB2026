using Diploma.Application.Interfaces.Database;
using Diploma.Application.Interfaces.Security;
using Diploma.Application.Persons.Commands.Extensions;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Authentication;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Commands.Authentication.UseCases;

public class PersonUpdatePasswordRecoveryHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPasswordHasherService passwordHasher,
    IPersonRepository repository,
    IPersonOperationRepository personOperationRepository
    ) : IRequestHandler<PersonUpdatePasswordRecoveryHandler.Request, PersonUpdatePasswordResult>
{
    public sealed record Request : IRequest<PersonUpdatePasswordResult>
    {
        public required Guid OperationId { get; init; }
        public required PersonUpdatePasswordRecoveryRequest Model { get; init; }
    }

    private static PersonUpdatePasswordResult.Failure.General Failure => new();
    private static PersonUpdatePasswordResult.Success Success => new();


    public async Task<PersonUpdatePasswordResult> Handle(Request request, CancellationToken cancellationToken)
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

        if (operation.PersonOperation != PersonOperation.InitiateUpdatingPassword)
            return Failure;

        if (operation.Verification != Verification.Code)
            return Failure;

        if (operation.Value != request.Model.Code)
            return Failure;

        await personOperationRepository.ActivateAsync(operation.PersonOperationId, cancellationToken);

        var personId = operation.PersonId;
        var person = await GetPersonAsync(personId, cancellationToken);

        var newHashedPassword = passwordHasher.Hash(request.Model.Password);
        person.UpdatePassword(newHashedPassword.HashedPassword, newHashedPassword.Salt);
        var updatingResult = await repository.UpdateAsync(person, cancellationToken);

        switch (updatingResult)
        {
            case PersonResult.Updating.Success:
                break;

            default:
                throw new NotImplementedException($"Unknown type of {nameof(PersonResult.Updating)}: {updatingResult.GetType()}");
        }

        await mediator.PublishEventsAsync(person, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return Success;
    }

    private async Task<Person> GetPersonAsync(Guid personId, CancellationToken cancellationToken)
    {
        var personResult = await repository.GetAsync(personId, cancellationToken);
        return personResult.Value ?? throw new InvalidOperationException($"Unable get {typeof(Person)} by Id : {personId}."); ;
    }
}
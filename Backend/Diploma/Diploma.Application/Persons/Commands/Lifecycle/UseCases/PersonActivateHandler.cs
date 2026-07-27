using Diploma.Application.Interfaces.Database;
using Diploma.Application.Persons.Commands.Extensions;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Lifecycle;
using Diploma.Shared.PersonOperations;
using Diploma.Shared.Verifications;
using MediatR;

namespace Diploma.Application.Persons.Commands.Lifecycle.UseCases;

public class PersonActivateHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository repository,
    IPersonOperationRepository operationRepository
    ) : IRequestHandler<PersonActivateHandler.Request, PersonActivateResult>
{
    public sealed record Request : IRequest<PersonActivateResult>
    {
        public required Guid OperationId { get; init; }
        public required PersonActivateRequest Model { get; init; }
    }


    private static PersonActivateResult.Failure Failure => new();
    private static PersonActivateResult.Success Success => new();


    public async Task<PersonActivateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);

        var operationResult = await operationRepository.GetAsync(request.OperationId, cancellationToken);

        if (!operationResult.HasValue)
            return Failure;

        var operation = operationResult.Value;

        if (operation.HasActivated)
            return Success;

        if (operation.HasExpired)
            return Failure;

        if (operation.PersonOperation != PersonOperation.ProfileCreatedAndActivation)
            return Failure;

        if (operation.Verification != Verification.Code)
            return Failure;

        if (operation.Value != request.Model.Code)
            return Failure;

        await operationRepository.ActivateAsync(operation.PersonOperationId, cancellationToken);
        await PersonActivateAsync(operation.PersonId, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return Success;
    }

    private async Task PersonActivateAsync(Guid personId, CancellationToken cancellationToken)
    {
        var person = await GetPersonAsync(personId, cancellationToken);
        person.Activate();

        var updatingResult = await repository.UpdateAsync(person, cancellationToken);

        switch (updatingResult)
        {
            case PersonResult.Updating.Success: break;
            default:
                throw new NotImplementedException($"Unknown type of {nameof(PersonResult.Updating)}: {updatingResult.GetType()}");
        }

        await mediator.PublishEventsAsync(person, cancellationToken);
    }

    private async Task<Person> GetPersonAsync(Guid personId, CancellationToken cancellationToken)
    {
        var personResult = await repository.GetAsync(personId, cancellationToken);
        return personResult.Value ?? throw new InvalidOperationException($"Unable get {typeof(Person)} by Id : {personId}."); ;
    }
}
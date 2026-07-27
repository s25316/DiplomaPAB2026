using Diploma.Application.Interfaces.Database;
using Diploma.Application.Persons.Commands.Extensions;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Lifecycle;
using MediatR;

namespace Diploma.Application.Persons.Commands.Lifecycle.UseCases;

public class PersonRestoreHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository repository,
    IPersonOperationRepository operationRepository
    ) : IRequestHandler<PersonRestoreHandler.Request, PersonRestoreResult>
{
    public sealed record Request : IRequest<PersonRestoreResult>
    {
        public required Guid OperationId { get; init; }
    }


    private static readonly PersonRestoreResult.Success Success = new();
    private static readonly PersonRestoreResult.Failure Failure = new();


    public async Task<PersonRestoreResult> Handle(Request request, CancellationToken cancellationToken)
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

        var person = await GetPersonAsync(operation.PersonId, cancellationToken);
        person.Restore();
        var updatingResult = await repository.UpdateAsync(person, cancellationToken);

        switch (updatingResult)
        {
            case PersonResult.Updating.Success: break;

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
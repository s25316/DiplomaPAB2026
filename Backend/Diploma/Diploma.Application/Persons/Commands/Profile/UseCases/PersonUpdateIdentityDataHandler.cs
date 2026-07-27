using Diploma.Application.Interfaces.Database;
using Diploma.Application.Persons.Commands.Extensions;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Profile;
using MediatR;

namespace Diploma.Application.Persons.Commands.Profile.UseCases;

public class PersonUpdateIdentityDataHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository repository
    ) : IRequestHandler<PersonUpdateIdentityDataHandler.Request, PersonUpdateIdentityDataResult>
{
    public sealed record Request : IRequest<PersonUpdateIdentityDataResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonUpdateIdentityDataRequest Model { get; init; }
    }


    private static readonly PersonUpdateIdentityDataResult.Success Success = new();
    private static readonly PersonUpdateIdentityDataResult.Failure Failure = new();


    public async Task<PersonUpdateIdentityDataResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var person = await GetPersonAsync(request.PersonId, cancellationToken);

        if (!person.HasActive)
            return Failure;

        person.UpdateIdentityData(request.Model.Name, request.Model.Surname);
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
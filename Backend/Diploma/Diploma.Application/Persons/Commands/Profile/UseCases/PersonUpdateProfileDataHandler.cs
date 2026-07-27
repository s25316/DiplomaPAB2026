using Diploma.Application.Interfaces.Database;
using Diploma.Application.Persons.Commands.Extensions;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Profile;
using MediatR;

namespace Diploma.Application.Persons.Commands.Profile.UseCases;

public class PersonUpdateProfileDataHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository repository
    ) : IRequestHandler<PersonUpdateProfileDataHandler.Request, PersonUpdateProfileDataResult>
{
    public sealed record Request : IRequest<PersonUpdateProfileDataResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonUpdateProfileDataRequest Model { get; init; }

    }


    private static readonly PersonUpdateProfileDataResult.Success Success = new();
    private static readonly PersonUpdateProfileDataResult.Failure Failure = new();


    public async Task<PersonUpdateProfileDataResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var person = await GetPersonAsync(request.PersonId, cancellationToken);

        if (!person.HasActive)
            return Failure;

        person.UpdateProfileData(request.Model.Title, request.Model.Summary);
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
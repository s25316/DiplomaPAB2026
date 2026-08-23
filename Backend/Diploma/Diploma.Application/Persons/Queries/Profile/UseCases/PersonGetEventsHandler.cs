using Diploma.Application.Persons.Queries.Profile.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEvents;
using MediatR;

namespace Diploma.Application.Persons.Queries.Profile.UseCases;

public class PersonGetEventsHandler(
    IPersonRepository personRepository,
    IPersonEventQueryService queryService
    ) : IRequestHandler<PersonGetEventsHandler.Request, PersonEventQueryResult>
{
    public sealed record Request : IRequest<PersonEventQueryResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonEventQueryParameters Model { get; init; }
    }


    public async Task<PersonEventQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEventQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEventQueryResult.Failure.ProfileInactive();

        var result = await queryService.GetAsync(request.PersonId, request.Model, cancellationToken);
        return new PersonEventQueryResult.Success(result);
    }
}
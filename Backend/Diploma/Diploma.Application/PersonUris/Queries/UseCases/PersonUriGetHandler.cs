using Diploma.Application.PersonUris.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonUris;
using MediatR;

namespace Diploma.Application.PersonUris.Queries.UseCases;

public class PersonUriGetHandler(
    IPersonRepository personRepository,
    IPersonUriQueryService service
    ) : IRequestHandler<PersonUriGetHandler.Request, PersonUriQueryResult>
{
    public sealed record Request : IRequest<PersonUriQueryResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonUriQueryParameters Model { get; init; }
    }


    public async Task<PersonUriQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonUriQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonUriQueryResult.Failure.ProfileInactive();

        var result = await service.GetAsync(request.PersonId, request.Model, cancellationToken);
        return new PersonUriQueryResult.Success(result);
    }
}
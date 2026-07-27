using Diploma.Application.PersonEducations.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEducations;
using MediatR;

namespace Diploma.Application.PersonEducations.Queries.UseCases;

public class PersonEducationGetHandler(
    IPersonRepository personRepository,
    IPersonEducationQueryService service
    ) : IRequestHandler<PersonEducationGetHandler.Request, PersonEducationQueryResult>
{
    public sealed record Request : IRequest<PersonEducationQueryResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonEducationQueryParameters Model { get; init; }
    }


    public async Task<PersonEducationQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEducationQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEducationQueryResult.Failure.ProfileInactive();

        var result = await service.GetAsync(request.PersonId, request.Model, cancellationToken);
        return new PersonEducationQueryResult.Success(result);
    }
}
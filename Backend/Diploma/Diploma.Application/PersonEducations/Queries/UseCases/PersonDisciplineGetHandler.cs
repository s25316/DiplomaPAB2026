using Diploma.Application.PersonEducations.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEducations;
using MediatR;

namespace Diploma.Application.PersonEducations.Queries.UseCases;

public class PersonDisciplineGetHandler(
    IPersonRepository personRepository,
    IPersonDisciplineQueryService service
    ) : IRequestHandler<PersonDisciplineGetHandler.Request, PersonDisciplineQueryResult>
{
    public sealed record Request : IRequest<PersonDisciplineQueryResult>
    {
        public required Guid PersonId { get; init; }
    }

    public async Task<PersonDisciplineQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonDisciplineQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonDisciplineQueryResult.Failure.ProfileInactive();

        var result = await service.GetAsync(request.PersonId, cancellationToken);
        return new PersonDisciplineQueryResult.Success(result);
    }
}
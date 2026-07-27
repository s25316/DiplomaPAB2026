using Diploma.Application.PersonEmployments.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEmployments;
using MediatR;

namespace Diploma.Application.PersonEmployments.Queries.UseCases;

public class PersonEmploymentGetHandler(
    IPersonRepository personRepository,
    IPersonEmploymentQueryService service
    ) : IRequestHandler<PersonEmploymentGetHandler.Request, PersonEmploymentQueryResult>
{
    public sealed record Request : IRequest<PersonEmploymentQueryResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonEmploymentQueryParameters Model { get; init; }
    }


    public async Task<PersonEmploymentQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEmploymentQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEmploymentQueryResult.Failure.ProfileInactive();

        var result = await service.GetAsync(request.PersonId, request.Model, cancellationToken);
        return new PersonEmploymentQueryResult.Success(result);
    }
}
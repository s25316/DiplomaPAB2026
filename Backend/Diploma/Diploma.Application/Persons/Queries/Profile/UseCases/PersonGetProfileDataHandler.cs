using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Profile;
using MediatR;

namespace Diploma.Application.Persons.Queries.Profile.UseCases;

public class PersonGetProfileDataHandler(
    IPersonRepository personRepository
    ) : IRequestHandler<PersonGetProfileDataHandler.Request, PersonProfileDataQueryResult>
{
    public sealed record Request : IRequest<PersonProfileDataQueryResult>
    {
        public required Guid PersonId { get; init; }
    }


    public async Task<PersonProfileDataQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonProfileDataQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonProfileDataQueryResult.Failure.ProfileInactive();

        return new PersonProfileDataQueryResult.Success(new PersonProfileDataDto
        {
            Title = person.Title,
            Summary = person.Summary,
        });
    }
}
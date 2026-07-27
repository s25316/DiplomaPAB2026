using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Profile;
using MediatR;

namespace Diploma.Application.Persons.Queries.Profile.UseCases;

public class PersonGetIdentityDataHandler(
    IPersonRepository personRepository
    ) : IRequestHandler<PersonGetIdentityDataHandler.Request, PersonIdentityDataQueryResult>
{
    public sealed record Request : IRequest<PersonIdentityDataQueryResult>
    {
        public required Guid PersonId { get; init; }
    }


    public async Task<PersonIdentityDataQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonIdentityDataQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonIdentityDataQueryResult.Failure.ProfileInactive();

        return new PersonIdentityDataQueryResult.Success(new PersonIdentityDataDto
        {
            Name = person.Name,
            Surname = person.Surname,
        });
    }
}
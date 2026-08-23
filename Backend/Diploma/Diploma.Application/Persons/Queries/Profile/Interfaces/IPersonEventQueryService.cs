using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEvents;
using Diploma.Models.Shared;

namespace Diploma.Application.Persons.Queries.Profile.Interfaces;

public interface IPersonEventQueryService
{
    Task<Response<PersonEventDto>> GetAsync(
        PersonId personId,
        PersonEventQueryParameters parameters,
        CancellationToken cancellationToken = default);
}
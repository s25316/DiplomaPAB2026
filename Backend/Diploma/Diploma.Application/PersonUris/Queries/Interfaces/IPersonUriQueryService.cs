using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonUris;
using Diploma.Models.Shared;

namespace Diploma.Application.PersonUris.Queries.Interfaces;

public interface IPersonUriQueryService
{
    Task<Response<PersonUriDto>> GetAsync(
        PersonId personId,
        PersonUriQueryParameters parameters,
        CancellationToken cancellationToken = default);
}
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEmployments;
using Diploma.Models.Shared;

namespace Diploma.Application.PersonEmployments.Queries.Interfaces;

public interface IPersonEmploymentQueryService
{
    Task<Response<PersonEmploymentDto>> GetAsync(
        PersonId personId,
        PersonEmploymentQueryParameters parameters,
        CancellationToken cancellationToken = default);
}
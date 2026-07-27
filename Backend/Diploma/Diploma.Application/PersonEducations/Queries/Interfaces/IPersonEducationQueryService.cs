using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEducations;

namespace Diploma.Application.PersonEducations.Queries.Interfaces;

public interface IPersonEducationQueryService
{
    Task<IEnumerable<PersonEducationDto>> GetAsync(
        PersonId personId,
        PersonEducationQueryParameters parameters,
        CancellationToken cancellationToken = default);
}
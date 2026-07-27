using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Educations;

namespace Diploma.Application.PersonEducations.Queries.Interfaces;

public interface IPersonDisciplineQueryService
{
    Task<IEnumerable<EducationDisciplineDto>> GetAsync(
        PersonId personId,
        CancellationToken cancellationToken = default);
}
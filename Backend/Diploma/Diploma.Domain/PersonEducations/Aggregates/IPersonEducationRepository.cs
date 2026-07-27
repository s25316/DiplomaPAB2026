using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.PersonEducations.Aggregates;

public interface IPersonEducationRepository
{
    Task<OptionalResult<PersonEducation>> GetAsync(PersonEducationId id, CancellationToken cancellationToken = default);
    Task CreateAsync(PersonEducation item, CancellationToken cancellationToken = default);
    Task<ExistingResult> UpdateAsync(PersonEducation item, CancellationToken cancellationToken = default);
    Task<ExistingResult> DeleteAsync(PersonEducation item, CancellationToken cancellationToken = default);
    Task<int> TotalCountAsync(PersonId id, CancellationToken cancellationToken = default);
}
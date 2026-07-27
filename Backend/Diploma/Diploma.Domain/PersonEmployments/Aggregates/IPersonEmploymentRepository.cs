using Diploma.Domain.Base.Results;

namespace Diploma.Domain.PersonEmployments.Aggregates;

public interface IPersonEmploymentRepository
{
    Task<OptionalResult<PersonEmployment>> GetAsync(PersonEmploymentId id, CancellationToken cancellationToken = default);
    Task CreateAsync(PersonEmployment item, CancellationToken cancellationToken = default);
    Task<ExistingResult> UpdateAsync(PersonEmployment item, CancellationToken cancellationToken = default);
    Task<ExistingResult> DeleteAsync(PersonEmployment item, CancellationToken cancellationToken = default);
}
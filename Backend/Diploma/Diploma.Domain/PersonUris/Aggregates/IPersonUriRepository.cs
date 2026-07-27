using Diploma.Domain.Base.Results;

namespace Diploma.Domain.PersonUris.Aggregates;

public interface IPersonUriRepository
{
    Task<OptionalResult<PersonUri>> GetAsync(PersonUriId id, CancellationToken cancellationToken = default);
    Task CreateAsync(PersonUri item, CancellationToken cancellationToken = default);
    Task<ExistingResult> UpdateAsync(PersonUri item, CancellationToken cancellationToken = default);
    Task<ExistingResult> DeleteAsync(PersonUri item, CancellationToken cancellationToken = default);
}
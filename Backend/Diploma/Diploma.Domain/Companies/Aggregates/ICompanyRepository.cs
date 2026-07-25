using Diploma.Domain.Base.Results;

namespace Diploma.Domain.Companies.Aggregates;

public interface ICompanyRepository
{
    Task<OptionalResult<Company>> GetAsync(
        CompanyId id,
        CancellationToken cancellationToken = default);
}
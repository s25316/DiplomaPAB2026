using Diploma.Domain.Base.Results;

namespace Diploma.Domain.EducationInstitutions.Aggregates;

public interface IEducationInstitutionRepository
{
    Task<OptionalResult<EducationInstitution>> GetAsync(
        EducationInstitutionId id,
        CancellationToken cancellationToken = default);
}
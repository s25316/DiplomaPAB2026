using Diploma.Domain.Base.Results;
using Diploma.Domain.Educations.Aggregates;

namespace Diploma.Domain.Educations.Repositories;

public interface IEducationInstitutionRepository
{
    Task<OptionalResult<EducationInstitution>> GetAsync(EducationInstitutionId id, CancellationToken cancellationToken = default);
}
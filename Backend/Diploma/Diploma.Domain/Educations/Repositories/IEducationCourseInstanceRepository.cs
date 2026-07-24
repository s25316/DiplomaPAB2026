using Diploma.Domain.Base.Results;
using Diploma.Domain.Educations.Aggregates;

namespace Diploma.Domain.Educations.Repositories;

public interface IEducationCourseInstanceRepository
{
    Task<OptionalResult<EducationCourseInstance>> GetAsync(EducationCourseInstanceId id, CancellationToken cancellationToken = default);
}
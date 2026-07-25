using Diploma.Domain.Base.Results;

namespace Diploma.Domain.EducationCourseInstances.Aggregates;

public interface IEducationCourseInstanceRepository
{
    Task<OptionalResult<EducationCourseInstance>> GetAsync(
        EducationCourseInstanceId id,
        CancellationToken cancellationToken = default);
}
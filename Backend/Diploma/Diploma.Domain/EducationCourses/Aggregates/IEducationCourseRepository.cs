using Diploma.Domain.Base.Results;

namespace Diploma.Domain.EducationCourses.Aggregates;

public interface IEducationCourseRepository
{
    Task<OptionalResult<EducationCourse>> GetAsync(
        EducationCourseId id,
        CancellationToken cancellationToken = default);
}
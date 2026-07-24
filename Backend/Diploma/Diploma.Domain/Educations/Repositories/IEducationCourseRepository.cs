using Diploma.Domain.Base.Results;
using Diploma.Domain.Educations.Aggregates;

namespace Diploma.Domain.Educations.Repositories;

public interface IEducationCourseRepository
{
    Task<OptionalResult<EducationCourse>> GetAsync(EducationCourseId id, CancellationToken cancellationToken = default);
}
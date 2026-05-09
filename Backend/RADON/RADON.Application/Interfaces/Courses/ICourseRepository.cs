using RADON.Models.Responses.Courses;

namespace RADON.Application.Interfaces.Courses;

public interface ICourseRepository
{
    Task CreateOrUpdateAsync(IEnumerable<Course> items, CancellationToken cancellationToken = default);
}
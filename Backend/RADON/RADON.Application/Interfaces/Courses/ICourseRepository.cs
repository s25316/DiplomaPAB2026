using RADON.Application.Interfaces.Base;
using RADON.Models.Courses;
using RADON.Models.Courses.Responses;

namespace RADON.Application.Interfaces.Courses;

public interface ICourseRepository : IRepository<Course, QueryParameters>;
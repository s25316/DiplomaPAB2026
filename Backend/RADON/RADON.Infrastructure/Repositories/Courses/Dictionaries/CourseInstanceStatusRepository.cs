using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Repositories.Courses.Dictionaries;

public class CourseInstanceStatusRepository(RadonDbContext context) : BaseDictionaryRepository<CourseInstanceStatus>(
    context,
    (context, cancellationToken) => context.CourseInstanceStatuses.ToDictionaryAsync(k => k.CourseInstanceStatusCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.CourseInstanceStatuses,
    dictionaryItem => new CourseInstanceStatus { CourseInstanceStatusCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem { Code = entity.CourseInstanceStatusCode, Name = entity.Name }
), ICourseInstanceStatusRepository;
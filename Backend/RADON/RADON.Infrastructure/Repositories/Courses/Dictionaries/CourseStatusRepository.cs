using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Courses.Dictionaries;

public class CourseStatusRepository(RadonDbContext context) : BaseDictionaryRepository<CourseStatus>(
    context,
    (context, cancellationToken) => context.CourseStatuses.ToDictionaryAsync(k => k.CourseStatusCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.CourseStatuses,
    dictionaryItem => new CourseStatus { CourseStatusCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem(entity.CourseStatusCode, entity.Name)
), ICourseStatusRepository;
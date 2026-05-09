using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Courses.Dictionaries;

public class CourseLevelRepository(RadonDbContext context) : BaseDictionaryRepository<CourseLevel>(
    context,
    (context, cancellationToken) => context.CourseLevels.ToDictionaryAsync(k => k.CourseLevelCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.CourseLevels,
    dictionaryItem => new CourseLevel { CourseLevelCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem(entity.CourseLevelCode, entity.Name)
), ICourseLevelRepository;
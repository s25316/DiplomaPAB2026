using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Repositories.Courses.Dictionaries;

public class CourseProfileRepository(RadonDbContext context) : BaseDictionaryRepository<CourseProfile>(
    context,
    (context, cancellationToken) => context.CourseProfiles.ToDictionaryAsync(k => k.CourseProfileCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.CourseProfiles,
    dictionaryItem => new CourseProfile { CourseProfileCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem { Code = entity.CourseProfileCode, Name = entity.Name }
), ICourseProfileRepository;
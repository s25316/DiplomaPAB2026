using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Repositories.Courses.Dictionaries;

public class CourseFormRepository(RadonDbContext context) : BaseDictionaryRepository<CourseForm>(
    context,
    (context, cancellationToken) => context.CourseForms.ToDictionaryAsync(k => k.CourseFormCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.CourseForms,
    dictionaryItem => new CourseForm { CourseFormCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem { Code = entity.CourseFormCode, Name = entity.Name }
), ICourseFormRepository;
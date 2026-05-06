using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Courses;

public class CourseFormRespository(RadonDbContext context) : BaseDictionaryRespository<CourseForm>(
    context,
    (context, cancellationToken) => context.CourseForms.ToDictionaryAsync(k => k.CourseFormCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.CourseForms,
    dictionaryItem => new CourseForm { CourseFormCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem(entity.CourseFormCode, entity.Name)
), ICourseFormRespository;
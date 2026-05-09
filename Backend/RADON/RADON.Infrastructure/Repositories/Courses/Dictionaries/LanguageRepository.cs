using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Repositories.Courses.Dictionaries;

public class LanguageRepository(RadonDbContext context) : BaseDictionaryRepository<Language>(
    context,
    (context, cancellationToken) => context.Languages.ToDictionaryAsync(k => k.LanguageCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.Languages,
    dictionaryItem => new Language { LanguageCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem { Code = entity.LanguageCode, Name = entity.Name }
), ILanguageRepository;
using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Courses;

public class LanguageRespository(RadonDbContext context) : BaseDictionaryRespository<Language>(
    context,
    (context, cancellationToken) => context.Languages.ToDictionaryAsync(k => k.LanguageCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.Languages,
    dictionaryItem => new Language { LanguageCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem(entity.LanguageCode, entity.Name)
), ILanguageRespository;
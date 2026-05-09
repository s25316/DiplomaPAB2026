using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Shared.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Shared;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Repositories.Shared.Dictionaries;

public class DisciplineRespository(RadonDbContext context) : BaseDictionaryRepository<Discipline>(
    context,
    (context, cancellationToken) => context.Disciplines.ToDictionaryAsync(k => k.DisciplineCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.Disciplines,
    dictionaryItem => new Discipline { DisciplineCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem { Code = entity.DisciplineCode, Name = entity.Name }
), IDisciplineRespository;
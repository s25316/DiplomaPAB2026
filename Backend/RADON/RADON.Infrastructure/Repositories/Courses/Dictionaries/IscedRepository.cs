using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Repositories.Courses.Dictionaries;

public class IscedRepository(RadonDbContext context) : BaseDictionaryRepository<Isced>(
    context,
    (context, cancellationToken) => context.Isceds.ToDictionaryAsync(k => k.IscedCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.Isceds,
    dictionaryItem => new Isced { IscedCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem { Code = entity.IscedCode, Name = entity.Name }
), IIscedRepository;
using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Institutions.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Institutions;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Repositories.Institutions.Dictionaries;

public class InstitutionStatusRepository(RadonDbContext context) : BaseDictionaryRepository<InstitutionStatus>(
    context,
    (context, cancellationToken) => context.InstitutionStatuses.ToDictionaryAsync(k => k.InstitutionStatusCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.InstitutionStatuses,
    dictionaryItem => new InstitutionStatus { InstitutionStatusCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem { Code = entity.InstitutionStatusCode, Name = entity.Name }
), IInstitutionStatusRepository;
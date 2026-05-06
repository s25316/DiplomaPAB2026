using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Institutions;
using RADON.Database;
using RADON.Database.Models.Institutions;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Institutions;

public class InstitutionStatusRespository(RadonDbContext context) : BaseDictionaryRespository<InstitutionStatus>(
    context,
    (context, cancellationToken) => context.InstitutionStatuses.ToDictionaryAsync(k => k.InstitutionStatusCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.InstitutionStatuses,
    dictionaryItem => new InstitutionStatus { InstitutionStatusCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem(entity.InstitutionStatusCode, entity.Name)
), IInstitutionStatusRespository;
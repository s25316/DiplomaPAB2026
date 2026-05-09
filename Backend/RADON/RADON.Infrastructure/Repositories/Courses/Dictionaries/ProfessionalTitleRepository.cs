using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.Repositories.Base;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Repositories.Courses.Dictionaries;

public class ProfessionalTitleRepository(RadonDbContext context) : BaseDictionaryRepository<ProfessionalTitle>(
    context,
    (context, cancellationToken) => context.ProfessionalTitles.ToDictionaryAsync(k => k.ProfessionalTitleCode, cancellationToken),
    (entity, name) => entity.Name = name,
    context => context.ProfessionalTitles,
    dictionaryItem => new ProfessionalTitle { ProfessionalTitleCode = dictionaryItem.Code, Name = dictionaryItem.Name },
    entity => new DictionaryItem(entity.ProfessionalTitleCode, entity.Name)
), IProfessionalTitleRepository;
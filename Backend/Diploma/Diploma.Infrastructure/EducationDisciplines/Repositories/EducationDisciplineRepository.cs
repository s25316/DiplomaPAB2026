using Diploma.Database;
using Diploma.Domain.EducationDisciplines.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.EducationDisciplines.Repositories;

public class EducationDisciplineRepository(
    DiplomaDbContext context
    ) : IEducationDisciplineRepository
{
    public async Task<IDictionary<string, EducationDiscipline>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        var items = await context
            .EducationDisciplines
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return items.ToDictionary(
            k => k.Code,
            v => new EducationDiscipline
            {
                Code = v.Code,
                Name = v.Name,
            });
    }
}
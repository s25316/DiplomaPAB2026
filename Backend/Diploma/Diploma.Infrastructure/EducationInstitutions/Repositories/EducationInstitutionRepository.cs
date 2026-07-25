using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.EducationInstitutions.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.EducationInstitutions.Repositories;

public class EducationInstitutionRepository(
    DiplomaDbContext context
    ) : IEducationInstitutionRepository
{
    private static readonly OptionalResult<EducationInstitution> NotFound = OptionalResult<EducationInstitution>.NotFound();


    public async Task<OptionalResult<EducationInstitution>> GetAsync(
        EducationInstitutionId id,
        CancellationToken cancellationToken = default)
    {
        var databaseItem = await context
            .EducationInstitutions
            .AsNoTracking()
            .FirstOrDefaultAsync(
                i => i.EducationInstitutionId == id.Value,
                cancellationToken
            );

        if (databaseItem is null)
            return NotFound;

        return OptionalResult.Success(new EducationInstitution
        {
            Id = new EducationInstitutionId() { Value = databaseItem.EducationInstitutionId },
            StartDate = databaseItem.StartDate,
            LiquidationStartDate = databaseItem.LiquidationStartDate,
            LiquidationDate = databaseItem.LiquidationDate,
        });
    }
}
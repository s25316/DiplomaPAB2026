using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.EducationCourseInstances.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.EducationCourseInstances.Repositories;

public class EducationCourseInstanceRepository(
    DiplomaDbContext context
    ) : IEducationCourseInstanceRepository
{
    private static readonly OptionalResult<EducationCourseInstance> NotFound = OptionalResult<EducationCourseInstance>.NotFound();


    public async Task<OptionalResult<EducationCourseInstance>> GetAsync(
        EducationCourseInstanceId id,
        CancellationToken cancellationToken = default)
    {
        var databaseItem = await context
            .EducationCourseInstances
            .AsNoTracking()
            .FirstOrDefaultAsync(i =>
                i.EducationCourseInstanceId == id.Value,
                cancellationToken
            );

        if (databaseItem is null)
            return NotFound;

        return OptionalResult.Success(new EducationCourseInstance
        {
            Id = new EducationCourseInstanceId { Value = databaseItem.EducationCourseInstanceId },
            EducationCourseId = databaseItem.EducationCourseId,
            EducationStartDate = databaseItem.EducationStartDate,
            LiquidationDate = databaseItem.LiquidationDate,
        });
    }
}
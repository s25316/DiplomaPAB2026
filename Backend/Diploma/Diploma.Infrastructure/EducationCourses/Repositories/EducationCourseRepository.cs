using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.EducationCourses.Aggregates;
using Diploma.Domain.EducationDisciplines.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.EducationCourses.Repositories;

public class EducationCourseRepository(
    DiplomaDbContext context
    ) : IEducationCourseRepository
{
    private static readonly OptionalResult<EducationCourse> NotFound = OptionalResult<EducationCourse>.NotFound();


    public async Task<OptionalResult<EducationCourse>> GetAsync(
        EducationCourseId id,
        CancellationToken cancellationToken = default)
    {
        var databaseItem = await context
            .EducationCourses
            .AsNoTracking()
            .Include(i => i.EducationCourseDisciplines)
            .ThenInclude(i => i.EducationDiscipline)
            .FirstOrDefaultAsync(
                i => i.EducationCourseId == id.Value,
                cancellationToken
            );

        if (databaseItem is null)
            return NotFound;

        return OptionalResult.Success(new EducationCourse
        {
            Id = new EducationCourseId { Value = databaseItem.EducationCourseId },
            CreationDate = databaseItem.CreationDate,
            TerminationInitializationDate = databaseItem.TerminationInitializationDate,
            LiquidationDate = databaseItem.LiquidationDate,
            EducationInstitutionId = databaseItem.EducationInstitutionId,
            Disciplines = databaseItem.EducationCourseDisciplines
                .Select(i => new EducationCourse.CourseDiscipline
                {
                    Percentage = i.Percentage,
                    IsLeading = i.IsLeading,
                    Discipline = new EducationDiscipline
                    {
                        Code = i.EducationDiscipline.Code,
                        Name = i.EducationDiscipline.Name,
                    }
                }).ToList()
        });
    }
}

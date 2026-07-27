using Diploma.Application.PersonEducations.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Persons;
using Diploma.Models.PersonEducations;
using Diploma.Shared.Semesters;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.PersonEducations.QueryServices;

public class PersonEducationQueryService(PersonEducationQueryBuilder builder) : IPersonEducationQueryService
{
    public async Task<IEnumerable<PersonEducationDto>> GetAsync(
        PersonId personId,
        PersonEducationQueryParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithPersonId(personId)
            .WithOrderBy(
                parameters.Order,
                parameters.OrderBy
            ).Build();

        var items = await query.ToListAsync(cancellationToken);

        return items.Select(i => new PersonEducationDto
        {
            EducationId = i.Root?.PersonEducationId ?? i.PersonEducationId,
            EducationCourseId = i.EducationCourseId,
            EducationCourseInstanceId = i.EducationCourseInstanceId,
            Start = new EducationSemestrResponseDto
            {
                Year = i.YearStart,
                Semester = new SemesterResponseDto
                {
                    SemestrId = Semester.FromId(i.SemesterStartId).Id,
                    Name = Semester.FromId(i.SemesterStartId).Name,
                },
            },
            End = i.YearEnd.HasValue && i.SemesterEndId.HasValue
                ? new EducationSemestrResponseDto
                {
                    Year = i.YearEnd.Value,
                    Semester = new SemesterResponseDto
                    {
                        SemestrId = Semester.FromId(i.SemesterEndId.Value).Id,
                        Name = Semester.FromId(i.SemesterEndId.Value).Name,
                    },
                } : null,
        });
    }
}
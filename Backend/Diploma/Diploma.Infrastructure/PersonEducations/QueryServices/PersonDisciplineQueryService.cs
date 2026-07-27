using Diploma.Application.PersonEducations.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Persons;
using Diploma.Models.Educations;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.PersonEducations.QueryServices;

public class PersonDisciplineQueryService(PersonEducationQueryBuilder builder) : IPersonDisciplineQueryService
{
    public async Task<IEnumerable<EducationDisciplineDto>> GetAsync(
        PersonId personId,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithPersonId(personId)
            .Build();

        var items = await query.ToListAsync(cancellationToken);
        return items
            .SelectMany(i => i.EducationCourse.EducationCourseDisciplines)
            .Select(i => new EducationDisciplineDto
            {
                Code = i.EducationDiscipline.Code,
                Name = i.EducationDiscipline.Name,
            });
    }
}

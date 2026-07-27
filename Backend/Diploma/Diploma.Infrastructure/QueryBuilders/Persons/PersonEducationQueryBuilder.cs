using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Diploma.Shared.PersonEvents;
using Microsoft.EntityFrameworkCore;
using static Diploma.Models.PersonEducations.PersonEducationQueryParameters;
using DatabasePersonEducation = Diploma.Database.Models.Persons.PersonEvents.Audits.PersonEducation;

namespace Diploma.Infrastructure.QueryBuilders.Persons;

public class PersonEducationQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabasePersonEducation>(
    context
    .PersonEducations
    .AsNoTracking()
    .Include(i => i.PersonEvent)
    .Include(i => i.Root)
    .ThenInclude(i => i!.PersonEvent)
    .Include(i => i.EducationCourse)
    .ThenInclude(i => i.EducationCourseDisciplines)
    .ThenInclude(i => i.EducationDiscipline)
    .Where(i => i.NextId == null && i.PersonEvent.PersonEventTypeId != PersonEvent.DeleteEducation.Id)
    )
{
    public PersonEducationQueryBuilder WithPersonId(PersonId item)
    {
        With(query => query.Where(i => i.PersonEvent.PersonId == item.Value));
        return this;
    }

    public PersonEducationQueryBuilder WithOrderBy(
        Order order,
        PersonEducationOrderBy orderBy)
    {
        With(query =>
        {
            return orderBy switch
            {
                _ => order == Order.Ascending
                    ? query
                    .OrderBy(i => i.YearStart)
                    .ThenBy(i => i.SemesterStartId)
                    .ThenBy(i => i.YearEnd)
                    .ThenBy(i => i.SemesterEndId)

                    : query
                    .OrderByDescending(i => i.YearStart)
                    .ThenByDescending(i => i.SemesterStartId)
                    .ThenByDescending(i => i.YearEnd)
                    .ThenByDescending(i => i.SemesterEndId),
            };
        });
        return this;
    }
}
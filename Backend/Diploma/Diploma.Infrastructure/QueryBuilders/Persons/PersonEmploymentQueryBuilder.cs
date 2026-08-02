using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Diploma.Shared.PersonEvents;
using Microsoft.EntityFrameworkCore;
using static Diploma.Models.PersonEmployments.PersonEmploymentQueryParameters;
using DatabasePersonEmployment = Diploma.Database.Models.Persons.PersonEvents.Audits.PersonEmployment;

namespace Diploma.Infrastructure.QueryBuilders.Persons;

public class PersonEmploymentQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabasePersonEmployment>(
    context
    .PersonEmployments
    .AsNoTracking()
    .Include(i => i.PersonEvent)
    .Include(i => i.Root)
    .ThenInclude(i => i!.PersonEvent)
    .Where(i => i.NextId == null && i.PersonEvent.PersonEventTypeId != PersonEvent.DeleteEmployment.Id)
    )
{
    public PersonEmploymentQueryBuilder WithPersonId(PersonId item)
    {
        With(query => query.Where(i => i.PersonEvent.PersonId == item.Value));
        return this;
    }

    public PersonEmploymentQueryBuilder WithOrderBy(
        Order order,
        PersonEmploymentOrderBy orderBy,
        QueryParametersPagination pagination)
    {
        With(query =>
        {
            return orderBy switch
            {
                PersonEmploymentOrderBy.Position => order == Order.Ascending
                    ? query.OrderBy(i => i.Position)
                    : query.OrderByDescending(i => i.Position),

                _ => order == Order.Ascending
                    ? query.OrderBy(i => i.From)
                    .OrderBy(i => i.To)
                    : query.OrderByDescending(i => i.From)
                    .ThenByDescending(i => i.To),
            };
        });
        Paginate(pagination);
        return this;
    }
}
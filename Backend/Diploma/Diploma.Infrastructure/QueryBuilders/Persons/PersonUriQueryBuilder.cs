using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Diploma.Shared.PersonEvents;
using Microsoft.EntityFrameworkCore;
using static Diploma.Models.PersonUris.PersonUriQueryParameters;
using DatabasePersonUri = Diploma.Database.Models.Persons.PersonEvents.Audits.PersonUri;

namespace Diploma.Infrastructure.QueryBuilders.Persons;

public class PersonUriQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabasePersonUri>(
    context
    .PersonUris
    .AsNoTracking()
    .Include(i => i.PersonEvent)
    .Include(i => i.Root)
    .ThenInclude(i => i!.PersonEvent)
    .Where(i => i.NextId == null && i.PersonEvent.PersonEventTypeId != PersonEvent.DeleteUri.Id)
    )
{
    public PersonUriQueryBuilder WithPersonId(PersonId item)
    {
        With(query => query.Where(i => i.PersonEvent.PersonId == item.Value));
        return this;
    }

    public PersonUriQueryBuilder WithOrderBy(
        Order order,
        PersonUriOrderBy orderBy,
        QueryParametersPagination pagination)
    {
        With(query =>
        {
            return orderBy switch
            {
                PersonUriOrderBy.Name => order == Order.Ascending
                    ? query.OrderBy(i => i.Name)
                    : query.OrderByDescending(i => i.Name),

                _ => order == Order.Ascending
                    ? query.OrderBy(i => i.Root != null
                        ? i.Root.PersonEvent.CreatedAt
                        : i.PersonEvent.CreatedAt)
                    : query.OrderByDescending(i => i.Root != null
                        ? i.Root.PersonEvent.CreatedAt
                        : i.PersonEvent.CreatedAt),
            };
        });
        Paginate(pagination);
        return this;
    }
}
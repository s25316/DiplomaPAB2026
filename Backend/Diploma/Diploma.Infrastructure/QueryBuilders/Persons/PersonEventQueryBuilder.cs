using Diploma.Database;
using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.QueryBuilders.Persons;

public class PersonEventQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<PersonEvent>(
    context
    .PersonEvents
    .AsNoTracking()
    )
{
    public PersonEventQueryBuilder WithPersonId(PersonId item)
    {
        With(query => query.Where(i => i.PersonId == item.Value));
        return this;
    }


    public PersonEventQueryBuilder WithOrderBy(QueryParametersPagination pagination)
    {
        With(query => query.OrderBy(i => i.CreatedAt));
        Paginate(pagination);
        return this;
    }
}
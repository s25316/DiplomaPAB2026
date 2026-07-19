using Diploma.Database;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;
using DatabasePersonOperation = Diploma.Database.Models.Persons.PersonOperations.PersonOperation;
using SharedPersonOperation = Diploma.Shared.PersonOperations.PersonOperation;

namespace Diploma.Infrastructure.QueryBuilders.Persons;

public class PersonOperationQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabasePersonOperation>(
    context
    .PersonOperations
    .AsNoTracking()
    )
{
    public PersonOperationQueryBuilder WithPersonId(Guid item)
    {
        With(query => query.Where(i => i.PersonId == item));
        return this;
    }

    public PersonOperationQueryBuilder WithPersonOperations(IEnumerable<SharedPersonOperation> items)
    {
        if (!items.Any())
            return this;

        var ids = items
            .Select(i => i.Id)
            .ToHashSet();

        With(query => query.Where(i => ids.Contains(i.PersonOperationTypeId)));
        return this;
    }

    public PersonOperationQueryBuilder WithOrderBy(
        Order order,
        QueryParametersPagination pagination)
    {
        With(query =>
        {
            return order == Order.Ascending
                ? query.OrderBy(i => i.CreatedAt)
                : query.OrderByDescending(i => i.CreatedAt);

        });
        Paginate(pagination);
        return this;
    }
}
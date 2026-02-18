// Ignore Spelling: Wojewodztwo
using Base.Models.Extensions;
using Base.Models.Interfaces.QueryBuilders;
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.QueryBuilders;

public class WojewodztwoQueryBuilder(TerytDbContext context) : BaseQueryBuilder<Wojewodztwo>(
    context.Wojewodztwa.AsNoTracking())
{
    public WojewodztwoQueryBuilder WithSearchText(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var words = value.SplitSearchText();
            With(query => query.Where(i => words.Any(w => i.Name.Contains(w))));
        }
        return this;
    }

    public WojewodztwoQueryBuilder WithIds(IEnumerable<WojewodztwoId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.Value);
            With(query => query.Where(i => ids.Contains(i.WojewodztwoCode)));
        }
        return this;
    }

    public WojewodztwoQueryBuilder WithPagination(Pagination pagination, Order order, WojewodztwoOrderBy orderBy)
    {
        With(query =>
        {
            var orderedQuery = orderBy switch
            {
                WojewodztwoOrderBy.Name => order == Order.Ascending
                    ? query.OrderBy(i => i.Name)
                    : query.OrderByDescending(i => i.Name),

                WojewodztwoOrderBy.Id => order == Order.Ascending
                    ? query.OrderBy(i => i.WojewodztwoCode)
                    : query.OrderByDescending(i => i.WojewodztwoCode),

                _ => throw new NotImplementedException(orderBy.ToString())
            };

            return orderedQuery.Paginate(pagination);
        });
        return this;
    }
}
// Ignore Spelling: Powiat, Wojewodztwo
using Base.Models.Extensions;
using Base.Models.Interfaces.QueryBuilders;
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.QueryBuilders;

public class PowiatQueryBuilder(TerytDbContext context) : BaseQueryBuilder<Powiat>(context
    .Powiaty
    .AsNoTracking()
    .Include(i => i.Type))
{
    public PowiatQueryBuilder WithSearchText(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var words = value.SplitSearchText();
            With(query => query.Where(i => words.Any(w => i.Name.Contains(w))));
        }
        return this;
    }

    public PowiatQueryBuilder WithWojewodztwoIds(IEnumerable<WojewodztwoId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(i.WojewodztwoCode)));
        }
        return this;
    }

    public PowiatQueryBuilder WithIds(IEnumerable<PowiatId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(i.WojewodztwoCode + "." + i.PowiatCode)));
        }
        return this;
    }

    public PowiatQueryBuilder WithTypeIds(IEnumerable<int> items)
    {
        if (items.Any())
        {
            With(query => query.Where(i => items.Contains(i.TypeCode)));
        }
        return this;
    }

    public PowiatQueryBuilder WithPagination(Pagination pagination, Order order, PowiatOrderBy orderBy)
    {
        With(query =>
        {
            var orderedQuery = orderBy switch
            {
                PowiatOrderBy.Name => order == Order.Ascending
                    ? query.OrderBy(i => i.Name)
                    : query.OrderByDescending(i => i.Name),

                PowiatOrderBy.Id => order == Order.Ascending
                    ? query.OrderBy(i => i.WojewodztwoCode)
                        .ThenBy(i => i.PowiatCode)
                    : query.OrderByDescending(i => i.WojewodztwoCode)
                        .ThenByDescending(i => i.PowiatCode),

                _ => throw new NotImplementedException(orderBy.ToString())
            };

            return orderedQuery.Paginate(pagination);
        });
        return this;
    }
}
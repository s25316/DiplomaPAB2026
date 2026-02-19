// Ignore Spelling: Ulica, Miejscowosc
using Base.Models.Extensions;
using Base.Models.Interfaces.QueryBuilders;
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Database.Models.Ulicy;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;
using GUS.TERYT.Models.Requests.ValueObjects.Ulicy;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.QueryBuilders;

public class UlicaQueryBuilder(TerytDbContext context) : BaseQueryBuilder<Ulica>(context
    .Ulicy
    .AsNoTracking()
    .Include(i => i.Type)
    .Include(i => i.SimcUlica))
{
    public UlicaQueryBuilder WithSearchText(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var words = value.SplitSearchText();
            With(query => query.Where(i => words.Any(w => i.Name.Contains(w))));
        }
        return this;
    }

    public UlicaQueryBuilder WithMiejscowoscIds(IEnumerable<MiejscowoscId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => i.SimcUlica.Any(si => ids.Contains(si.MiejscowoscCode))));
        }
        return this;
    }

    public UlicaQueryBuilder WithIds(IEnumerable<UlicaId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(i.UlicaCode)));
        }
        return this;
    }

    public UlicaQueryBuilder WithTypeIds(IEnumerable<int> items)
    {
        if (items.Any())
        {
            With(query => query.Where(i =>
                i.TypeCode != null &&
                items.Any(id => id == i.TypeCode)));
        }
        return this;
    }

    public UlicaQueryBuilder WithPagination(Pagination pagination, Order order, UlicaOrderBy orderBy)
    {
        With(query =>
        {
            var orderedQuery = orderBy switch
            {
                UlicaOrderBy.Name => order == Order.Ascending
                    ? query.OrderBy(i => i.Name)
                    : query.OrderByDescending(i => i.Name),

                UlicaOrderBy.Id => order == Order.Ascending
                    ? query.OrderBy(i => i.UlicaCode)
                    : query.OrderByDescending(i => i.UlicaCode),

                _ => throw new NotImplementedException(orderBy.ToString())
            };

            return orderedQuery.Paginate(pagination);
        });
        return this;
    }
}
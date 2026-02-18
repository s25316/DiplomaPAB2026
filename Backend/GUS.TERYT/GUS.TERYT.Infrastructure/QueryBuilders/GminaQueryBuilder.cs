// Ignore Spelling: Gmina, Powiat, Wojewodztwo
using Base.Models.Extensions;
using Base.Models.Interfaces.QueryBuilders;
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Database.Models.Tercs;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Gminy;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.QueryBuilders;

public class GminaQueryBuilder(TerytDbContext context) : BaseQueryBuilder<Gmina>(context
    .Gminy
    .AsNoTracking()
    .Include(i => i.Rodz)
    .Include(i => i.Powiat))
{
    public GminaQueryBuilder WithSearchText(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var words = value.SplitSearchText();
            With(query => query.Where(i => words.Any(w => i.Name.Contains(w))));
        }
        return this;
    }

    public GminaQueryBuilder WithWojewodztwoIds(IEnumerable<WojewodztwoId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Any(id => id == i.Powiat.WojewodztwoCode)));
        }
        return this;
    }

    public GminaQueryBuilder WithPowiatIds(IEnumerable<PowiatId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(i.Powiat.WojewodztwoCode + "." + i.Powiat.PowiatCode)));
        }
        return this;
    }

    public GminaQueryBuilder WithIds(IEnumerable<GminaId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(
                i.Powiat.WojewodztwoCode + "." + i.Powiat.PowiatCode + "." + i.GminaCode + i.GminaRodzCode)));
        }
        return this;
    }

    public GminaQueryBuilder WithTypeIds(IEnumerable<GminaTypeId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(i.GminaRodzCode)));
        }
        return this;
    }


    public GminaQueryBuilder WithPagination(Pagination pagination, Order order, GminaOrderBy orderBy)
    {
        With(query =>
        {
            var orderedQuery = orderBy switch
            {
                GminaOrderBy.Name => order == Order.Ascending
                    ? query.OrderBy(i => i.Name)
                    : query.OrderByDescending(i => i.Name),

                GminaOrderBy.Id => order == Order.Ascending
                    ? query.OrderBy(i => i.Powiat.WojewodztwoCode)
                        .ThenBy(i => i.Powiat.PowiatCode)
                        .ThenBy(i => i.GminaCode)
                        .ThenBy(i => i.GminaRodzCode)

                    : query.OrderByDescending(i => i.Powiat.WojewodztwoCode)
                        .ThenByDescending(i => i.Powiat.PowiatCode)
                        .ThenByDescending(i => i.GminaCode)
                        .ThenByDescending(i => i.GminaRodzCode),

                _ => throw new NotImplementedException(orderBy.ToString())
            };

            return orderedQuery.Paginate(pagination);
        });
        return this;
    }
}
// Ignore Spelling: Miejscowosc, Wojewodztwo, Gmina
using Base.Models.Extensions;
using Base.Models.Interfaces.QueryBuilders;
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Database.Models.Simcs;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Requests.ValueObjects.Gminy;
using GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.QueryBuilders;

public class MiejscowoscQueryBuilder(TerytDbContext context) : BaseQueryBuilder<Simc>(context
    .Miejscowosci
    .AsNoTracking()
    .Include(i => i.Type)
    .Include(i => i.Gmina)
    .ThenInclude(i => i.Powiat))
{
    public MiejscowoscQueryBuilder WithSearchText(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var words = value.SplitSearchText();
            With(query => query.Where(i => words.Any(w => i.Name.Contains(w))));
        }
        return this;
    }

    public MiejscowoscQueryBuilder WithWojewodztwoIds(IEnumerable<WojewodztwoId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Any(id => id == i.Gmina.Powiat.WojewodztwoCode)));
        }
        return this;
    }

    public MiejscowoscQueryBuilder WithPowiatIds(IEnumerable<PowiatId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(i.Gmina.Powiat.WojewodztwoCode + "." + i.Gmina.Powiat.PowiatCode)));
        }
        return this;
    }

    public MiejscowoscQueryBuilder WithGminaIds(IEnumerable<GminaId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(
                i.Gmina.Powiat.WojewodztwoCode + "." + i.Gmina.Powiat.PowiatCode + "." + i.Gmina.GminaCode + i.Gmina.GminaRodzCode)));
        }
        return this;
    }

    public MiejscowoscQueryBuilder WithIds(IEnumerable<MiejscowoscId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(i.MiejscowoscCode)));
        }
        return this;
    }

    public MiejscowoscQueryBuilder WithTypeIds(IEnumerable<MiejscowoscTypeId> items)
    {
        if (items.Any())
        {
            var ids = items.Select(i => i.ToString());
            With(query => query.Where(i => ids.Contains(i.TypeCode)));
        }
        return this;
    }


    public MiejscowoscQueryBuilder WithPagination(Pagination pagination, Order order, MiejscowoscOrderBy orderBy)
    {
        With(query =>
        {
            var orderedQuery = orderBy switch
            {
                MiejscowoscOrderBy.Name => order == Order.Ascending
                    ? query.OrderBy(i => i.Name)
                    : query.OrderByDescending(i => i.Name),

                MiejscowoscOrderBy.Id => order == Order.Ascending
                    ? query.OrderBy(i => i.MiejscowoscCode)
                    : query.OrderByDescending(i => i.MiejscowoscCode),

                _ => throw new NotImplementedException(orderBy.ToString())
            };

            return orderedQuery.Paginate(pagination);
        });
        return this;
    }
}
using Base.Models.Interfaces.QueryBuilders;
using Base.Models.ValueObjects.Regony;
using GUS.REGON.Database;
using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GUS.REGON.Infrastructure.QueryBuilders;

public class QueryQueryBuilder(RegonDbContext context) : BaseQueryBuilder<Query>(context
    .Queries

    .Include(i => i.Report)
    .ThenInclude(i => i!.TypJednostki)

    .Include(i => i.Report)
    .ThenInclude(i => i!.FormaFinansowania)

    .Include(i => i.Report)
    .ThenInclude(i => i!.FormaWlasnosci)

    .Include(i => i.Report)
    .ThenInclude(i => i!.OrganRejestrowy)

    .Include(i => i.Report)
    .ThenInclude(i => i!.OrganZalozycielski)

    .Include(i => i.Report)
    .ThenInclude(i => i!.PodstawowaFormaPrawna)

    .Include(i => i.Report)
    .ThenInclude(i => i!.SzczegolnaFormaPrawna)

    .Include(i => i.Report)
    .ThenInclude(i => i!.RodzajRejestru)

    .Include(i => i.Report)
    .ThenInclude(i => i!.Address)
    .ThenInclude(i => i!.Kraj)

    .Include(i => i.Report)
    .ThenInclude(i => i!.Address)
    .ThenInclude(i => i!.Wojewodztwo)

    .Include(i => i.Report)
    .ThenInclude(i => i!.Address)
    .ThenInclude(i => i!.Powiat)

    .Include(i => i.Report)
    .ThenInclude(i => i!.Address)
    .ThenInclude(i => i!.Gmina)

    .Include(i => i.Report)
    .ThenInclude(i => i!.Address)
    .ThenInclude(i => i!.MiejscowoscPoczty)

    .Include(i => i.Report)
    .ThenInclude(i => i!.Address)
    .ThenInclude(i => i!.Miejscowosc)

    .Include(i => i.Report)
    .ThenInclude(i => i!.Address)
    .ThenInclude(i => i!.Ulica))
{
    public QueryQueryBuilder WithRegons(IEnumerable<Regon> items)
    {
        if (items.Any())
        {
            var ids = items
                .Select(i => i.To14SCharacters());
            With(query => query.Where(i => ids.Contains(i.Regon)));
        }
        return this;
    }

    public QueryQueryBuilder WithAsNoTracking()
    {
        With(query => query.AsNoTracking());
        return this;
    }
}
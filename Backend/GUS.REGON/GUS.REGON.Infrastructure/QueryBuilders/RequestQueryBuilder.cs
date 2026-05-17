using Base.Models.Interfaces.QueryBuilders;
using Base.Models.ValueObjects.Regony;
using GUS.REGON.Database;
using GUS.REGON.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GUS.REGON.Infrastructure.QueryBuilders;

public class RequestQueryBuilder : BaseQueryBuilder<Request>
{
    private RequestQueryBuilder(IQueryable<Request> query) : base(query) { }
    public RequestQueryBuilder(RegonDbContext context) : this(context.Requests
        .Include(i => i.RequestStatus)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.TypJednostki)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.FormaFinansowania)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.FormaWlasnosci)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.OrganRejestrowy)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.OrganZalozycielski)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.PodstawowaFormaPrawna)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.SzczegolnaFormaPrawna)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.RodzajRejestru)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.Address)
        .ThenInclude(i => i!.Kraj)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.Address)
        .ThenInclude(i => i!.Wojewodztwo)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.Address)
        .ThenInclude(i => i!.Powiat)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.Address)
        .ThenInclude(i => i!.Gmina)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.Address)
        .ThenInclude(i => i!.MiejscowoscPoczty)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.Address)
        .ThenInclude(i => i!.Miejscowosc)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.Address)
        .ThenInclude(i => i!.Ulica)

        .Include(i => i.Institution)
        .ThenInclude(i => i!.Pkds)
        .ThenInclude(i => i!.Pkd))
    { }

    public RequestQueryBuilder WithRegons(IEnumerable<Regon> items)
    {
        if (items.Any())
        {
            var ids = items
                .Select(i => i.To14SCharacters());
            With(query => query.Where(i => ids.Contains(i.Regon)));
        }
        return new RequestQueryBuilder(query);
    }

    public RequestQueryBuilder WithAsNoTracking()
    {
        With(query => query.AsNoTracking());
        return new RequestQueryBuilder(query);
    }
}
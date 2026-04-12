//Ignore Spelling: Regon
using Base.Models.ValueObjects.Regony;
using GUS.REGON.Application.Interfaces;
using GUS.REGON.Database;
using GUS.REGON.Database.Models;
using GUS.REGON.Infrastructure.Configurations;
using GUS.REGON.Infrastructure.Interfaces;
using GUS.REGON.Infrastructure.QueryBuilders;
using GUS.REGON.Models.Requests;
using GUS.REGON.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ResponseReport = GUS.REGON.Models.Responses.Report;

namespace GUS.REGON.Infrastructure.Repositories;

public class ReportRepository(
    RegonDbContext context,
    IOptions<UpdateDataConfiguration> updateDataOptions,
    IQueryRepository queryRepository
) : IReportRepository
{
    public async Task<IEnumerable<Result>> GetAsync(InputParameters parameters, CancellationToken cancellationToken)
    {
        if (!parameters.Regons.Any())
        {
            return [];
        }

        var regons = parameters.Regons.ToHashSet();
        var regonStrings = regons.Select(i => i.To14SCharacters()).ToHashSet();
        var dbDictionary = await GetQueryDictionary(regons, cancellationToken);

        var today = GetDateOnly();
        var notExistingRegons = regonStrings
            .Except(dbDictionary.Keys)
            .Select(Regon.Parse);
        var updatingRegons = dbDictionary
            .Where(i => i.Value.LastUpdate != today)
            .Select(i => Regon.Parse(i.Key));

        foreach (var regon in notExistingRegons)
        {
            await queryRepository.CreateAsync(regon);
        }

        foreach (var regon in updatingRegons)
        {
            await queryRepository.UpdateAsync(regon);
        }

        if (notExistingRegons.Any() || updatingRegons.Any())
        {
            dbDictionary = await GetQueryDictionary(regons, cancellationToken);
        }

        return dbDictionary.Values.Select(Map);
    }

    private static DateOnly GetDateOnly() => DateOnly.FromDateTime(DateTimeOffset.Now.Date);

    private async Task<Dictionary<string, Query>> GetQueryDictionary(IEnumerable<Regon> regons, CancellationToken cancellationToken = default)
    {
        var query = new QueryQueryBuilder(context)
            .WithRegons(regons)
            .WithAsNoTracking()
            .Build();
        return await query.ToDictionaryAsync(
            k => k.Regon,
            v => v,
            cancellationToken);
    }

    private static Result Map(Query item)
    {
        var organRejestrowy = item.Report?.OrganRejestrowy is null
            ? null
            : new ResponseReport.Pair(
                item.Report.OrganRejestrowy.OrganRejestrowyId,
                item.Report.OrganRejestrowy.Name);
        var rodzajRejestru = item.Report?.RodzajRejestru is null
            ? null
            : new ResponseReport.Pair(
                item.Report.RodzajRejestru.RodzajRejestruId,
                item.Report.RodzajRejestru.Name);
        var formaFinansowania = item.Report?.FormaFinansowania is null
            ? null
            : new ResponseReport.Pair(
                item.Report.FormaFinansowania.FormaFinansowaniaId,
                item.Report.FormaFinansowania.Name);
        var podstawowaFormaPrawna = item.Report?.PodstawowaFormaPrawna is null
            ? null
            : new ResponseReport.Pair(
                item.Report.PodstawowaFormaPrawna.PodstawowaFormaPrawnaId,
                item.Report.PodstawowaFormaPrawna.Name);
        var szczegolnaFormaPrawna = item.Report?.SzczegolnaFormaPrawna is null
            ? null
            : new ResponseReport.Pair(
                item.Report.SzczegolnaFormaPrawna.SzczegolnaFormaPrawnaId,
                item.Report.SzczegolnaFormaPrawna.Name);
        var organZalozycielski = item.Report?.OrganZalozycielski is null
            ? null
            : new ResponseReport.Pair(
                item.Report.OrganZalozycielski.OrganZalozycielskiId,
                item.Report.OrganZalozycielski.Name);
        var formaWlasnosci = item.Report?.FormaWlasnosci is null
            ? null
            : new ResponseReport.Pair(
                item.Report.FormaWlasnosci.FormaWlasnosciId,
                item.Report.FormaWlasnosci.Name);
        var ulica = item.Report?.Address?.Ulica is null
            ? null
            : new ResponseReport.Pair(
            item.Report.Address.Ulica.UlicaId,
            item.Report.Address.Ulica.Name);
        var address = item.Report?.Address is null
            ? null
            : new ResponseReport.Address
            {
                KodPocztowy = item.Report.Address.KodPocztowy,
                NumerNieruchomosci = item.Report.Address.NumerNieruchomosci,
                NumerLokalu = item.Report.Address.NumerLokalu,
                NietypoweMiejsceLokalizacji = item.Report.Address.NietypoweMiejsceLokalizacji,
                Kraj = new ResponseReport.Pair(
                    item.Report.Address.Kraj.KrajId,
                    item.Report.Address.Kraj.Name
                    ),
                Wojewodztwo = new ResponseReport.Pair(
                    item.Report.Address.Wojewodztwo.WojewodztwoId,
                    item.Report.Address.Wojewodztwo.Name
                    ),
                Powiat = new ResponseReport.Pair(
                    item.Report.Address.Powiat.PowiatId,
                    item.Report.Address.Powiat.Name
                    ),
                Gmina = new ResponseReport.Pair(
                    item.Report.Address.Gmina.GminaId,
                    item.Report.Address.Gmina.Name
                    ),
                MiejscowoscPoczty = new ResponseReport.Pair(
                    item.Report.Address.MiejscowoscPoczty.MiejscowoscPocztyId,
                    item.Report.Address.MiejscowoscPoczty.Name),
                Miejscowosc = new ResponseReport.Pair(
                    item.Report.Address.Miejscowosc.MiejscowoscId,
                    item.Report.Address.Miejscowosc.Name
                    ),
                Ulica = ulica,
            };

        return new Result
        {
            Regon = item.Regon,
            Report = item.Report is null
            ? null
            : new ResponseReport
            {
                Regon = item.Report.Regon,
                Nazwa = item.Report.Nazwa,
                NazwaSkrocona = item.Report.NazwaSkrocona,
                NumerwRejestrzeEwidencji = item.Report.NumerwRejestrzeEwidencji,
                Dzialalnosci = item.Report.Dzialalnosci,
                Daty = new ResponseReport.Dates
                {
                    DataPowstania = item.Report.DataPowstania,
                    DataRozpoczecia = item.Report.DataRozpoczecia,
                    DataWpisu = item.Report.DataWpisu,
                    DataZawieszenia = item.Report.DataZawieszenia,
                    DataWznowienia = item.Report.DataWznowienia,
                    DataZmiany = item.Report.DataZmiany,
                    DataZakonczenia = item.Report.DataZakonczenia,
                    DataSkreslenia = item.Report.DataSkreslenia,
                    DataWpisuDoRejestruEwidencji = item.Report.DataWpisuDoRejestruEwidencji,
                },
                OrganRejestrowy = organRejestrowy,
                RodzajRejestru = rodzajRejestru,
                FormaFinansowania = formaFinansowania,
                PodstawowaFormaPrawna = podstawowaFormaPrawna,
                SzczegolnaFormaPrawna = szczegolnaFormaPrawna,
                OrganZalozycielski = organZalozycielski,
                FormaWlasnosci = formaWlasnosci,
                Adres = address,
            },
        };
    }
}
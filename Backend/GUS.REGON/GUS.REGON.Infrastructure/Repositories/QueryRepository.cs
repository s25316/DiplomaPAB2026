//Ignore Spelling: Regon
using Base.Models.ValueObjects.Regony;
using GUS.REGON.Database;
using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.RegistrationDetails;
using GUS.REGON.Infrastructure.Interfaces;
using GUS.REGON.Infrastructure.QueryBuilders;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.Models.Results;
using Microsoft.EntityFrameworkCore;
using DaneSzukaj = GUS.REGON.Models.DaneSzukaj;
using DatabaseReport = GUS.REGON.Database.Models.Report;
using RegonAddress = GUS.REGON.Models.RaportJednostki.Address;
using RegonRaportJednostki = GUS.REGON.Models.RaportJednostki;
using RegonTypJednostki = GUS.REGON.Models.Responses.Enums.TypJednostki;

namespace GUS.REGON.Infrastructure.Repositories;

public class QueryRepository(
    RegonDbContext context,
    RegonService regonService,
    IAddressRepository addressRepository) : IQueryRepository
{
    public async Task CreateAsync(Regon regon, CancellationToken cancellationToken = default)
    {
        var databaseQuery = new Query
        {
            Regon = regon.To14SCharacters(),
            LastUpdate = GetDateOnly(),
        };
        await context.Queries.AddAsync(databaseQuery, cancellationToken);

        var daneSzukaj = await GetDaneSzukajAsync(regon, cancellationToken);
        if (daneSzukaj is null)
        {
            await context.SaveChangesAsync();
            return;
        }

        var report = await GetRaportJednostkiAsync(regon, daneSzukaj.Typ, daneSzukaj.SilosId, cancellationToken);
        if (report is null)
        {
            return;
        }

        var databaseReport = new DatabaseReport
        {
            Regon = regon.To14SCharacters(),
            Query = databaseQuery,
            SilosId = daneSzukaj.SilosId,
        };

        await SetTypJednostkiAsync(databaseReport, daneSzukaj.Typ);
        await UpdateReportAsync(databaseReport, report, cancellationToken);

        await context.Reports.AddAsync(databaseReport);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Regon regon, CancellationToken cancellationToken = default)
    {
        var query = new QueryQueryBuilder(context)
            .WithRegons([regon])
            .Build();
        var databaseQuery = await query.FirstAsync(cancellationToken);
        databaseQuery.LastUpdate = GetDateOnly();

        var daneSzukajItems = await GetValueAsync(() => regonService.GetDaneSzukajAsync(
            regon,
            cancellationToken));

        if (daneSzukajItems is null || !daneSzukajItems.Any())
        {
            await context.SaveChangesAsync(cancellationToken);
            return;
        }

        var daneSzukaj = daneSzukajItems.First();
        var report = await GetValueAsync(() => regonService.GetRaportJednostkiAsync(
            regon,
            daneSzukaj.Typ,
            daneSzukaj.SilosId,
            cancellationToken));
        if (report is null)
        {
            return;
        }

        if (databaseQuery.Report is null)
        {
            databaseQuery.Report = new DatabaseReport
            {
                Regon = Regon.Parse(report.Regon).To14SCharacters(),
                Query = databaseQuery,
                SilosId = daneSzukaj.SilosId,
            };
            await SetTypJednostkiAsync(databaseQuery.Report, daneSzukaj.Typ);
        }

        await UpdateReportAsync(databaseQuery.Report, report, cancellationToken);
        await context.SaveChangesAsync();
    }

    private static DateOnly GetDateOnly() => DateOnly.FromDateTime(DateTimeOffset.Now.Date);

    private static async Task<T?> GetValueAsync<T>(Func<Task<RegonResult<T>>> func)
    {
        var result = await func();
        switch (result)
        {
            case { IsSuccess: true }:
                return result.Value;

            case { IsFailure: true, KomunikatKod: KomunikatKod.NieZnalezionoPodmiotów }:
            case { IsFailure: true, KomunikatKod: KomunikatKod.BrakUprawnienDoRaportu }:
                return default;

            case { IsFailure: true, KomunikatKod: KomunikatKod.KodCaptcha }:
            case { IsFailure: true, KomunikatKod: KomunikatKod.BrakSesji }:
            case { IsFailure: true, KomunikatKod: KomunikatKod.DaneSzukajWieleIdentyfikatorow }:
                throw new Exception(result.KomunikatKod.ToString());

            case { IsFailure: true, StatusUslugi: not StatusUslugi.UslugaDostepna }:
                throw new Exception("Service not avaliable"); //TODO

            default: throw new NotImplementedException(nameof(result));
        }
    }

    private async Task<DaneSzukaj?> GetDaneSzukajAsync(Regon regon, CancellationToken cancellationToken = default)
    {
        var items = await GetValueAsync(() => regonService.GetDaneSzukajAsync(regon, cancellationToken));
        return items?.FirstOrDefault();
    }

    private async Task<RegonRaportJednostki?> GetRaportJednostkiAsync(
        Regon regon,
        RegonTypJednostki typ,
        int? silosId,
        CancellationToken cancellationToken = default
    ) => await GetValueAsync(() => regonService.GetRaportJednostkiAsync(regon, typ, silosId, cancellationToken));


    private async Task<DatabaseReport> UpdateReportAsync(DatabaseReport report, RegonRaportJednostki raportJednostki, CancellationToken cancellationToken = default)
    {
        await SetAddressAsync(report, raportJednostki.Adres, cancellationToken);
        await SetFormaFinansowaniaAsync(report, raportJednostki.FormaFinansowania);
        await SetFormaWlasnosciAsync(report, raportJednostki.FormaWlasnosci);
        await SetOrganRejestrowyAsync(report, raportJednostki.OrganRejestrowy);
        await SetOrganZalozycielskiAsync(report, raportJednostki.OrganZalozycielski);
        await SetPodstawowaFormaPrawnaAsync(report, raportJednostki.PodstawowaFormaPrawna);
        await SetSzczegolnaFormaPrawnaAsync(report, raportJednostki.SzczegolnaFormaPrawna);
        await SetRodzajRejestruAsync(report, raportJednostki.RodzajRejestru);

        report.Nazwa = raportJednostki.Nazwa;
        report.NazwaSkrocona = raportJednostki.NazwaSkrocona;
        report.Nazwa = raportJednostki.Nazwa;
        report.NumerwRejestrzeEwidencji = raportJednostki.NumerwRejestrzeEwidencji;
        report.Dzialalnosci = raportJednostki.Dzialalnosci;

        report.DataPowstania = raportJednostki.Daty.DataPowstania;
        report.DataRozpoczecia = raportJednostki.Daty.DataRozpoczecia;
        report.DataWpisu = raportJednostki.Daty.DataWpisu;
        report.DataZawieszenia = raportJednostki.Daty.DataZawieszenia;
        report.DataWznowienia = raportJednostki.Daty.DataWznowienia;
        report.DataZmiany = raportJednostki.Daty.DataZmiany;
        report.DataZakonczenia = raportJednostki.Daty.DataZakonczenia;
        report.DataSkreslenia = raportJednostki.Daty.DataSkreslenia;
        report.DataWpisuDoRejestruEwidencji = raportJednostki.Daty.DataWpisuDoRejestruEwidencji;

        return report;
    }

    private async Task SetTypJednostkiAsync(DatabaseReport database, RegonTypJednostki typJednostki)
    {
        var typJednostkiString = typJednostki.ToString();
        var dbItem = await context.TypyJednostki.FirstOrDefaultAsync(i => i.TypJednostkiId == typJednostkiString);
        if (dbItem is not null)
        {
            database.TypJednostki = dbItem;
            return;
        }
        throw new NotImplementedException(typJednostkiString);
    }

    private async Task SetAddressAsync(DatabaseReport database, RegonAddress? input, CancellationToken cancellationToken = default)
    {
        if (input is null)
        {
            database.Address = null;
            return;
        }

        if (database.Address is not null &&
            database.Address.KrajId == input.Kraj.Symbol &&
            database.Address.WojewodztwoId == input.Wojewodztwo.Symbol &&
            database.Address.PowiatId == input.Powiat.Symbol &&
            database.Address.GminaId == input.Gmina.Symbol &&
            database.Address.MiejscowoscPocztyId == input.MiejscowoscPoczty.Symbol &&
            database.Address.MiejscowoscId == input.Miejscowosc.Symbol &&
            database.Address.UlicaId == input.Ulica?.Symbol &&
            database.Address.KodPocztowy == input.KodPocztowy &&
            database.Address.NumerNieruchomosci == input.NumerNieruchomosci &&
            database.Address.NumerLokalu == input.NumerLokalu &&
            database.Address.NietypoweMiejsceLokalizacji == input.NietypoweMiejsceLokalizacji)
        {
            return;
        }

        var address = await addressRepository.GetAsync(input, cancellationToken);
        database.Address = address;
    }

    private async Task SetFormaFinansowaniaAsync(DatabaseReport database, RegonRaportJednostki.Pair? input)
    {
        if (input is null)
        {
            database.FormaFinansowania = null;
            return;
        }

        if (database.FormaFinansowania is not null &&
            database.FormaFinansowaniaId == input.Symbol)
        {
            return;
        }

        var dbItem = await context.FormyFinansowania.FirstOrDefaultAsync(i => i.FormaFinansowaniaId == input.Symbol);
        if (dbItem is not null)
        {
            database.FormaFinansowania = dbItem;
            return;
        }

        dbItem = new FormaFinansowania
        {
            FormaFinansowaniaId = input.Symbol,
            Name = input.Nazwa,
        };
        await context.FormyFinansowania.AddAsync(dbItem);
        database.FormaFinansowania = dbItem;
    }

    private async Task SetFormaWlasnosciAsync(DatabaseReport database, RegonRaportJednostki.Pair? input)
    {
        if (input is null)
        {
            database.FormaWlasnosci = null;
            return;
        }

        if (database.FormaWlasnosci is not null &&
            database.FormaWlasnosciId == input.Symbol)
        {
            return;
        }

        var dbItem = await context.FormyWlasnosci.FirstOrDefaultAsync(i => i.FormaWlasnosciId == input.Symbol);
        if (dbItem is not null)
        {
            database.FormaWlasnosci = dbItem;
            return;
        }

        dbItem = new FormaWlasnosci
        {
            FormaWlasnosciId = input.Symbol,
            Name = input.Nazwa,
        };
        await context.FormyWlasnosci.AddAsync(dbItem);
        database.FormaWlasnosci = dbItem;
    }

    private async Task SetOrganRejestrowyAsync(DatabaseReport database, RegonRaportJednostki.Pair? input)
    {
        if (input is null)
        {
            database.OrganRejestrowy = null;
            return;
        }

        if (database.OrganRejestrowy is not null &&
            database.OrganRejestrowyId == input.Symbol)
        {
            return;
        }

        var dbItem = await context.OrganyRejestrowe.FirstOrDefaultAsync(i => i.OrganRejestrowyId == input.Symbol);
        if (dbItem is not null)
        {
            database.OrganRejestrowy = dbItem;
            return;
        }

        dbItem = new OrganRejestrowy
        {
            OrganRejestrowyId = input.Symbol,
            Name = input.Nazwa,
        };
        await context.OrganyRejestrowe.AddAsync(dbItem);
        database.OrganRejestrowy = dbItem;
    }

    private async Task SetOrganZalozycielskiAsync(DatabaseReport database, RegonRaportJednostki.Pair? input)
    {
        if (input is null)
        {
            database.OrganZalozycielski = null;
            return;
        }

        if (database.OrganZalozycielski is not null &&
            database.OrganZalozycielskiId == input.Symbol)
        {
            return;
        }

        var dbItem = await context.OrganyZalozycielskie.FirstOrDefaultAsync(i => i.OrganZalozycielskiId == input.Symbol);
        if (dbItem is not null)
        {
            database.OrganZalozycielski = dbItem;
            return;
        }

        dbItem = new OrganZalozycielski
        {
            OrganZalozycielskiId = input.Symbol,
            Name = input.Nazwa,
        };
        await context.OrganyZalozycielskie.AddAsync(dbItem);
        database.OrganZalozycielski = dbItem;
    }

    private async Task SetPodstawowaFormaPrawnaAsync(DatabaseReport database, RegonRaportJednostki.Pair? input)
    {
        if (input is null)
        {
            database.PodstawowaFormaPrawna = null;
            return;
        }

        if (database.PodstawowaFormaPrawna is not null &&
            database.PodstawowaFormaPrawnaId == input.Symbol)
        {
            return;
        }

        var dbItem = await context.PodstawowwFormyPrawne.FirstOrDefaultAsync(i => i.PodstawowaFormaPrawnaId == input.Symbol);
        if (dbItem is not null)
        {
            database.PodstawowaFormaPrawna = dbItem;
            return;
        }

        dbItem = new PodstawowaFormaPrawna
        {
            PodstawowaFormaPrawnaId = input.Symbol,
            Name = input.Nazwa,
        };
        await context.PodstawowwFormyPrawne.AddAsync(dbItem);
        database.PodstawowaFormaPrawna = dbItem;
    }

    private async Task SetSzczegolnaFormaPrawnaAsync(DatabaseReport database, RegonRaportJednostki.Pair? input)
    {
        if (input is null)
        {
            database.SzczegolnaFormaPrawna = null;
            return;
        }

        if (database.SzczegolnaFormaPrawna is not null &&
            database.SzczegolnaFormaPrawnaId == input.Symbol)
        {
            return;
        }

        var dbItem = await context.SzczegolneFormyPrawne.FirstOrDefaultAsync(i => i.SzczegolnaFormaPrawnaId == input.Symbol);
        if (dbItem is not null)
        {
            database.SzczegolnaFormaPrawna = dbItem;
            return;
        }

        dbItem = new SzczegolnaFormaPrawna
        {
            SzczegolnaFormaPrawnaId = input.Symbol,
            Name = input.Nazwa,
        };
        await context.SzczegolneFormyPrawne.AddAsync(dbItem);
        database.SzczegolnaFormaPrawna = dbItem;
    }

    private async Task SetRodzajRejestruAsync(DatabaseReport database, RegonRaportJednostki.Pair? input)
    {
        if (input is null)
        {
            database.RodzajRejestru = null;
            return;
        }

        if (database.RodzajRejestru is not null &&
            database.RodzajRejestruId == input.Symbol)
        {
            return;
        }

        var dbItem = await context.RodzajeRejestru.FirstOrDefaultAsync(i => i.RodzajRejestruId == input.Symbol);
        if (dbItem is not null)
        {
            database.RodzajRejestru = dbItem;
            return;
        }

        dbItem = new RodzajRejestru
        {
            RodzajRejestruId = input.Symbol,
            Name = input.Nazwa,
        };
        await context.RodzajeRejestru.AddAsync(dbItem);
        database.RodzajRejestru = dbItem;
    }
}
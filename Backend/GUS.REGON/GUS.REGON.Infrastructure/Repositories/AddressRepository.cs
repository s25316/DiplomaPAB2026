// Ignore Spelling: regon, kraj, wojewodztwo, powiat, gmina, miejscowosc, poczty, ulica
using GUS.REGON.Database;
using GUS.REGON.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using DatabaseAddress = GUS.REGON.Database.Models.Addresses.Address;
using DatabaseGmina = GUS.REGON.Database.Models.Addresses.Gmina;
using DatabaseKraj = GUS.REGON.Database.Models.Addresses.Kraj;
using DatabaseMiejscowosc = GUS.REGON.Database.Models.Addresses.Miejscowosc;
using DatabaseMiejscowoscPoczty = GUS.REGON.Database.Models.Addresses.MiejscowoscPoczty;
using DatabasePowiat = GUS.REGON.Database.Models.Addresses.Powiat;
using DatabaseUlica = GUS.REGON.Database.Models.Addresses.Ulica;
using DatabaseWojewodztwo = GUS.REGON.Database.Models.Addresses.Wojewodztwo;
using RegonAddress = GUS.REGON.Models.RaportJednostki.Address;
using RegonRaportJednostki = GUS.REGON.Models.RaportJednostki;

namespace GUS.REGON.Infrastructure.Repositories;

public class AddressRepository(RegonDbContext dbContext) : IAddressRepository
{
    public async Task<DatabaseAddress> GetAsync(RegonAddress input, CancellationToken cancellationToken = default)
    {
        var regonUlicaId = input.Ulica?.Symbol;
        var address = await dbContext.Addresses.FirstOrDefaultAsync(i =>
            i.KrajId == input.Kraj.Symbol &&
            i.WojewodztwoId == input.Wojewodztwo.Symbol &&
            i.PowiatId == input.Powiat.Symbol &&
            i.GminaId == input.Gmina.Symbol &&
            i.MiejscowoscPocztyId == input.MiejscowoscPoczty.Symbol &&
            i.MiejscowoscId == input.Miejscowosc.Symbol &&
            i.UlicaId == regonUlicaId &&
            i.KodPocztowy == input.KodPocztowy &&
            i.NumerNieruchomosci == input.NumerNieruchomosci &&
            i.NumerLokalu == input.NumerLokalu &&
            i.NietypoweMiejsceLokalizacji == input.NietypoweMiejsceLokalizacji
        );
        if (address is not null)
        {
            return address;
        }

        return await CreateAndReturnAddressAsync(input);
    }

    private async Task<DatabaseAddress> CreateAndReturnAddressAsync(RegonAddress input)
    {
        var database = new DatabaseAddress
        {
            KodPocztowy = input.KodPocztowy,
            NumerNieruchomosci = input.NumerNieruchomosci,
            NumerLokalu = input.NumerLokalu,
            NietypoweMiejsceLokalizacji = input.NietypoweMiejsceLokalizacji,
        };

        await SetKrajAsync(database, input.Kraj);
        await SetWojewodztwoAsync(database, input.Wojewodztwo);
        await SetPowiatAsync(database, input.Powiat);
        await SetGminaAsync(database, input.Gmina);
        await SetMiejscowoscPocztyAsync(database, input.MiejscowoscPoczty);
        await SetMiejscowoscAsync(database, input.Miejscowosc);
        await SetUlicaAsync(database, input.Ulica);

        return database;
    }

    private async Task SetKrajAsync(DatabaseAddress database, RegonRaportJednostki.Pair input)
    {
        var dbItem = await dbContext.Kraje.FirstOrDefaultAsync(i => i.KrajId == input.Symbol);
        if (dbItem is not null)
        {
            database.Kraj = dbItem;
            return;
        }

        dbItem = new DatabaseKraj
        {
            KrajId = input.Symbol,
            Name = input.Nazwa,
        };
        await dbContext.Kraje.AddAsync(dbItem);
        database.Kraj = dbItem;
    }

    private async Task SetWojewodztwoAsync(DatabaseAddress database, RegonRaportJednostki.Pair input)
    {
        var dbItem = await dbContext.Wojewodztwa.FirstOrDefaultAsync(i => i.WojewodztwoId == input.Symbol);
        if (dbItem is not null)
        {
            database.Wojewodztwo = dbItem;
            return;
        }

        dbItem = new DatabaseWojewodztwo
        {
            WojewodztwoId = input.Symbol,
            Name = input.Nazwa,
        };
        await dbContext.Wojewodztwa.AddAsync(dbItem);
        database.Wojewodztwo = dbItem;
    }

    private async Task SetPowiatAsync(DatabaseAddress database, RegonRaportJednostki.Pair input)
    {
        var dbItem = await dbContext.Powiaty.FirstOrDefaultAsync(i => i.PowiatId == input.Symbol);
        if (dbItem is not null)
        {
            database.Powiat = dbItem;
            return;
        }

        dbItem = new DatabasePowiat
        {
            PowiatId = input.Symbol,
            Name = input.Nazwa,
        };
        await dbContext.Powiaty.AddAsync(dbItem);
        database.Powiat = dbItem;
    }

    private async Task SetGminaAsync(DatabaseAddress database, RegonRaportJednostki.Pair input)
    {
        var dbItem = await dbContext.Gminy.FirstOrDefaultAsync(i => i.GminaId == input.Symbol);
        if (dbItem is not null)
        {
            database.Gmina = dbItem;
            return;
        }

        dbItem = new DatabaseGmina
        {
            GminaId = input.Symbol,
            Name = input.Nazwa,
        };
        await dbContext.Gminy.AddAsync(dbItem);
        database.Gmina = dbItem;
    }

    private async Task SetMiejscowoscPocztyAsync(DatabaseAddress database, RegonRaportJednostki.Pair input)
    {
        var dbItem = await dbContext.MiejscowosciPoczty.FirstOrDefaultAsync(i => i.MiejscowoscPocztyId == input.Symbol);
        if (dbItem is not null)
        {
            database.MiejscowoscPoczty = dbItem;
            return;
        }

        dbItem = new DatabaseMiejscowoscPoczty
        {
            MiejscowoscPocztyId = input.Symbol,
            Name = input.Nazwa,
        };
        await dbContext.MiejscowosciPoczty.AddAsync(dbItem);
        database.MiejscowoscPoczty = dbItem;
    }

    private async Task SetMiejscowoscAsync(DatabaseAddress database, RegonRaportJednostki.Pair input)
    {
        var dbItem = await dbContext.Miejscowosci.FirstOrDefaultAsync(i => i.MiejscowoscId == input.Symbol);
        if (dbItem is not null)
        {
            database.Miejscowosc = dbItem;
            return;
        }

        dbItem = new DatabaseMiejscowosc
        {
            MiejscowoscId = input.Symbol,
            Name = input.Nazwa,
        };
        await dbContext.Miejscowosci.AddAsync(dbItem);
        database.Miejscowosc = dbItem;
    }

    private async Task SetUlicaAsync(DatabaseAddress database, RegonRaportJednostki.Pair? input)
    {
        if (input is null)
        {
            database.Ulica = null;
            return;
        }

        var dbItem = await dbContext.Ulicy.FirstOrDefaultAsync(i => i.UlicaId == input.Symbol);
        if (dbItem is not null)
        {
            database.Ulica = dbItem;
            return;
        }

        dbItem = new DatabaseUlica
        {
            UlicaId = input.Symbol,
            Name = input.Nazwa,
        };
        await dbContext.Ulicy.AddAsync(dbItem);
        database.Ulica = dbItem;
    }
}
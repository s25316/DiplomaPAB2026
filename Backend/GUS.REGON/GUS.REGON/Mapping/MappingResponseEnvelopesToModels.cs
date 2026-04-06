using GUS.REGON.Models;
using GUS.REGON.Models.Responses;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GUS.REGON.Mapping;

internal static class MappingResponseEnvelopesToModels
{
    private const string REGEX_REGON_REPLACE_ZEROS = @"^(\d{9})(0{5})$";


    public static DaneSzukaj MapToAdapted(this Response.DaneSzukaj item)
    {
        var regon = Regex.Replace(
            item.Regon,
            REGEX_REGON_REPLACE_ZEROS,
            "$1");

        DaneSzukaj.Address? address = null;
        if (!string.IsNullOrWhiteSpace(item.Wojewodztwo) &&
            !string.IsNullOrWhiteSpace(item.Powiat) &&
            !string.IsNullOrWhiteSpace(item.Gmina) &&
            !string.IsNullOrWhiteSpace(item.Miejscowosc) &&
            !string.IsNullOrWhiteSpace(item.KodPocztowy))
        {
            address = new DaneSzukaj.Address
            {
                Wojewodztwo = item.Wojewodztwo,
                Powiat = item.Powiat,
                Gmina = item.Gmina,
                Miejscowosc = item.Miejscowosc,
                KodPocztowy = item.KodPocztowy,
                Ulica = item.Ulica,
            };
        }

        return new DaneSzukaj
        {
            Regon = regon,
            Nazwa = item.Nazwa,
            Typ = item.Typ,
            SilosId = item.SilosId,
            Adres = address,
        };
    }

    public static RaportPkd MapToAdapted(this Response.RaportPkd item)
    {
        return new RaportPkd
        {
            Kod = item.Kod,
            Nazwa = item.Nazwa,
            IsMain = int.Parse(item.Przewazajace) > 0,
        };
    }

    public static RaportJednostki MapToAdapted(this Response.RaportJednostki item)
    {
        var regon = Regex.Replace(
            item.Regon,
            REGEX_REGON_REPLACE_ZEROS,
            "$1");

        return new RaportJednostki
        {
            Regon = regon,
            Nazwa = item.Nazwa,
            Nip = AdaptString(item.Nip),
            NazwaSkrocona = AdaptString(item.NazwaSkrocona),
            NumerwRejestrzeEwidencji = AdaptString(item.NumerwRejestrzeEwidencji),
            Dzialalnosci = AdaptString(item.Dzialalnosci),
            Adres = item.MapToAdaptedAddress(),
            Kontakty = item.MapToContacts(),
            Daty = item.MapToDates(),
            OrganRejestrowy = ParseToPair(
                item.OrganRejestrowySymbol,
                item.OrganRejestrowyNazwa),
            RodzajRejestru = ParseToPair(
                item.RodzajRejestruSymbol,
                item.RodzajRejestruNazwa),
            FormaFinansowania = ParseToPair(
                item.FormaFinansowaniaSymbol,
                item.FormaFinansowaniaNazwa),
            PodstawowaFormaPrawna = ParseToPair(
                item.PodstawowaFormaPrawnaSymbol,
                item.PodstawowaFormaPrawnaNazwa),
            SzczegolnaFormaPrawna = ParseToPair(
                item.SzczegolnaFormaPrawnaSymbol,
                item.SzczegolnaFormaPrawnaNazwa),
            OrganZalozycielski = ParseToPair(
                item.OrganZalozycielskiSymbol,
                item.OrganZalozycielskiNazwa),
            FormaWlasnosci = ParseToPair(
                item.FormaWlasnosciSymbol,
                item.FormaWlasnosciNazwa),
            Silos = ParseToPair(
                item.SilosSymbol,
                item.SilosNazwa),
        };
    }

    private static RaportJednostki.Address? MapToAdaptedAddress(this Response.RaportJednostki item)
    {
        var kraj = ParseToPair(
            item.KrajSymbol,
            item.KrajNazwa);
        var wojewodztwo = ParseToPair(
            item.WojewodztwoSymbol,
            item.WojewodztwoNazwa);
        var powiat = ParseToPair(
            item.PowiatSymbol,
            item.PowiatNazwa);
        var gmina = ParseToPair(
            item.GminaSymbol,
            item.GminaNazwa);
        var miejscowoscPoczty = ParseToPair(
            item.MiejscowoscPocztySymbol,
            item.MiejscowoscPocztyNazwa);
        var miejscowosc = ParseToPair(
            item.MiejscowoscSymbol,
            item.MiejscowoscNazwa);
        var ulica = ParseToPair(
            item.UlicaSymbol,
            item.UlicaNazwa);
        var kodPocztowy = AdaptString(item.KodPocztowy);
        var numerNieruchomosci = AdaptString(item.NumerNieruchomosci);
        var numerLokalu = AdaptString(item.NumerLokalu);
        var nietypoweMiejsceLokalizacji = AdaptString(item.NietypoweMiejsceLokalizacji);


        if (kraj is null ||
            wojewodztwo is null ||
            powiat is null ||
            gmina is null ||
            miejscowoscPoczty is null ||
            miejscowosc is null ||
            string.IsNullOrWhiteSpace(kodPocztowy) ||
            string.IsNullOrWhiteSpace(numerNieruchomosci))
        {
            return null;
        }


        return new RaportJednostki.Address
        {
            Kraj = kraj,
            Wojewodztwo = wojewodztwo,
            Powiat = powiat,
            Gmina = gmina,
            MiejscowoscPoczty = miejscowoscPoczty,
            Miejscowosc = miejscowosc,
            Ulica = ulica,
            KodPocztowy = kodPocztowy,
            NumerNieruchomosci = numerNieruchomosci,
            NumerLokalu = numerLokalu,
            NietypoweMiejsceLokalizacji = nietypoweMiejsceLokalizacji,
        };
    }

    private static RaportJednostki.Contacts? MapToContacts(this Response.RaportJednostki item)
    {
        var contacts = new RaportJednostki.Contacts
        {
            NumerTelefonu = item
                .NumerTelefonu
                .Where(item => !string.IsNullOrWhiteSpace(item)),
            NumerWewnetrznyTelefonu = item
                .NumerWewnetrznyTelefonu
                .Where(item => !string.IsNullOrWhiteSpace(item)),
            NumerFaksu = item
                .NumerFaksu
                .Where(item => !string.IsNullOrWhiteSpace(item)),
            Email = item
                .AdresEmail
                .Where(item => !string.IsNullOrWhiteSpace(item)),
            WWW = item
                .WWW
                .Where(item => !string.IsNullOrWhiteSpace(item)),
        };

        if (contacts.NumerTelefonu.Any() ||
            contacts.NumerWewnetrznyTelefonu.Any() ||
            contacts.NumerFaksu.Any() ||
            contacts.Email.Any() ||
            contacts.WWW.Any())
        {
            return contacts;
        }

        return null;
    }

    private static RaportJednostki.Dates MapToDates(this Response.RaportJednostki item)
    {
        return new RaportJednostki.Dates
        {
            DataPowstania = ParseDateOnly(item.DataPowstania),
            DataRozpoczecia = ParseDateOnly(item.DataRozpoczecia),
            DataWpisu = ParseDateOnlyOrNull(item.DataWpisu),
            DataZawieszenia = ParseDateOnlyOrNull(item.DataZawieszenia),
            DataWznowienia = ParseDateOnlyOrNull(item.DataWznowienia),
            DataZmiany = ParseDateOnlyOrNull(item.DataZmiany),
            DataZakonczenia = ParseDateOnlyOrNull(item.DataZakonczenia),
            DataSkreslenia = ParseDateOnlyOrNull(item.DataSkreslenia),
            DataWpisuDoRejestruEwidencji = ParseDateOnlyOrNull(item.DataWpisuDoRejestruEwidencji),
        };
    }

    private static RaportJednostki.Pair? ParseToPair(string? symbol, string? nazwa)
    {
        symbol = AdaptString(symbol);
        nazwa = AdaptString(nazwa);

        if (string.IsNullOrWhiteSpace(symbol) ||
            string.IsNullOrWhiteSpace(nazwa))
        {
            return null;
        }

        return new RaportJednostki.Pair(symbol, nazwa);
    }

    private static DateOnly ParseDateOnly(string value)
    {
        return DateOnly.Parse(value, CultureInfo.InvariantCulture);
    }

    private static DateOnly? ParseDateOnlyOrNull(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }
        return ParseDateOnly(value);
    }

    private static string? AdaptString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }
        return value.Trim();
    }
}
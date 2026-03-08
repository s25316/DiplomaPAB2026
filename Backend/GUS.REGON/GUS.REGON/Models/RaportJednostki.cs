// Ignore spelling: Regon, Raport, Jednostki
// Ignore spelling: Nazwa, Skrocona, Adres, Kontakty, Daty, Numerw, Rejestrze, Ewidencji
// Ignore spelling: Rejestrowy, Rodzaj, Rejestru, Forma, Finansowania, Podstawowa, Prawna
// Ignore spelling: Szczegolna, Zalozycielski, Wlasnosci, Dzialalnosci

// Ignore spelling: Daty
// Ignore Spelling: Powstania, Rozpoczecia, Wpisu, Zawieszenia, Wznowienia, Zmiany, 
// Ignore Spelling: Zakonczenia, Skreslenia, Rejestru, Ewidencji 

// Ignore spelling: Contacts
// Ignore spelling: Numer, Telefonu, Wewnetrzny, Faksu

// Ignore spelling: Adres
// Ignore spelling: Kraj, Wojewodztwo, Powiat, Gmina, Miejscowosc, Kod, Poczty, Pocztowy, Ulica
// Ignore spelling: Nieruchomosci, Lokalu, Nietypowe, Miejsce, Lokalizacji

namespace GUS.REGON.Models;

public record RaportJednostki
{
    public sealed record Pair(string Symbol, string Nazwa);

    public sealed record Address
    {
        public Pair Kraj { get; init; } = null!;
        public Pair Wojewodztwo { get; init; } = null!;
        public Pair Powiat { get; init; } = null!;
        public Pair Gmina { get; init; } = null!;
        public Pair MiejscowoscPoczty { get; init; } = null!;
        public Pair Miejscowosc { get; init; } = null!;
        public Pair? Ulica { get; init; } = null;
        public string KodPocztowy { get; init; } = null!;
        public string NumerNieruchomosci { get; init; } = null!;
        public string? NumerLokalu { get; init; } = null;
        public string? NietypoweMiejsceLokalizacji { get; init; } = null;
    }

    public sealed record Contacts
    {
        public IEnumerable<string> NumerTelefonu { get; init; } = [];
        public IEnumerable<string> NumerWewnetrznyTelefonu { get; init; } = [];
        public IEnumerable<string> NumerFaksu { get; init; } = [];
        public IEnumerable<string> Email { get; init; } = [];
        public IEnumerable<string> WWW { get; init; } = [];
    }

    public sealed record Dates
    {
        public DateOnly DataPowstania { get; init; }
        public DateOnly DataRozpoczecia { get; init; }
        public DateOnly? DataWpisu { get; init; }
        public DateOnly? DataZawieszenia { get; init; } = null;
        public DateOnly? DataWznowienia { get; init; } = null;
        public DateOnly? DataZmiany { get; init; } = null;
        public DateOnly? DataZakonczenia { get; init; } = null;
        public DateOnly? DataSkreslenia { get; init; } = null;
        public DateOnly? DataWpisuDoRejestruEwidencji { get; init; } = null;
    }


    public string Regon { get; init; } = null!;
    public string? Nip { get; init; } = null;
    public string Nazwa { get; init; } = null!;
    public string? NazwaSkrocona { get; init; } = null;
    public string? NumerwRejestrzeEwidencji { get; init; } = null;
    public string? Dzialalnosci { get; init; } = null;
    public Address? Adres { get; init; } = null;
    public Contacts? Kontakty { get; init; } = null;
    public Dates Daty { get; init; } = null!;
    public Pair? OrganRejestrowy { get; init; } = null;
    public Pair? RodzajRejestru { get; init; } = null;
    public Pair? FormaFinansowania { get; init; } = null;
    public Pair? PodstawowaFormaPrawna { get; init; } = null;
    public Pair? SzczegolnaFormaPrawna { get; init; } = null;
    public Pair? OrganZalozycielski { get; init; } = null;
    public Pair? FormaWlasnosci { get; init; } = null;
    public Pair? Silos { get; init; } = null;
}
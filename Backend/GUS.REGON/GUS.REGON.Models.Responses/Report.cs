namespace GUS.REGON.Models.Responses;

public class Report
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
    public Address? Adres { get; init; } = null;
    public Dates Daty { get; init; } = null!;
    public Pair? OrganRejestrowy { get; init; } = null;
    public Pair? RodzajRejestru { get; init; } = null;
    public Pair? FormaFinansowania { get; init; } = null;
    public Pair? PodstawowaFormaPrawna { get; init; } = null;
    public Pair? SzczegolnaFormaPrawna { get; init; } = null;
    public Pair? OrganZalozycielski { get; init; } = null;
    public Pair? FormaWlasnosci { get; init; } = null;
}
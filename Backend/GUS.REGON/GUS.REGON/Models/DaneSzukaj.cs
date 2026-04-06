// Ignore Spelling: Dane, Szukaj, Regon, Nazwa
// Ignore Spelling: Adres
// Ignore Spelling: Adres, Wojewodztwo, Powiat, Gmina, Miejscowosc, Kod, Pocztowy, Ulica
using GUS.REGON.Models.Responses.Enums;

namespace GUS.REGON.Models;

public sealed record DaneSzukaj
{
    public sealed record Address
    {
        public string Wojewodztwo { get; init; } = null!;
        public string Powiat { get; init; } = null!;
        public string Gmina { get; init; } = null!;
        public string Miejscowosc { get; init; } = null!;
        public string KodPocztowy { get; init; } = null!;
        public string? Ulica { get; init; } = null;
    }


    public required string Regon { get; init; } = null!;
    public required string Nazwa { get; init; } = null!;
    public required TypJednostki Typ { get; init; }
    public int? SilosId { get; init; } = null;
    public Address? Adres { get; init; } = null;
}
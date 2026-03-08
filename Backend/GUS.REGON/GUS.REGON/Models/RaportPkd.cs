// Ignore Spelling: Raport, Pkd, Kod, Nazwa, 
namespace GUS.REGON.Models;

public sealed record RaportPkd
{
    public string Kod { get; init; } = null!;
    public string Nazwa { get; init; } = null!;
    public bool IsMain { get; init; } = false;
}
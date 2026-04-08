using GUS.REGON.Database.Models.Addresses;

namespace GUS.REGON.Database.Models;

public class Report
{
    public string Regon { get; init; } = null!;
    public string Nazwa { get; init; } = null!;
    public string? NazwaSkrocona { get; init; } = null;

    public DateOnly DataPowstania { get; init; }
    public DateOnly DataRozpoczecia { get; init; }
    public DateOnly? DataWpisu { get; init; }
    public DateOnly? DataZawieszenia { get; init; } = null;
    public DateOnly? DataWznowienia { get; init; } = null;
    public DateOnly? DataZmiany { get; init; } = null;
    public DateOnly? DataZakonczenia { get; init; } = null;
    public DateOnly? DataSkreslenia { get; init; } = null;
    public DateOnly? DataWpisuDoRejestruEwidencji { get; init; } = null;

    public Guid? AddressId { get; set; } = null;
    public virtual Address? Address { get; set; } = null;

    public virtual Query Query { get; set; } = null!;
}
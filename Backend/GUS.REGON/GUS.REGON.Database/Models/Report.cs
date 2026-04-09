using GUS.REGON.Database.Models.Addresses;
using GUS.REGON.Database.Models.RegistrationDetails;

namespace GUS.REGON.Database.Models;

public class Report
{
    public string Regon { get; init; } = null!;
    public string Nazwa { get; init; } = null!;
    public string? NazwaSkrocona { get; init; } = null;
    public int? SilosId { get; set; } = null;

    public DateOnly DataPowstania { get; init; }
    public DateOnly DataRozpoczecia { get; init; }
    public DateOnly? DataWpisu { get; init; }
    public DateOnly? DataZawieszenia { get; init; } = null;
    public DateOnly? DataWznowienia { get; init; } = null;
    public DateOnly? DataZmiany { get; init; } = null;
    public DateOnly? DataZakonczenia { get; init; } = null;
    public DateOnly? DataSkreslenia { get; init; } = null;
    public DateOnly? DataWpisuDoRejestruEwidencji { get; init; } = null;


    public string TypJednostkiCode { get; set; } = null!;
    public virtual TypJednostki TypJednostki { get; set; } = null!;

    public virtual Query Query { get; set; } = null!;

    public Guid? AddressId { get; set; } = null;
    public virtual Address? Address { get; set; } = null;

    public string? FormaFinansowaniaCode { get; set; } = null;
    public virtual FormaFinansowania? FormaFinansowania { get; set; } = null;

    public string? FormaWlasnosciCode { get; set; } = null;
    public virtual FormaWlasnosci? FormaWlasnosci { get; set; } = null;

    public string? OrganRejestrowyCode { get; set; } = null;
    public virtual OrganRejestrowy? OrganRejestrowy { get; set; } = null;

    public string? OrganZalozycielskiCode { get; set; } = null;
    public virtual OrganZalozycielski? OrganZalozycielski { get; set; } = null;

    public string? PodstawowaFormaPrawnaCode { get; set; } = null;
    public virtual PodstawowaFormaPrawna? PodstawowaFormaPrawna { get; set; } = null;

    public string? SzczegolnaFormaPrawnaCode { get; set; } = null;
    public virtual SzczegolnaFormaPrawna? SzczegolnaFormaPrawna { get; set; } = null;

    public string? RodzajRejestruCode { get; set; } = null;
    public virtual RodzajRejestru? RodzajRejestru { get; set; } = null;
}
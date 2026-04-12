using GUS.REGON.Database.Models.Addresses;
using GUS.REGON.Database.Models.Contacts;
using GUS.REGON.Database.Models.RegistrationDetails;

namespace GUS.REGON.Database.Models;

public class Report
{
    public string Regon { get; set; } = null!;
    public string Nazwa { get; set; } = null!;
    public string? NazwaSkrocona { get; set; } = null;
    public string? NumerwRejestrzeEwidencji { get; set; } = null;
    public string? Dzialalnosci { get; set; } = null;
    public int? SilosId { get; set; } = null;

    public DateOnly DataPowstania { get; set; }
    public DateOnly DataRozpoczecia { get; set; }
    public DateOnly? DataWpisu { get; set; }
    public DateOnly? DataZawieszenia { get; set; } = null;
    public DateOnly? DataWznowienia { get; set; } = null;
    public DateOnly? DataZmiany { get; set; } = null;
    public DateOnly? DataZakonczenia { get; set; } = null;
    public DateOnly? DataSkreslenia { get; set; } = null;
    public DateOnly? DataWpisuDoRejestruEwidencji { get; set; } = null;


    public virtual Query Query { get; set; } = null!;

    public string TypJednostkiId { get; set; } = null!;
    public virtual TypJednostki TypJednostki { get; set; } = null!;

    public Guid? AddressId { get; set; } = null;
    public virtual Address? Address { get; set; } = null;

    public string? FormaFinansowaniaId { get; set; } = null;
    public virtual FormaFinansowania? FormaFinansowania { get; set; } = null;

    public string? FormaWlasnosciId { get; set; } = null;
    public virtual FormaWlasnosci? FormaWlasnosci { get; set; } = null;

    public string? OrganRejestrowyId { get; set; } = null;
    public virtual OrganRejestrowy? OrganRejestrowy { get; set; } = null;

    public string? OrganZalozycielskiId { get; set; } = null;
    public virtual OrganZalozycielski? OrganZalozycielski { get; set; } = null;

    public string? PodstawowaFormaPrawnaId { get; set; } = null;
    public virtual PodstawowaFormaPrawna? PodstawowaFormaPrawna { get; set; } = null;

    public string? SzczegolnaFormaPrawnaId { get; set; } = null;
    public virtual SzczegolnaFormaPrawna? SzczegolnaFormaPrawna { get; set; } = null;

    public string? RodzajRejestruId { get; set; } = null;
    public virtual RodzajRejestru? RodzajRejestru { get; set; } = null;

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = [];
    public virtual ICollection<Website> Websites { get; set; } = [];
    public virtual ICollection<Email> Emails { get; set; } = [];
}
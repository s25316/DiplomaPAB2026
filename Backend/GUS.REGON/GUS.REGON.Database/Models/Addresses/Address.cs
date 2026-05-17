namespace GUS.REGON.Database.Models.Addresses;

public class Address
{
    public Guid AddressId { get; set; }
    public string KodPocztowy { get; set; } = null!;
    public string NumerNieruchomosci { get; set; } = null!;
    public string? NumerLokalu { get; set; } = null;
    public string? NietypoweMiejsceLokalizacji { get; set; } = null;


    public string KrajCode { get; set; } = null!;
    public virtual Kraj Kraj { get; set; } = null!;

    public string WojewodztwoCode { get; set; } = null!;
    public virtual Wojewodztwo Wojewodztwo { get; set; } = null!;

    public string PowiatCode { get; set; } = null!;
    public virtual Powiat Powiat { get; set; } = null!;

    public string GminaCode { get; set; } = null!;
    public virtual Gmina Gmina { get; set; } = null!;

    public string MiejscowoscPocztyCode { get; set; } = null!;
    public virtual MiejscowoscPoczty MiejscowoscPoczty { get; set; } = null!;

    public string MiejscowoscCode { get; set; } = null!;
    public virtual Miejscowosc Miejscowosc { get; set; } = null!;

    public string? UlicaCode { get; set; } = null;
    public virtual Ulica? Ulica { get; set; } = null;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}
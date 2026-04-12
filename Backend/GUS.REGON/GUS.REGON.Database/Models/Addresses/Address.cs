namespace GUS.REGON.Database.Models.Addresses;

public class Address
{
    public Guid AddressId { get; set; }
    public string KodPocztowy { get; set; } = null!;
    public string NumerNieruchomosci { get; set; } = null!;
    public string? NumerLokalu { get; set; } = null;
    public string? NietypoweMiejsceLokalizacji { get; set; } = null;


    public string KrajId { get; set; } = null!;
    public virtual Kraj Kraj { get; set; } = null!;

    public string WojewodztwoId { get; set; } = null!;
    public virtual Wojewodztwo Wojewodztwo { get; set; } = null!;

    public string PowiatId { get; set; } = null!;
    public virtual Powiat Powiat { get; set; } = null!;

    public string GminaId { get; set; } = null!;
    public virtual Gmina Gmina { get; set; } = null!;

    public string MiejscowoscPocztyId { get; set; } = null!;
    public virtual MiejscowoscPoczty MiejscowoscPoczty { get; set; } = null!;

    public string MiejscowoscId { get; set; } = null!;
    public virtual Miejscowosc Miejscowosc { get; set; } = null!;

    public string? UlicaId { get; set; } = null;
    public virtual Ulica? Ulica { get; set; } = null;

    public virtual ICollection<Report> Reports { get; set; } = [];
}
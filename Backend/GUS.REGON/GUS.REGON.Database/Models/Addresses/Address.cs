namespace GUS.REGON.Database.Models.Addresses;

public class Address
{
    public Guid Id { get; set; }
    public string KodPocztowy { get; init; } = null!;
    public string NumerNieruchomosci { get; init; } = null!;
    public string? NumerLokalu { get; init; } = null;
    public string? NietypoweMiejsceLokalizacji { get; init; } = null;


    public string KrajCode { get; init; } = null!;
    public virtual Kraj Kraj { get; init; } = null!;

    public string WojewodztwoCode { get; init; } = null!;
    public virtual Wojewodztwo Wojewodztwo { get; init; } = null!;

    public string PowiatCode { get; init; } = null!;
    public virtual Powiat Powiat { get; init; } = null!;

    public string GminaCode { get; init; } = null!;
    public virtual Gmina Gmina { get; init; } = null!;

    public string MiejscowoscPocztyCode { get; init; } = null!;
    public virtual MiejscowoscPoczty MiejscowoscPoczty { get; init; } = null!;

    public string MiejscowoscCode { get; init; } = null!;
    public virtual Miejscowosc Miejscowosc { get; init; } = null!;

    public string? UlicaCode { get; init; } = null;
    public virtual Ulica? Ulica { get; init; } = null;

    public virtual ICollection<Report> Reports { get; set; } = [];
}
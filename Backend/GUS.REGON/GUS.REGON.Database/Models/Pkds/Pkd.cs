namespace GUS.REGON.Database.Models.Pkds;

public class Pkd
{
    public string PkdCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<InstitutionPkd> Institutions { get; set; } = [];
}
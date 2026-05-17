namespace GUS.REGON.Database.Models.Pkds;

public class InstitutionPkd
{
    public Guid InstitutionPkdId { get; set; }
    public bool IsMain { get; set; }


    public string PkdCode { get; set; } = null!;
    public virtual Pkd Pkd { get; set; } = null!;
    public string Regon { get; set; } = null!;
    public virtual Institution Institution { get; set; } = null!;
}
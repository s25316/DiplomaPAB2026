namespace RADON.Database.Models.Institutions;

public class InstitutionStatus
{
    public string InstitutionStatusCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<InstitutionStatusSnapshot> InstitutionSnapshots { get; set; } = [];
}
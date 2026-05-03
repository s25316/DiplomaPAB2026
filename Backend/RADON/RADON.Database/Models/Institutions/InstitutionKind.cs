namespace RADON.Database.Models.Institutions;

public class InstitutionKind
{
    public string InstitutionKindCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public string ClassificationCode { get; set; } = null!;
    public virtual InstitutionClassification Classification { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = [];
}
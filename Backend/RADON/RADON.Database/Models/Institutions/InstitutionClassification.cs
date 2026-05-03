namespace RADON.Database.Models.Institutions;

public class InstitutionClassification
{
    public string InstitutionClassificationCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<InstitutionKind> Kinds { get; set; } = [];
    public virtual ICollection<InstitutionType> Types { get; set; } = [];
}
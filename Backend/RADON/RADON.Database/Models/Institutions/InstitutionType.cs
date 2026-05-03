namespace RADON.Database.Models.Institutions;

public class InstitutionType
{
    public Guid InstitutionTypeId { get; set; }
    public string InstitutionTypeCode { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual string ClassificationCode { get; set; } = null!;
    public virtual InstitutionClassification Classification { get; set; } = null!;

    public virtual ICollection<InstitutionTypeSnapshot> InstitutionSnapshots { get; set; } = [];
}
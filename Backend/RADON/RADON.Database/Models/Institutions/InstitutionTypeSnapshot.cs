namespace RADON.Database.Models.Institutions;

public class InstitutionTypeSnapshot
{
    public Guid InstitutionTypeSnapshotId { get; set; }
    public DateOnly Date { get; set; }


    public Guid InstitutionTypeId { get; set; }
    public virtual InstitutionType InstitutionType { get; set; } = null!;
    public Guid InstitutionUuid { get; set; }
    public virtual Institution Institution { get; set; } = null!;
}
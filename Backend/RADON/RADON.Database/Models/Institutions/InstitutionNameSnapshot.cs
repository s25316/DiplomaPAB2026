namespace RADON.Database.Models.Institutions;

public class InstitutionNameSnapshot
{
    public Guid InstitutionNameSnapshotId { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly Date { get; set; }


    public Guid InstitutionUuid { get; set; }
    public virtual Institution Institution { get; set; } = null!;
}
namespace RADON.Database.Models.Institutions;

public class InstitutionStatusSnapshot
{
    public Guid InstitutionStatusSnapshotId { get; set; }
    public DateOnly Date { get; set; }


    public string InstitutionStatusCode { get; set; } = null!;
    public virtual InstitutionStatus InstitutionStatus { get; set; } = null!;
    public Guid InstitutionUuid { get; set; }
    public virtual Institution Institution { get; set; } = null!;
}
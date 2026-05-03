namespace RADON.Database.Models.Institutions;

public class Institution
{
    public Guid InstitutionUuid { get; set; }

    public string? Regon { get; set; } = null;
    public string? Nip { get; set; } = null;
    public string? Krs { get; set; } = null;

    public DateOnly StartDate { get; set; }
    public DateOnly? LiquidationStartDate { get; set; } = null;
    public DateOnly? LiquidationDate { get; set; } = null;

    public string? Www { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Phone { get; set; } = null;

    public DateTimeOffset LastRefresh { get; set; }
    public DateTimeOffset SourceLastRefresh { get; set; }


    public Guid DataSourceId { get; set; }
    public virtual DataSource DataSource { get; set; } = null!;

    public string InstitutionKindCode { get; set; } = null!;
    public virtual InstitutionKind InstitutionKind { get; set; } = null!;

    public virtual ICollection<InstitutionNameSnapshot> NameSnapshots { get; set; } = [];
    public virtual ICollection<InstitutionTypeSnapshot> TypeSnapshots { get; set; } = [];
    public virtual ICollection<InstitutionStatusSnapshot> StatusSnapshots { get; set; } = [];
}
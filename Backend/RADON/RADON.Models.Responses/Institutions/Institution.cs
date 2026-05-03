using RADON.Models.Responses.Dictionaries;

namespace RADON.Models.Responses.Institutions;

public record Institution
{
    public record NameSnapshot(string Name, DateOnly Date);
    public record TypeSnapshot(DictionaryItem Type, DateOnly Date);
    public record StatusSnapshot(DictionaryItem Status, DateOnly Date);


    public required Guid InstitutionUuid { get; init; }

    public string? Regon { get; init; } = null;
    public string? Nip { get; init; } = null;
    public string? Krs { get; init; } = null;

    public required DateOnly StartDate { get; init; }
    public required DateOnly? LiquidationStartDate { get; init; } = null;
    public required DateOnly? LiquidationDate { get; init; } = null;

    public string? Www { get; init; } = null;
    public string? Email { get; init; } = null;
    public string? Phone { get; init; } = null;

    public DateTimeOffset LastRefresh { get; init; }
    public DateTimeOffset SourceLastRefresh { get; init; }
    public string DataSource { get; init; } = null!;


    public DictionaryItem InstitutionKind { get; init; } = null!;
    public ICollection<NameSnapshot> Names { get; init; } = [];
    public ICollection<TypeSnapshot> Types { get; init; } = [];
    public ICollection<StatusSnapshot> Statuses { get; init; } = [];
}
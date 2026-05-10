using RADON.Models.Dictionaries.Responses;

namespace RADON.Models.Institutions.Responses;

public record Institution
{
    public record NameSnapshot
    {
        public required string Name { get; init; }
        public required DateOnly Date { get; init; }
    }
    public record TypeSnapshot
    {
        public required DictionaryItem Type { get; init; }
        public required DateOnly Date { get; init; }
    }
    public record StatusSnapshot
    {
        public required DictionaryItem Status { get; init; }
        public required DateOnly Date { get; init; }
    }


    public required Guid InstitutionUuid { get; init; }

    public required string? Regon { get; init; } = null;
    public required string? Nip { get; init; } = null;
    public required string? Krs { get; init; } = null;

    public required DateOnly StartDate { get; init; }
    public required DateOnly? LiquidationStartDate { get; init; } = null;
    public required DateOnly? LiquidationDate { get; init; } = null;

    public required string? Www { get; init; } = null;
    public required string? Email { get; init; } = null;
    public required string? Phone { get; init; } = null;

    public required DictionaryItem InstitutionKind { get; init; } = null!;
    public required ICollection<NameSnapshot> Names { get; init; } = [];
    public required ICollection<TypeSnapshot> Types { get; init; } = [];
    public required ICollection<StatusSnapshot> Statuses { get; init; } = [];

    public required DateTimeOffset LastRefresh { get; init; }
    public required DateTimeOffset SourceLastRefresh { get; init; }
    public required string DataSource { get; init; } = null!;
}
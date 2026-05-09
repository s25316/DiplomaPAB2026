using Base.Models.ValueObjects.Krsy;
using Base.Models.ValueObjects.Nipy;
using Base.Models.ValueObjects.Regony;
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

    public Regon? Regon { get; init; } = null;
    public Nip? Nip { get; init; } = null;
    public Krs? Krs { get; init; } = null;

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
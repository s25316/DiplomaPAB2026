using Base.Models.ValueObjects.Krsy;
using Base.Models.ValueObjects.Nipy;
using Base.Models.ValueObjects.Regony;

namespace RADON.Models.Institutions;

public sealed class QueryParameters
{
    public ICollection<Guid> InstitutionUuids { get; set; } = [];

    public string? Name { get; set; } = null;
    public Regon? Regon { get; set; } = null;
    public Nip? Nip { get; set; } = null;
    public Krs? Krs { get; set; } = null;


    public ICollection<string> InstitutionKindCodes { get; init; } = [];
    public ICollection<string> UniversityTypeCodes { get; init; } = [];
    public ICollection<string> ScientificInstitutionTypeCodes { get; init; } = [];
    public ICollection<string> StatusCodes { get; init; } = [];
}
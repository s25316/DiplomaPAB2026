// Ignore Spelling: Teryt, Terc

using GUS.TERYT.Files.Mapping;

namespace GUS.TERYT.Files.Models.Source;

public abstract partial record Teryt
{
    public sealed record Terc(
        string WojewodztwoCode,
        string? PowiatCode,
        string? GminaCode,
        string? GminaRodzCode,
        string Nazwa,
        string NazwaDod,
        DateOnly Date) : Teryt
    {
        public static Terc Parse(string value) => MappingSourceModels.MapStringToTerc(value);
    }
}

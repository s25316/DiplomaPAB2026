// Ignore Spelling: Teryt, Ulic

using GUS.TERYT.Files.Mapping;

namespace GUS.TERYT.Files.Models.Source;

public abstract partial record Teryt
{
    public sealed record Ulic(
        string WojewodstwoId,
        string PowiatId,
        string GminaId,
        string GminaTypeId,
        string MiejscowoscId,
        string UlicaId,
        string? Ceha,
        string Nazwa1,
        string? Nazwa2,
        DateOnly Date) : Teryt
    {
        public static Ulic Parse(string value) => MappingSourceModels.MapStringToUlic(value);
    }
}

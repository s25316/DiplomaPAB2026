// Ignore Spelling: Teryt, Simc

using GUS.TERYT.Files.Mapping;

namespace GUS.TERYT.Files.Models.Source;

public abstract partial record Teryt
{
    public sealed record Simc(
        string WojewodstwoCode,
        string PowiatCode,
        string GminaCode,
        string GminaRodzCode,
        string MiejscowoscRodzaj,
        string MiejscowoscZwyczajowa,
        string Nazwa,
        string MiejscowoscId,
        string ParentMiejscowoscId,
        DateOnly Date) : Teryt
    {
        public static Simc Parse(string value) => MappingSourceModels.MapStringToSimc(value);
    }
}

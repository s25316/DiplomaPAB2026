// Ignore Spelling: Teryt, Simc, Ulic, SimcId, UlicId
using SourceUlic = GUS.TERYT.Files.Models.Source.Teryt.Ulic;

namespace GUS.TERYT.Files.Models.Adapted;

public abstract partial record Teryt
{
    public record SimcUlicIds(Simc.Id SimcId, Ulic.Id UlicId, DateOnly Date) : Teryt
    {
        public static SimcUlicIds Parse(SourceUlic item) => new(
            item.MiejscowoscId,
            item.UlicaId,
            item.Date);
    }
}

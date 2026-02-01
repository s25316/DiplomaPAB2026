// Ignore Spelling: Ulic, Simc, Ulics

namespace GUS.TERYT.Database.Models.Ulics;

public class Ulic
{
    public string UlicId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int? TypeId { get; set; } = null;

    public virtual UlicType? Type { get; set; } = null;
    public virtual ICollection<SimcUlic> SimcUlics { get; set; } = [];
}

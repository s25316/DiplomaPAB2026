// Ignore Spelling: Ulic, Ulicy

namespace GUS.TERYT.Database.Models.Ulics;

public class UlicType
{
    public int TypeId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<Ulic> Ulicy { get; set; } = [];
}

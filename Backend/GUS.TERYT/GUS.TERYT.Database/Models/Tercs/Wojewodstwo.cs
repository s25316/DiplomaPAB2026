// Ignore Spelling: Wojewodstwo, Powiaty

namespace GUS.TERYT.Database.Models.Tercs;

public class Wojewodstwo
{
    public string WojewodstwoId { get; set; } = null!;
    public string Name { get; set; } = null!;


    public virtual ICollection<Powiat> Powiaty { get; set; } = [];
}

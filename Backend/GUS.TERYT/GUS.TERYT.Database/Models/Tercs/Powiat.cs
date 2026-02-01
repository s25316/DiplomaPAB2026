// Ignore Spelling: Wojewodstwo, Powiat, Gminy

namespace GUS.TERYT.Database.Models.Tercs;

public class Powiat
{
    public Guid PowiatId { get; set; }
    public string WojewodstwoId { get; set; } = null!;
    public string PowiatCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int TypeId { get; set; }


    public virtual PowiatType PowiatType { get; set; } = null!;
    public virtual Wojewodstwo Wojewodstwo { get; set; } = null!;
    public virtual ICollection<Gmina> Gminy { get; set; } = [];
}

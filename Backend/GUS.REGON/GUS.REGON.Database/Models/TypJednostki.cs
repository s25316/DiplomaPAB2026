namespace GUS.REGON.Database.Models;

public class TypJednostki
{
    public string TypJednostkiId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}
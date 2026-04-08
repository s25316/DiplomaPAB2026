namespace GUS.REGON.Database.Models;

public class Query
{
    public string Regon { get; set; } = null!;
    public DateOnly LastUpdate { get; set; }

    public virtual Report? Report { get; set; } = null;
}
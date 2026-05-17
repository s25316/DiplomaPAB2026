namespace GUS.REGON.Database.Models;

public class Request
{
    public string Regon { get; set; } = null!;
    public DateOnly LastUpdate { get; set; }

    public int RequestStatusCode { get; set; }
    public virtual RequestStatus RequestStatus { get; set; } = null!;

    public virtual Institution? Institution { get; set; } = null;
}
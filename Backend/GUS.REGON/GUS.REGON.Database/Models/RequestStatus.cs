namespace GUS.REGON.Database.Models;

public class RequestStatus
{
    public int RequestStatusCode { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<Request> Requests { get; set; } = [];
}
namespace Diploma.Database.Models.Persons.PersonEvents.Audits;

public class PersonRefreshToken
{
    public Guid PersonRefreshTokenId { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTimeOffset ExpiresAt { get; set; }


    public Guid LoginInEventId { get; set; }
    public virtual PersonEvent LoginInEvent { get; set; } = null!;

    public Guid? LogOutEventId { get; set; }
    public virtual PersonEvent? LogOutEvent { get; set; } = null;
}
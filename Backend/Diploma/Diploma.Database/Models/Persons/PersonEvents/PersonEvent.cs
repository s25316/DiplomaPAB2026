using Diploma.Database.Models.Persons.PersonEvents.Audits;

namespace Diploma.Database.Models.Persons.PersonEvents;

public class PersonEvent
{
    public Guid PersonEventId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }


    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;

    public int PersonEventTypeId { get; set; }
    public virtual PersonEventType PersonEventType { get; set; } = null!;


    public virtual PersonIdentity? PersonIdentity { get; set; } = null;
    public virtual PersonProfile? PersonProfile { get; set; } = null;

    #region PersonRefreshToken
    public virtual PersonRefreshToken? PersonLoginIn { get; set; } = null;
    public virtual PersonRefreshToken? PersonLogOut { get; set; } = null;
    #endregion


    public virtual PersonUri? PersonUri { get; set; } = null;
    public virtual PersonEducation? PersonEducation { get; set; } = null;
    public virtual PersonEmployment? PersonEmployment { get; set; } = null;
}
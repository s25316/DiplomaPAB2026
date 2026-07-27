using Base.Models.ValueObjects.Regony;
using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.PersonEmployments.Aggregates;

public sealed record PersonEmploymentId : BaseEntityId<Guid>
{
    public static implicit operator Guid(PersonEmploymentId value) => value.Value;
    public static implicit operator PersonEmploymentId(Guid value) => new() { Value = value };
}
public partial class PersonEmployment : BaseEntity<PersonEmploymentId>
{
    public PersonEmploymentId LastSnapshotId { get; protected set; } = null!;
    public PersonId PersonId { get; protected set; } = null!;
    public Regon Regon { get; protected set; } = null!;
    public string Position { get; set; } = null!;
    public string Descrition { get; set; } = null!;
    public DateOnly From { get; protected set; }
    public DateOnly? To { get; protected set; } = null;


    public void UpdateDates(DateOnly from, DateOnly? to)
    {
        if (to.HasValue && to < from)
        {
            var value = from;
            from = to.Value;
            to = value;
        }

        From = from;
        To = to;
    }


    public static PersonEmployment Create(
        PersonId personId,
        Regon regon,
        string position,
        string descrition,
        DateOnly from,
        DateOnly? to)
    {
        var employment = new PersonEmployment();

        employment.PersonId = personId;
        employment.Regon = regon;
        employment.Position = position;
        employment.Descrition = descrition;
        employment.UpdateDates(from, to);

        return employment;
    }
}
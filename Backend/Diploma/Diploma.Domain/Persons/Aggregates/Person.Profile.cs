using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Events.Profile;

namespace Diploma.Domain.Persons.Aggregates;

public partial class Person : BaseEntity<PersonId>
{
    public void UpdateIdentityData(
        string name,
        string surname)
    {
        ArgumentNullException.ThrowIfNull(Id);

        if (Name == name && Surname == surname) return;

        var @event = new PersonUpdateIdentityDataEvent
        {
            EntityId = Id,
            Name = name.Trim(),
            Surname = surname.Trim(),
        };

        AddEvent(@event);
    }

    public void UpdateProfileData(
        string? title,
        string? summary)
    {
        ArgumentNullException.ThrowIfNull(Id);

        if (Title == title && Summary == summary) return;

        var @event = new PersonUpdateProfileDataEvent
        {
            EntityId = Id,
            Title = title?.Trim(),
            Summary = summary?.Trim(),
        };

        AddEvent(@event);
    }
}
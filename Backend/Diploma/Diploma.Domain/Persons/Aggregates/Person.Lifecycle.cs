using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Events.Lifecycle;
using Diploma.Domain.ValueObjects;

namespace Diploma.Domain.Persons.Aggregates;

public partial class Person : BaseEntity<PersonId>
{
    public static Person Create(Email login, string password, string salt)
    {
        var person = new Person();
        person.HasEnabledEvents = false;

        var @event = new PersonCretedEvent
        {
            GetTEntityId = () => person.Id,
            Login = login,
        };

        person.Login = login;
        person.Password = password;
        person.Salt = salt;
        person.CreatedAt = @event.CreatedAt;
        person.HasEnabledEvents = true;

        person.AddCreationalEvent(@event);
        return person;
    }

    public void Activate()
    {
        ArgumentNullException.ThrowIfNull(Id);

        if (HasActivated)
            return;

        var @event = new PersonActivatedEvent
        {
            EntityId = Id,
            Login = Login,
        };

        ActivatedAt = @event.CreatedAt;
        AddEvent(@event);
    }

    public void Remove(DateTimeOffset removedAt, DateTimeOffset anonymizedAt)
    {
        ArgumentNullException.ThrowIfNull(Id);

        if (HasRemoved)
            return;

        var @event = new PersonRemovedEvent
        {
            EntityId = Id,
            Login = Login,
            CreatedAt = removedAt,
        };

        RemovedAt = removedAt;
        AnonymizedAt = anonymizedAt;
        AddEvent(@event);
    }

    public void Restore()
    {
        ArgumentNullException.ThrowIfNull(Id);

        if (!HasRemoved)
            return;

        var @event = new PersonRestoredEvent
        {
            EntityId = Id,
            Login = Login,
        };

        RemovedAt = null;
        AnonymizedAt = null;
        AddEvent(@event);
    }

    public void Anonymize()
    {
        throw new NotImplementedException();
        ArgumentNullException.ThrowIfNull(Id);

        if (!HasRemoved)
            return;

        var @event = new PersonAnonymizedEvent
        {
            EntityId = Id,
            Login = Login,
        };

        AnonymizedAt = @event.CreatedAt;
        AddEvent(@event);
    }
}
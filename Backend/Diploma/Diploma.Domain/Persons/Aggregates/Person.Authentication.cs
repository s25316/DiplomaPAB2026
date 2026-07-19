using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Events.Authentication;
using Diploma.Domain.ValueObjects;

namespace Diploma.Domain.Persons.Aggregates;

public partial class Person : BaseEntity<PersonId>
{
    public void LoginInSucess(string refreshToken, DateTimeOffset expiresAt)
    {
        ArgumentNullException.ThrowIfNull(Id);
        var @event = new PersonLoginInSuccessEvent
        {
            EntityId = Id,
            Login = Login,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
        };

        AddEvent(@event);
    }

    public void LoginInUnSuccess(PersonLoginInUnSuccessReason reason)
    {
        ArgumentNullException.ThrowIfNull(Id);
        var @event = new PersonLoginInUnSuccessEvent
        {
            EntityId = Id,
            Login = Login,
            Reason = reason,
        };

        AddEvent(@event);
    }

    public void LogOut(Guid refreshTokenId)
    {
        ArgumentNullException.ThrowIfNull(Id);
        var @event = new PersonLogOutEvent
        {
            EntityId = Id,
            Login = Login,
            PersonRefreshTokenId = refreshTokenId,
        };

        AddEvent(@event);
    }

    public void UpdateLogin(Email login)
    {
        ArgumentNullException.ThrowIfNull(Id);
        var @event = new PersonUpdateLoginEvent
        {
            EntityId = Id,
            OldLogin = Login,
            NewLogin = login,
        };

        Login = login;
        AddEvent(@event);
    }

    public void UpdatePassword(string password, string salt)
    {
        ArgumentNullException.ThrowIfNull(Id);
        var @event = new PersonUpdatePasswordEvent
        {
            EntityId = Id,
            Login = Login,
        };

        Password = password;
        Salt = salt;
        AddEvent(@event);
    }
}
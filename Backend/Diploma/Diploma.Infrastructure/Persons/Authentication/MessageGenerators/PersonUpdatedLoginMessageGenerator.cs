using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Authentication.MessageGenerators;

namespace Diploma.Infrastructure.Persons.Authentication.MessageGenerators;

public class PersonUpdatedLoginMessageGenerator : IPersonUpdatedLoginMessageGenerator
{
    private const string SUBJECT = "Powiadomienie o zmianie loginu";

    public MessageResult Generate(PersonUpdatedLoginMessageInput input)
    {
        string body = $@"Szanowny Użytkowniku,<br><br>
            Informujemy, że login do Państwa konta został pomyślnie zmieniony.<br><br>
            Stary login: <b>{input.OldLogin}</b><br>
            Nowy login: <b>{input.NewLogin}</b><br><br>
            Jeśli nie dokonywali Państwo tej zmiany, prosimy o niezwłoczny kontakt z administratorem.";

        return new MessageResult
        {
            Subject = SUBJECT,
            Body = body,
        };
    }
}
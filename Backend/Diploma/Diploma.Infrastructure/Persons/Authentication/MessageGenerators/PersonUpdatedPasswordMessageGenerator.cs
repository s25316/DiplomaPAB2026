using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Authentication.MessageGenerators;

namespace Diploma.Infrastructure.Persons.Authentication.MessageGenerators;

public class PersonUpdatedPasswordMessageGenerator : IPersonUpdatedPasswordMessageGenerator
{
    private const string SUBJECT = "Powiadomienie o zmianie hasła";
    private const string MESSAGE_TEMPLATE =
        "Szanowny Użytkowniku,<br><br>" +
        "Informujemy, że hasło do Państwa konta w systemie zostało pomyślnie zmienione.<br><br>" +
        "Jeśli nie dokonywali Państwo tej zmiany, prosimy o niezwłoczny kontakt z administratorem systemu lub skorzystanie z procedury odzyskiwania hasła.";

    public MessageResult Generate(PersonUpdatedPasswordMessageInput input)
    {
        return new MessageResult
        {
            Subject = SUBJECT,
            Body = MESSAGE_TEMPLATE,
        };
    }
}
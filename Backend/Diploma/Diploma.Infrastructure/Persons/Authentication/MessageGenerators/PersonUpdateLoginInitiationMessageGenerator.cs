using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Authentication.MessageGenerators;

namespace Diploma.Infrastructure.Persons.Authentication.MessageGenerators;

public class PersonUpdateLoginInitiationMessageGenerator : IPersonUpdateLoginInitiationMessageGenerator
{
    private const string SUBJECT = "Potwierdzenie zmiany loginu";


    public MessageResult Generate(PersonUpdateLoginInitiationMessageInput input)
    {
        string body = $@"Szanowny Użytkowniku,<br><br>
            Otrzymaliśmy prośbę o zmianę loginu do Państwa konta.<br><br>
            Aby potwierdzić tę operację, prosimy o wpisanie poniższego kodu w aplikacji:<br><br>
            <div style='font-size: 18px; font-weight: bold; padding: 10px; background-color: #f0f0f0; display: inline-block;'>
                {input.Code}
            </div><br><br>
            Kod jest ważny przez ograniczony czas. Jeśli nie wysyłali Państwo takiej prośby, prosimy o zignorowanie tej wiadomości.";

        return new MessageResult
        {
            Subject = SUBJECT,
            Body = body,
        };
    }
}
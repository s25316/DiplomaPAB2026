using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Authentication.MessageGenerators;

namespace Diploma.Infrastructure.Persons.Authentication.MessageGenerators;

public class PersonUpdatePasswordRecoveryInitiationMessageGenerator : IPersonUpdatePasswordRecoveryInitiationMessageGenerator
{
    private const string SUBJECT = "Odzyskiwanie dostępu do konta";


    public MessageResult Generate(PersonUpdatePasswordRecoveryInitiationMessageInput input)
    {
        string body = $@"Szanowny Użytkowniku,<br><br>
            Otrzymaliśmy prośbę o zresetowanie hasła do Państwa konta.<br><br>
            Aby kontynuować proces odzyskiwania dostępu, prosimy o wpisanie poniższego kodu w aplikacji:<br><br>
            <div style='font-size: 18px; font-weight: bold; padding: 10px; background-color: #f0f0f0; display: inline-block;'>
                {input.Code}
            </div><br><br>
            Jeśli nie inicjowali Państwo tej operacji, prosimy o zignorowanie tej wiadomości. Państwa hasło pozostanie bez zmian.";

        return new MessageResult
        {
            Subject = SUBJECT,
            Body = body,
        };
    }
}
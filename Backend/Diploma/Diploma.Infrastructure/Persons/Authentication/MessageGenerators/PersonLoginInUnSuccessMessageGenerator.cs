using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Authentication.MessageGenerators;
using Diploma.Domain.Persons.Events.Authentication;

namespace Diploma.Infrastructure.Persons.Authentication.MessageGenerators;

public class PersonLoginInUnSuccessMessageGenerator : IPersonLoginInUnSuccessMessageGenerator
{
    private const string SUBJECT = "Powiadomienie o nieudanej próbie logowania";


    public MessageResult Generate(PersonLoginInUnSuccessMessageInput input)
    {
        string body = input.Reason switch
        {
            PersonLoginInUnSuccessReason.ProfileIsNotActivated =>
                "Szanowny Użytkowniku,<br><br>Informujemy, iż logowanie nie powiodło się, ponieważ konto nie zostało jeszcze aktywowane. Prosimy o sprawdzenie skrzynki odbiorczej i finalizację procesu aktywacji.",

            PersonLoginInUnSuccessReason.ProfileRemoved =>
                "Szanowny Użytkowniku,<br><br>Informujemy, iż próba logowania dotyczy konta, które zostało usunięte. Dostęp do tej usługi nie jest już możliwy.",

            PersonLoginInUnSuccessReason.InvalidPassword =>
                "Szanowny Użytkowniku,<br><br>Informujemy, iż próba logowania zakończyła się niepowodzeniem z powodu nieprawidłowych danych uwierzytelniających. Prosimy o ponowną próbę lub skorzystanie z funkcji resetowania hasła.",

            _ => throw new NotImplementedException($"Nieobsługiwany powód nieudanego logowania: {input.Reason.GetType()}")
        };

        return new MessageResult
        {
            Subject = SUBJECT,
            Body = body
        };
    }
}
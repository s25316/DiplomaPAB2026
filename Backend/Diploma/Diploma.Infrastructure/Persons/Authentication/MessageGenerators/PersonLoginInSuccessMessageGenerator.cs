using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Authentication.MessageGenerators;

namespace Diploma.Infrastructure.Persons.Authentication.MessageGenerators;

public class PersonLoginInSuccessMessageGenerator : IPersonLoginInSuccessMessageGenerator
{
    private const string SUBJECT = "Powiadomienie o logowaniu do systemu";
    private const string MESSAGE_TEMPLATE =
        "Szanowny Użytkowniku,<br><br>" +
        "Informujemy, że w systemie odnotowano poprawne logowanie na Państwa konto.<br><br>" +
        "W przypadku, gdy logowanie nie zostało zainicjowane przez Państwa, prosimy o pilną zmianę hasła.";


    public MessageResult Generate(PersonLoginInSuccessMessageInput input)
    {
        return new MessageResult
        {
            Subject = SUBJECT,
            Body = MESSAGE_TEMPLATE,
        };
    }
}
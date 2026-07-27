using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;

namespace Diploma.Infrastructure.Persons.Lifecycle.MessageGenerators;

public class PersonActivatedMessageGenerator : IPersonActivatedMessageGenerator
{
    private const string SUBJECT = "Aktywacja Konta";
    private const string MESSAGE = "Twoje konto zostało aktywowane. Możesz zalogować się na swoje konto.<br><br>";

    public MessageResult Generate(PersonActivatedMessageInput input) => new()
    {
        Subject = SUBJECT,
        Body = MESSAGE,
    };
}
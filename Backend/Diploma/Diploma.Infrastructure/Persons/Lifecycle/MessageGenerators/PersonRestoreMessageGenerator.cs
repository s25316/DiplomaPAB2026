using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Lifecycle.MessageGenerators;

namespace Diploma.Infrastructure.Persons.Lifecycle.MessageGenerators;

public class PersonRestoreMessageGenerator : IPersonRestoreMessageGenerator
{
    private const string SUBJECT = "Przywrócenie dostępu do konta";
    private const string MESSAGE = "Twoje konto zostało pomyślnie przywrócone. Możesz ponownie logować się do systemu.<br><br>";

    public MessageResult Generate(PersonRestoreMessageInput input) => new()
    {
        Subject = SUBJECT,
        Body = MESSAGE,
    };
}
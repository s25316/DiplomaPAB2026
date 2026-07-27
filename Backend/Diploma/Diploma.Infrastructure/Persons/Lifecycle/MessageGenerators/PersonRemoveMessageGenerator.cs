using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;
using Diploma.Infrastructure.Persons.Lifecycle.LinkGenerators;

namespace Diploma.Infrastructure.Persons.Lifecycle.MessageGenerators;

public class PersonRemoveMessageGenerator(
    IPersonRestoreLinkGenerator linkGenerator
    ) : IPersonRemoveMessageGenerator
{
    private const string SUBJECT = "Usunięcie konta";
    private const string MESSAGE_TEMPLATE =
        "Twoje konto zostało usunięte zgodnie z Twoją dyspozycją.<br><br>" +
        "Jeśli decyzja była pomyłkowa, możesz przywrócić konto, korzystając z poniższego linku:<br>" +
        "Link: {0}<br><br>" +
        "Możliwość przywrócenia konta wygasa: {1}";

    public MessageResult Generate(PersonRemoveMessageInput input)
    {
        var link = linkGenerator.Generate(new()
        {
            OperationId = input.OperationId,
        });

        return new()
        {
            Subject = SUBJECT,
            Body = string.Format(MESSAGE_TEMPLATE, link.ToString(), input.ExpiresAt.ToString("g")),
        };
    }
}
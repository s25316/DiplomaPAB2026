using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;
using Diploma.Infrastructure.Persons.Lifecycle.LinkGenerators;

namespace Diploma.Infrastructure.Persons.Lifecycle.MessageGenerators;

public class PersonCreateAndActivationMessageGenerator(
    IPersonActivationLinkGenerator generator
    ) : IPersonCreateAndActivationMessageGenerator
{
    private const string SUBJECT = "Aktywacja Konta";
    private const string MESSAGE_TEMPLATE =
        "Twoje konto zostało utworzone. Wymagana jest aktywacja, przejdź pod poniższy link i podaj kod:<br><br>" +
        "Link: {0}<br>" +
        "Kod: {1}";


    public MessageResult Generate(PersonCreateAndActivationMessageInput input)
    {
        var link = generator.Generate(new PersonActivationLinkInput
        {
            OperationId = input.OperationId,
        });
        return new()
        {
            Subject = SUBJECT,
            Body = string.Format(MESSAGE_TEMPLATE, link.ToString(), input.Code),
        };
    }
}
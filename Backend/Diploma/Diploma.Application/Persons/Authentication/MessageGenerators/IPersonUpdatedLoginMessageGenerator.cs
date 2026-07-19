using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Persons.Authentication.MessageGenerators;

public sealed record PersonUpdatedLoginMessageInput : MessageGeneratorInput
{
    public required string OldLogin { get; set; }
    public required string NewLogin { get; set; }
}
public interface IPersonUpdatedLoginMessageGenerator : IMessageGenerator<PersonUpdatedLoginMessageInput>;
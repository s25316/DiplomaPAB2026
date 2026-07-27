using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Persons.Commands.Authentication.MessageGenerators;

public sealed record PersonUpdateLoginInitiationMessageInput : MessageGeneratorInput
{
    public required Guid OperationId { get; init; }
    public required string Code { get; init; }
}
public interface IPersonUpdateLoginInitiationMessageGenerator : IMessageGenerator<PersonUpdateLoginInitiationMessageInput>;
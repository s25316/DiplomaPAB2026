using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;

public sealed record PersonCreateAndActivationMessageInput : MessageGeneratorInput
{
    public required Guid OperationId { get; init; }
    public required string Code { get; init; }
}
public interface IPersonCreateAndActivationMessageGenerator : IMessageGenerator<PersonCreateAndActivationMessageInput>;
using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Persons.Authentication.MessageGenerators;

public sealed record PersonUpdatePasswordRecoveryInitiationMessageInput : MessageGeneratorInput
{
    public required Guid OperationId { get; init; }
    public required string Code { get; init; }
}
public interface IPersonUpdatePasswordRecoveryInitiationMessageGenerator : IMessageGenerator<PersonUpdatePasswordRecoveryInitiationMessageInput>;
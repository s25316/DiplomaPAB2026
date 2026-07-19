using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Persons.Lifecycle.MessageGenerators;

public sealed record PersonRemoveMessageInput : MessageGeneratorInput
{
    public required Guid OperationId { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
}
public interface IPersonRemoveMessageGenerator : IMessageGenerator<PersonRemoveMessageInput>;
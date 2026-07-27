using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;

public sealed record PersonRestoreMessageInput : MessageGeneratorInput;
public interface IPersonRestoreMessageGenerator : IMessageGenerator<PersonRestoreMessageInput>;
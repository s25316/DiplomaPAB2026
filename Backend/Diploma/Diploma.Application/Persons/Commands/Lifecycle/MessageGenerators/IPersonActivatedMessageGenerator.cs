using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;

public sealed record PersonActivatedMessageInput : MessageGeneratorInput;
public interface IPersonActivatedMessageGenerator : IMessageGenerator<PersonActivatedMessageInput>;
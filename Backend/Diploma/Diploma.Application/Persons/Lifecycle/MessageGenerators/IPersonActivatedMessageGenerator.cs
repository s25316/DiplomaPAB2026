using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Persons.Lifecycle.MessageGenerators;

public sealed record PersonActivatedMessageInput : MessageGeneratorInput;
public interface IPersonActivatedMessageGenerator : IMessageGenerator<PersonActivatedMessageInput>;
using Diploma.Application.Interfaces.Generators;
using Diploma.Domain.Persons.Events.Authentication;

namespace Diploma.Application.Persons.Commands.Authentication.MessageGenerators;

public sealed record PersonLoginInUnSuccessMessageInput : MessageGeneratorInput
{
    public required PersonLoginInUnSuccessReason Reason { get; init; }
}
public interface IPersonLoginInUnSuccessMessageGenerator : IMessageGenerator<PersonLoginInUnSuccessMessageInput>;
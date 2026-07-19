namespace Diploma.Application.Interfaces.Generators;

public abstract record MessageGeneratorInput;
public sealed record MessageResult
{
    public required string Subject { get; init; }
    public required string Body { get; init; }
}

public interface IMessageGenerator<in TInput>
    where TInput : MessageGeneratorInput
{
    MessageResult Generate(TInput input);
}
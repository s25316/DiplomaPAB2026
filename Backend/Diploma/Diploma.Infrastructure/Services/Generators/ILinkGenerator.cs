namespace Diploma.Infrastructure.Services.Generators;

public abstract record LinkGeneratorInput;
public interface ILinkGenerator<in TInput>
    where TInput : LinkGeneratorInput
{
    Uri Generate(TInput input);
}
using Diploma.Application.Interfaces.Generators;

namespace Diploma.Application.Services.Generators;

public interface ISaltGenerator
{
    string Generate();
}

public class SaltGenerator(IStringGenerator generator) : ISaltGenerator
{
    private const int SALT_BYTES = 128 / 8;
    public string Generate() => generator.GenerateBase64String(SALT_BYTES);
}
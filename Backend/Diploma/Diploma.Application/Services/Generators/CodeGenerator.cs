using System.Security.Cryptography;

namespace Diploma.Application.Services.Generators;

public interface ICodeGenerator
{
    string Generate();
}

public class CodeGenerator : ICodeGenerator
{
    public string Generate()
    {
        int code = RandomNumberGenerator.GetInt32(0, 1_000_000);
        return code.ToString("D6");
    }
}
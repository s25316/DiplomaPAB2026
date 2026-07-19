using Diploma.Application.Interfaces.Generators;
using System.Security.Cryptography;

namespace Diploma.Infrastructure.Services.Generators;

public class StringGenerator : IStringGenerator
{
    public string GenerateBase64String(int byteSize)
    {
        byte[] array = new byte[byteSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(array);
        }
        return Convert.ToBase64String(array);
    }
}
using Diploma.Application.Interfaces.Security;
using Diploma.Application.Services.Generators;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Diploma.Infrastructure.Services.Security;

public class PasswordHasherService(
    ISaltGenerator saltGenerator) : IPasswordHasherService
{
    private const int PASSWORD_BYTES = 256 / 8;
    private static readonly int ITERATION_COUNT = 10_000;


    public PasswordHasherResult Hash(string password, string salt)
    {
        var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: password,
          salt: Convert.FromBase64String(salt),
          prf: KeyDerivationPrf.HMACSHA1,
          iterationCount: ITERATION_COUNT,
          numBytesRequested: PASSWORD_BYTES
        ));

        return new PasswordHasherResult
        {
            Salt = salt,
            HashedPassword = hashedPassword
        };
    }

    public PasswordHasherResult Hash(string password)
    {
        var salt = saltGenerator.Generate();
        return Hash(password, salt);
    }
}
namespace Diploma.Application.Interfaces.Security;

public sealed record PasswordHasherResult
{
    public required string Salt { get; init; }
    public required string HashedPassword { get; init; }
}

public interface IPasswordHasherService
{
    PasswordHasherResult Hash(string password);
    PasswordHasherResult Hash(string password, string salt);
}
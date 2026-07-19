namespace Base.Exceptions;

public abstract class ResourceException(string? message) : Exception(message)
{
    public class InvalidData(string? message) : ResourceException(message);
    public class IncorrectFormat(string? message) : ResourceException(message);
    public class NotFound(string? message) : ResourceException(message);
    public class Unauthorized(string? message) : ResourceException(message);
    public class InvalidOperation(string? message) : ResourceException(message);
    public class NotExistEmailRecipient(string email) : ResourceException(email)
    {
        public readonly string Email = email;
    };
}
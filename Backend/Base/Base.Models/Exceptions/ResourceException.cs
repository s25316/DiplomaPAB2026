namespace Base.Models.Exceptions;

public abstract class ResourceException(string? message) : Exception(message)
{
    public class NotFound(string? message) : ResourceException(message);
}

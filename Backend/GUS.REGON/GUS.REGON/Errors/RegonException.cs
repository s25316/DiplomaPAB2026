// Ignore Spelling: Regon
using Base.Exceptions;

namespace GUS.REGON.Errors;

public abstract class RegonException(string? message) : ResourceException(message)
{
    public sealed class InvalidKey(string message) : RegonException(message);
    public sealed class RequiredAuthorization() : RegonException("");
    public sealed class Other(string message) : RegonException(message);
}
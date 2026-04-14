// Ignore Spelling: Regon
using Base.Exceptions;

namespace GUS.REGON.Errors;

public abstract class RegonException
{
    public sealed class InvalidKey(string message) : ResourceException.InvalidData(message);
    public sealed class Unauthorized(string message = "") : ResourceException.Unauthorized(message);
    public sealed class Other(string message) : ServiceException.Other(message);
}
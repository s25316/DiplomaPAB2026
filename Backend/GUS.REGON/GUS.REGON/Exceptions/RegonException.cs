// Ignore Spelling: Regon
using Base.Models.Exceptions;

namespace GUS.REGON.Exceptions;

public abstract class RegonException(string? message) : ResourceException(message)
{
    public sealed class InvalidKey(string message) : InvalidData(message);
}

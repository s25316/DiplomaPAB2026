// Ignore Spelling: Regon
using Base.Exceptions;

namespace GUS.REGON.Exceptions;

public abstract class RegonException(string? message) : ResourceException(message)
{
    public static readonly MaintenanceBreakException MaintenanceBreak = new();
    public static readonly ServiceUnavailableException ServiceUnavailable = new();


    public sealed class InvalidKey(string message) : InvalidData(message);
    public sealed class MaintenanceBreakException() : InvalidData(Messages.StatusUslugi2);
    public sealed class ServiceUnavailableException() : InvalidData(Messages.StatusUslugi0);
}

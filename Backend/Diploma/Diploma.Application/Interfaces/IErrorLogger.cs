namespace Diploma.Application.Interfaces;

public interface IErrorLogger
{
    Task LogErrorAsync(
        Exception exception,
        string? traceIdentifier = null,
        CancellationToken cancellationToken = default);
}
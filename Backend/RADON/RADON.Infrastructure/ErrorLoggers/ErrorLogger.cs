using Microsoft.Extensions.Logging;
using RADON.Application.Interfaces;
using RADON.Database;
using RADON.Database.Models;

namespace RADON.Infrastructure.ErrorLoggers;

public class ErrorLogger(
    ILoggerFactory loggerFactory,
    RadonDbContext context) : IErrorLogger
{
    public async Task LogErrorAsync(
        Exception exception,
        string? traceIdentifier = null,
        CancellationToken cancellationToken = default)
    {
        var databaseError = new Error
        {
            Message = exception.ToString(),
            StackTrace = exception.StackTrace,
            ExceptionType = exception.GetType().FullName,
            TraceIdentifier = traceIdentifier,
            CreatedAt = DateTimeOffset.Now,
        };
        await context.AddAsync(databaseError, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var logger = loggerFactory.CreateLogger<Error>();
        logger.LogError(exception, exception.Message);
    }
}
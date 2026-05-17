using GUS.REGON.Application.Interfaces;
using GUS.REGON.Database;
using GUS.REGON.Database.Models;
using Microsoft.Extensions.Logging;

namespace GUS.REGON.Infrastructure.ErrorLoggers;

public class ErrorLogger(ILoggerFactory loggerFactory, RegonDbContext context) : IErrorLogger
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
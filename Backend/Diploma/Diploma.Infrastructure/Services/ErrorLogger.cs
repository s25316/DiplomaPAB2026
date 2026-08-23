using Diploma.Application.Interfaces;
using Diploma.Database;
using Diploma.Database.Models.Shared;
using Microsoft.Extensions.Logging;

namespace Diploma.Infrastructure.Services;

public class ErrorLogger(ILoggerFactory loggerFactory, DiplomaDbContext context) : IErrorLogger
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
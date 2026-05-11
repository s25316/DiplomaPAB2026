using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RADON.Application.Interfaces;
using System.Net;
using static Base.Exceptions.ResourceException;

namespace RADON.API.ExceptionHandlers;

public class GlobalExceptionHandler(
    ILoggerFactory loggerFactory,
    IErrorLogger errorLogger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> logger = loggerFactory.CreateLogger<GlobalExceptionHandler>();
    private readonly IErrorLogger errorLogger = errorLogger;


    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException or TaskCanceledException)
        {
            return true;
        }

        var traceIdentifier = httpContext.TraceIdentifier;

        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions["traceId"] = traceIdentifier;


        switch (exception)
        {
            case TaskCanceledException:
            case OperationCanceledException:
                return true;

            case InvalidData invalidDataException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = ApiErrorDescription.InvalidDatat_Title;
                problemDetails.Detail = invalidDataException.Message;
                break;

            case IncorrectFormat incorrectFormatException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = ApiErrorDescription.InvalidDatat_Title;
                problemDetails.Detail = incorrectFormatException.Message;
                break;

            default:
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                problemDetails.Title = ApiErrorDescription.InternalServerErrort_Title;
                problemDetails.Detail = ApiErrorDescription.InternalServerErrort_Detail;

                logger.LogError(exception, "BŁĄD 500: {Message}. Instance: {Path}", exception.Message, httpContext.Request.Path);
                await errorLogger.LogErrorAsync(exception, traceIdentifier, cancellationToken);
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
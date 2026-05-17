using Base.Exceptions;
using GUS.REGON.Application.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Base.Exceptions.ResourceException;

namespace GUS.REGON.API.ExceptionHandlers;


public class GlobalExceptionHandler : IExceptionHandler
{
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
        var errorLogger = httpContext.RequestServices.GetRequiredService<IErrorLogger>();

        switch (exception)
        {
            case TaskCanceledException:
            case OperationCanceledException:
                return true;

            case InvalidData invalidDataException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = ApiErrorDescription.InvalidData_Title;
                problemDetails.Detail = invalidDataException.Message;
                break;

            case IncorrectFormat incorrectFormatException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = ApiErrorDescription.InvalidData_Title;
                problemDetails.Detail = incorrectFormatException.Message;
                break;

            case ServiceException serviceException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = ApiErrorDescription.InternalServerErrort_Title;
                problemDetails.Detail = serviceException.Message;

                await errorLogger.LogErrorAsync(exception, traceIdentifier, cancellationToken);
                break;

            default:
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                problemDetails.Title = ApiErrorDescription.InternalServerErrort_Title;
                problemDetails.Detail = ApiErrorDescription.InternalServerErrort_Detail;

                await errorLogger.LogErrorAsync(exception, traceIdentifier, cancellationToken);
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
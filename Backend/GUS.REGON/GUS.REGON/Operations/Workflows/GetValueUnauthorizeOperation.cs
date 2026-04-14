using Base.Pipelines.Operations;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Extensions;
using GUS.REGON.Interfaces;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.Models.Results;
using GUS.REGON.Operations.Primitives;
using GUS.REGON.Operations.Primitives.ElementsTo;
using GUS.REGON.Operations.Primitives.FirstValueTo;
using static GUS.REGON.Errors.RegonOperationError;

namespace GUS.REGON.Operations.Workflows;

internal abstract class GetValueUnauthorizeOperation<T>(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    ISyncOperation<string, T> operation,
    RequestInput input)
{
    public abstract string Name { get; }


    protected virtual async Task<Result<T>> GetResultAsync(CancellationToken cancellationToken = default)
    {
        var result = await AsyncOperation
            .Create(new RequestToDocumentOperation(clientFactory, sessionManager))
            .Add(new DocumentToElementsOperation(ElementDefinition.GetValue))
            .Add(new ElementsToFirstValueOperation())
            .Add(operation)
            .ExecuteAsync(input, cancellationToken);

        return result switch
        {
            { IsSuccess: true } => Result.Success<T>(result.Value),
            { IsFailure: true, Error: EmptyFirstValue } => Result.Failed<T>(),
            _ => throw result.Error.MapToException(),
        };
    }
}

internal sealed class KomunikatUslugiOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    Request.GetValue request
) : GetValueUnauthorizeOperation<string>(
    clientFactory,
    sessionManager,
    new FirstValueToFirstValueOperation(),
    new RequestInput(request.KomunikatUslugi(), false))
{
    private const string NAME = nameof(KomunikatUslugiOperation);
    public override string Name => NAME;


    public async Task<string> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var result = await GetResultAsync(cancellationToken);
        return result.IsSuccess
            ? result.Value
            : String.Empty;
    }
}

internal sealed class StatusUslugiOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    Request.GetValue request
) : GetValueUnauthorizeOperation<StatusUslugi>(
    clientFactory,
    sessionManager,
    new FirstValueToEnumOperation<StatusUslugi>(),
    new RequestInput(request.StatusUslugi(), false))
{
    private const string NAME = nameof(StatusUslugiOperation);
    public override string Name => NAME;


    public async Task<StatusUslugi> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var result = await GetResultAsync(cancellationToken);
        return result.IsSuccess
            ? result.Value
            : StatusUslugi.UslugaNiedostepna;
    }
}
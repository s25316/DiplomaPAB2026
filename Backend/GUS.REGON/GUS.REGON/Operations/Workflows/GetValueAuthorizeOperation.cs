using Base.Pipelines.Operations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Interfaces;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.Models.Results;
using GUS.REGON.Operations.Primitives;
using GUS.REGON.Operations.Primitives.FirstValueTo;

namespace GUS.REGON.Operations.Workflows;

internal abstract class GetValueAuthorizeOperation<T>(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    StatusUslugiOperation statusUslugiOperation,
    ISyncOperation<string, T> operation,
    RequestInput input
) : GetValueUnauthorizeOperation<T>(
    clientFactory,
    sessionManager,
    operation,
    input)
{
    public virtual async Task<RegonBaseResult<T>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var result = await GetResultAsync(cancellationToken);
        if (result.IsFailure)
        {
            var statusUslugiResult = await statusUslugiOperation.ExecuteAsync(cancellationToken);
            return RegonBaseResult.Failed<T>(statusUslugiResult);
        }
        return RegonBaseResult.Success<T>(result.Value);
    }
}

internal sealed class StatusSesjiOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    Request.GetValue request,
    StatusUslugiOperation statusUslugiOperation
) : GetValueAuthorizeOperation<StatusSesji>(
    clientFactory,
    sessionManager,
    statusUslugiOperation,
    new FirstValueToEnumOperation<StatusSesji>(),
    new RequestInput(request.StatusSesji(), true))
{
    private const string NAME = nameof(StatusSesjiOperation);
    public override string Name => NAME;
}

internal sealed class StanDanychOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    Request.GetValue request,
    StatusUslugiOperation statusUslugiOperation
) : GetValueAuthorizeOperation<DateOnly>(
    clientFactory,
    sessionManager,
    statusUslugiOperation,
    new FirstValueToDateOnlyOperation(),
    new RequestInput(request.StanDanych(), true))
{
    private const string NAME = nameof(StanDanychOperation);
    public override string Name => NAME;
}

internal sealed class KomunikatTrescOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    Request.GetValue request,
    StatusUslugiOperation statusUslugiOperation
) : GetValueAuthorizeOperation<string>(
    clientFactory,
    sessionManager,
    statusUslugiOperation,
    new FirstValueToFirstValueOperation(),
    new RequestInput(request.KomunikatTresc(), true))
{
    private const string NAME = nameof(KomunikatTrescOperation);
    public override string Name => NAME;
}

internal sealed class KomunikatKodOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    Request.GetValue request,
    StatusUslugiOperation statusUslugiOperation
) : GetValueAuthorizeOperation<KomunikatKod>(
    clientFactory,
    sessionManager,
    statusUslugiOperation,
    new FirstValueToEnumOperation<KomunikatKod>(),
    new RequestInput(request.KomunikatKod(), true))
{
    private const string NAME = nameof(KomunikatKodOperation);
    public override string Name => NAME;
}
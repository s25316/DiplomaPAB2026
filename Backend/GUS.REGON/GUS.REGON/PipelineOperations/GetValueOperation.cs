using Base.Pipelines;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Exceptions;
using GUS.REGON.Interfaces;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.PipelineOperations.Base;
using GUS.REGON.PipelineOperations.Base.ElementsTo;
using GUS.REGON.PipelineOperations.Base.FirstValueTo;

namespace GUS.REGON.PipelineOperations;

internal abstract class GetValueOperation<T>(
    IHttpClientFactory factory,
    ISessionManager sessionManager,
    Request.GetValue request,
    Func<Request.GetValue, string> getValueFunc,
    ISyncOperation<string, T> operation,
    bool isAuthorize
    ) : IAsyncOperation<T>
{
    public abstract string Name { get; }


    public virtual async Task<OperationResult<T>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        using var client = factory.CreateClient(HttpClientNames.BASE);
        var requestEnvelope = getValueFunc(request);

        return await PipelineBuilder
            .Create(new MakeRequestOperation(client, isAuthorize ? sessionManager : null))
            .Add(new ResponseToEnvelopeOperation())
            .Add(new EnvelopeToDocumentOperation())
            .Add(new DocumentToElementsOperation(ElementDefinition.GetValue))
            .Add(new ElementsToFirstValueOperation())
            .Add(operation)
            .ExecuteAsync(requestEnvelope, cancellationToken);
    }
}

internal sealed class KomunikatUslugiOperation(
    IHttpClientFactory factory,
    ISessionManager sessionManager,
    Request.GetValue request
    ) : GetValueOperation<string>(
    factory,
    sessionManager,
    request,
    getValue => getValue.KomunikatUslugi(),
    new FirstValueToFirstValueOperation(),
    false)
{
    public override string Name => nameof(KomunikatUslugiOperation);
}

internal sealed class StatusUslugiOperation(
    IHttpClientFactory factory,
    ISessionManager sessionManager,
    Request.GetValue request
    ) : GetValueOperation<StatusUslugi>(
    factory,
    sessionManager,
    request,
    getValue => getValue.StatusUslugi(),
    new FirstValueToEnumOperation<StatusUslugi>(),
    false)
{
    public override string Name => nameof(StatusUslugiOperation);


    public override async Task<OperationResult<StatusUslugi>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.ExecuteAsync(cancellationToken);
        if (result.IsSuccess && result.Value == StatusUslugi.UslugaDostepna)
        {
            return result;
        }

        Exception exception = result switch
        {
            { IsSuccess: true, Value: StatusUslugi.UslugaNiedostepna } => RegonException.ServiceUnavailable,
            { IsSuccess: true, Value: StatusUslugi.PrzerwaTechniczna } => RegonException.MaintenanceBreak,
            _ => new NotImplementedException($"IsSuccess : {result.IsSuccess}, StatusUslugi : {result.Value}"),
        };
        return OperationResult.Failed<StatusUslugi>(exception.Message, exception);
    }
}

internal sealed class KomunikatTrescOperation(
    IHttpClientFactory factory,
    ISessionManager sessionManager,
    Request.GetValue request
    ) : GetValueOperation<string>(
    factory,
    sessionManager,
    request,
    getValue => getValue.KomunikatTresc(),
    new FirstValueToFirstValueOperation(),
    true)
{
    public override string Name => nameof(KomunikatTrescOperation);
}

internal sealed class KomunikatKodOperation(
    IHttpClientFactory factory,
    ISessionManager sessionManager,
    Request.GetValue request
    ) : GetValueOperation<KomunikatKod>(
    factory,
    sessionManager,
    request,
    getValue => getValue.KomunikatKod(),
    new FirstValueToEnumOperation<KomunikatKod>(),
    true)
{
    public override string Name => nameof(KomunikatKodOperation);
}

internal sealed class StatusSesjiOperation(
    IHttpClientFactory factory,
    ISessionManager sessionManager,
    Request.GetValue request
    ) : GetValueOperation<StatusSesji>(
    factory,
    sessionManager,
    request,
    getValue => getValue.StatusSesji(),
    new FirstValueToEnumOperation<StatusSesji>(),
    true)
{
    public override string Name => nameof(StatusSesjiOperation);
}

internal sealed class StanDanychOperation(
    IHttpClientFactory factory,
    ISessionManager sessionManager,
    Request.GetValue request
    ) : GetValueOperation<DateOnly>(
    factory,
    sessionManager,
    request,
    getValue => getValue.StanDanych(),
    new FirstValueToDateOnlyOperation(),
    true)
{
    public override string Name => nameof(StanDanychOperation);
}
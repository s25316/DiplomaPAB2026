using Base.Pipelines.Operations;
using GUS.REGON.Configurations;
using GUS.REGON.Extensions;
using GUS.REGON.Interfaces;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.Models.Results;
using GUS.REGON.Operations.Primitives;
using GUS.REGON.Operations.Primitives.ElementsTo;
using RegonResponse = GUS.REGON.Models.Responses.Response;

namespace GUS.REGON.Operations.Workflows;

internal abstract class GetDataOperation<TItem>(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    KomunikatKodOperation komunikatKodOperation
) where TItem : class
{
    private const int MAX_RETRY = 3;
    public abstract string Name { get; }

    public async Task<RegonResult<IEnumerable<TItem>>> ExecuteAsync(string input, CancellationToken cancellationToken = default)
    {
        var operationInput = new RequestInput(input, true);

        var result = await AsyncOperation
            .Create(new RequestToDocumentOperation(clientFactory, sessionManager))
            .Add(new DocumentToElementsOperation(ElementDefinition.Dane))
            .Add(new ElementsToClassesOperation<TItem>())
            .Add(Name, async (input, cancellationToken, handler) =>
            {
                var retryCount = MAX_RETRY;
                do
                {
                    retryCount--;

                    var result = await handler(input, cancellationToken);
                    if (result.IsFailure)
                    {
                        return OperationResult.Failed<RegonResult<IEnumerable<TItem>>>(result.Error);
                    }
                    if (result.IsSuccess && result.Value.Any())
                    {
                        return OperationResult.Success(SuccessRegonResult(result.Value));
                    }


                    var komunikatKodResult = await komunikatKodOperation.ExecuteAsync(cancellationToken);
                    switch (komunikatKodResult)
                    {
                        case { IsFailure: true, StatusUslugi: not StatusUslugi.UslugaDostepna }:
                            return OperationResult.Success(FailedRegonResult(null, komunikatKodResult.StatusUslugi));

                        case { IsSuccess: true, Value: KomunikatKod.DaneSzukajWieleIdentyfikatorow }:
                        case { IsSuccess: true, Value: KomunikatKod.KodCaptcha }:
                            throw new NotImplementedException(nameof(komunikatKodResult.Value));

                        case { IsSuccess: true, Value: KomunikatKod.NieZnalezionoPodmiotów }:
                        case { IsSuccess: true, Value: KomunikatKod.BrakUprawnienDoRaportu }:
                            return OperationResult.Success(FailedRegonResult(komunikatKodResult.Value));

                        default:
                            await sessionManager.UpdateSessionAsync(cancellationToken);
                            continue;
                    }
                } while (retryCount > 0);

                throw new InvalidOperationException("Service is available but can not return reason of invalid Request");
            }).ExecuteAsync(operationInput, cancellationToken);

        if (result.IsFailure) throw result.Error.MapToRegonException();
        return result.Value;
    }

    private static RegonResult<IEnumerable<TItem>> SuccessRegonResult(IEnumerable<TItem> items)
        => RegonResult.Success(items);

    private static RegonResult<IEnumerable<TItem>> FailedRegonResult(
        KomunikatKod? komunikatKod,
        StatusUslugi statusUslugi = StatusUslugi.UslugaDostepna
    ) => RegonResult.Failed<IEnumerable<TItem>>(komunikatKod, statusUslugi);
}

internal class DaneSzukajOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    KomunikatKodOperation komunikatKodOperation
) : GetDataOperation<RegonResponse.DaneSzukaj>(
    clientFactory,
    sessionManager,
    komunikatKodOperation)
{
    private const string NAME = nameof(DaneSzukajOperation);
    public override string Name => NAME;
}

internal class RaportJednostkiOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    KomunikatKodOperation komunikatKodOperation
) : GetDataOperation<RegonResponse.RaportJednostki>(
    clientFactory,
    sessionManager,
    komunikatKodOperation)
{
    private const string NAME = nameof(RaportJednostkiOperation);
    public override string Name => NAME;
}
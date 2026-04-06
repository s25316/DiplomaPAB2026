using Base.Pipelines.Operations;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Errors;
using GUS.REGON.Extensions;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.Models.Results;
using GUS.REGON.Operations.Primitives;
using GUS.REGON.Operations.Primitives.ElementsTo;
using GUS.REGON.ValueObjects;
using static GUS.REGON.Errors.RegonOperationError;

namespace GUS.REGON.Operations.Workflows;

internal class ZalogujOperation(
    IHttpClientFactory clientFactory,
    StatusUslugiOperation statusUslugiOperation,
    Request.Zaloguj request,
    Key key)
{
    private const string NAME = nameof(ZalogujOperation);
    public string Name => NAME;


    public async Task<RegonBaseResult<string>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var requestEnvelope = request.Generate(key);
        var input = new RequestInput(requestEnvelope, false);

        var result = await AsyncOperation
            .Create(new RequestToDocumentOperation(clientFactory))
            .Add(new DocumentToElementsOperation(ElementDefinition.Zaloguj))
            .Add(new ElementsToFirstValueOperation())
            .ExecuteAsync(input, cancellationToken);

        switch (result)
        {
            case { IsSuccess: true }:
                return RegonBaseResult.Success<string>(result.Value);

            case { IsFailure: true, Error: EmptyFirstValue }:
                var statusUslugiResult = await statusUslugiOperation.ExecuteAsync(cancellationToken);
                if (statusUslugiResult is StatusUslugi.UslugaDostepna)
                {
                    throw new RegonException.InvalidKey($"{Messages.KeyErrorMessageInvalid}: {key.Value}");
                }
                return RegonBaseResult.Failed<string>(statusUslugiResult);

            default:
                throw result.Error.MapToRegonException();
        }
    }
}
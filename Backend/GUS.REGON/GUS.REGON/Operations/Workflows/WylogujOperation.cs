using Base.Pipelines.Operations;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Extensions;
using GUS.REGON.Interfaces;
using GUS.REGON.Models.Results;
using GUS.REGON.Operations.Primitives;
using GUS.REGON.Operations.Primitives.ElementsTo;
using GUS.REGON.Operations.Primitives.FirstValueTo;
using static GUS.REGON.Errors.RegonOperationError;

namespace GUS.REGON.Operations.Workflows;

internal class WylogujOperation(
    IHttpClientFactory clientFactory,
    ISessionManager sessionManager,
    StatusUslugiOperation statusUslugiOperation,
    Request.Wyloguj request
    )
{
    private const string NAME = nameof(WylogujOperation);
    public string Name => NAME;


    public async Task<RegonBaseResult<bool>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var session = sessionManager.Session;
        if (session.IsExpired)
        {
            return RegonBaseResult.Success(true);
        }

        var requestEnvelope = request.Generate(session.SessionId);
        var input = new RequestInput(requestEnvelope, true);

        var result = await AsyncOperation
            .Create(new RequestToDocumentOperation(clientFactory, sessionManager))
            .Add(new DocumentToElementsOperation(ElementDefinition.Wyloguj))
            .Add(new ElementsToFirstValueOperation())
            .Add(new FirstValueToBoolOperation())
            .ExecuteAsync(input, cancellationToken);


        switch (result)
        {
            case { IsSuccess: true }:
                return RegonBaseResult.Success(result.Value);

            case { IsFailure: true, Error: EmptyFirstValue }:
                return RegonBaseResult.Success(true);

            default:
                throw result.Error.MapToException();
        }
    }
}
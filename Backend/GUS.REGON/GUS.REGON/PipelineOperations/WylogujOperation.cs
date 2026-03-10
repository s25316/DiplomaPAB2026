using Base.Pipelines;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Interfaces;
using GUS.REGON.PipelineOperations.Base;
using GUS.REGON.PipelineOperations.Base.ElementsTo;
using GUS.REGON.PipelineOperations.Base.FirstValueTo;

namespace GUS.REGON.PipelineOperations;

internal class WylogujOperation(
    IHttpClientFactory factory,
    ISessionManager sessionManager,
    Request.Wyloguj request
    ) : IAsyncOperation<bool>
{
    public string Name => nameof(WylogujOperation);


    public async Task<OperationResult<bool>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        using var client = factory.CreateClient(HttpClientNames.BASE);
        var session = sessionManager.Session;

        if (session.IsExpired)
        {
            return OperationResult.Success(true);
        }

        var requestEnvelope = request.Generate(session.SessionId);

        return await PipelineBuilder
            .Create(new MakeRequestOperation(client, sessionManager))
            .Add(new ResponseToEnvelopeOperation())
            .Add(new EnvelopeToDocumentOperation())
            .Add(new DocumentToElementsOperation(ElementDefinition.Wyloguj))
            .Add(new ElementsToFirstValueOperation())
            .Add(new FirstValueToBoolOperation())
            .ExecuteAsync(requestEnvelope, cancellationToken);
    }
}
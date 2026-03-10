using Base.Pipelines;
using Base.Pipelines.Interfaces.Operations;
using Base.Pipelines.Models;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.PipelineOperations.Base;
using GUS.REGON.PipelineOperations.Base.ElementsTo;
using GUS.REGON.ValueObjects;

namespace GUS.REGON.PipelineOperations;

internal class ZalogujOperation(IHttpClientFactory factory, Request.Zaloguj request, Key key) : IAsyncOperation<string>
{
    public string Name => nameof(ZalogujOperation);


    public async Task<OperationResult<string>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        using var client = factory.CreateClient(HttpClientNames.BASE);
        var requestEnvelope = request.Generate(key);

        return await PipelineBuilder
            .Create(new MakeRequestOperation(client))
            .Add(new ResponseToEnvelopeOperation())
            .Add(new EnvelopeToDocumentOperation())
            .Add(new DocumentToElementsOperation(ElementDefinition.Zaloguj))
            .Add(new ElementsToFirstValueOperation())
            .ExecuteAsync(requestEnvelope, cancellationToken);
    }
}
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Diploma.API.OpenApi;

public class ServersCleanDocumentTransformer : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Servers?.Clear();
    }
}
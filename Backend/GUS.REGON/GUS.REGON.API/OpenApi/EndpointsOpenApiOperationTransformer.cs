using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace GUS.REGON.API.OpenApi;

public class EndpointsOpenApiOperationTransformer : IOpenApiOperationTransformer
{
    private const string API_DESCRIPTION_XML = "ApiDescription.xml";

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, API_DESCRIPTION_XML);

        if (!File.Exists(xmlPath))
            return Task.CompletedTask;

        var methodInfo = context.Description.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

        if (methodInfo is null)
            return Task.CompletedTask;

        var methodName = methodInfo.ActionName;

        var doc = new System.Xml.XmlDocument();
        doc.Load(xmlPath);

        var summaryNode = doc.SelectSingleNode($"/docs/members/member[@name='{methodName}_Summary']/summary");
        if (summaryNode != null)
        {
            operation.Summary = summaryNode.InnerText;
        }

        var descriptionNode = doc.SelectSingleNode($"/docs/members/member[@name='{methodName}_Description']/summary");
        if (descriptionNode != null)
        {
            operation.Description = descriptionNode.InnerText;
        }

        return Task.CompletedTask;
    }
}
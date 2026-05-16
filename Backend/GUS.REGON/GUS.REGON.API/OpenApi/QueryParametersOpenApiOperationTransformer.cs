using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.ComponentModel;
using System.Reflection;
using DisplayAttribute = System.ComponentModel.DataAnnotations.DisplayAttribute;

namespace GUS.REGON.API.OpenApi;

public class QueryParametersOpenApiOperationTransformer : IOpenApiOperationTransformer
{
    public async Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        if (operation.Parameters is null)
            return;

        foreach (var apiParam in operation.Parameters)
        {
            var propMetadata = context.Description.ParameterDescriptions
                .FirstOrDefault(p => string.Equals(p.Name, apiParam.Name, StringComparison.OrdinalIgnoreCase));

            var containerType = propMetadata?.ModelMetadata?.ContainerType;
            if (containerType is null)
                continue;

            var prop = containerType.GetProperty(propMetadata!.ModelMetadata.PropertyName!);
            if (prop is null)
                continue;

            var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
            var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();

            string? description = displayAttr?.GetName() ?? descAttr?.Description;

            if (string.IsNullOrEmpty(description))
                continue;

            apiParam.Description = description;
        }
    }
}
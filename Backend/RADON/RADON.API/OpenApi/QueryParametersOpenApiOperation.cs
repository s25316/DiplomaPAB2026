using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.ComponentModel;
using System.Reflection;
using DisplayAttribute = System.ComponentModel.DataAnnotations.DisplayAttribute;

namespace RADON.API.OpenApi;

public class QueryParametersOpenApiOperation : IOpenApiOperationTransformer
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
            if (containerType != null)
            {
                var prop = containerType.GetProperty(propMetadata!.ModelMetadata.PropertyName!);
                if (prop != null)
                {
                    var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                    var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();

                    string? description = displayAttr?.GetName() ?? descAttr?.Description;

                    if (!string.IsNullOrEmpty(description))
                    {
                        apiParam.Description = description;
                    }
                }
            }
        }
    }
}
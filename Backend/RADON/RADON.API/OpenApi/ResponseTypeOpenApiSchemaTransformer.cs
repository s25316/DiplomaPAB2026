using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using DisplayAttribute = System.ComponentModel.DataAnnotations.DisplayAttribute;

namespace RADON.API.OpenApi;

public class ResponseTypeOpenApiSchemaTransformer : IOpenApiSchemaTransformer
{
    public async Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var type = context.JsonTypeInfo.Type;
        if (schema.Properties == null)
            return;

        var classDisplayAttr = type.GetCustomAttribute<DisplayAttribute>();
        var classDescAttr = type.GetCustomAttribute<DescriptionAttribute>();

        string? classDescription = classDisplayAttr?.GetName() ?? classDescAttr?.Description;

        if (!string.IsNullOrEmpty(classDescription))
        {
            schema.Description = classDescription;
        }


        foreach (var property in type.GetProperties())
        {
            var displayAttr = property.GetCustomAttribute<DisplayAttribute>();

            var descAttr = property.GetCustomAttribute<DescriptionAttribute>();

            string? descriptionText = displayAttr?.GetName() ?? descAttr?.Description;

            if (string.IsNullOrEmpty(descriptionText))
                continue;

            var jsonPropertyName = property
                .GetCustomAttribute<JsonPropertyNameAttribute>()?
                .Name
                ?? JsonPropertyName(property.Name);

            if (schema.Properties.TryGetValue(jsonPropertyName, out var openApiProperty))
            {
                openApiProperty.Description = descriptionText;
            }
        }
    }

    public static string JsonPropertyName(string name) => char.ToLowerInvariant(name[0]) + name.Substring(1);
}
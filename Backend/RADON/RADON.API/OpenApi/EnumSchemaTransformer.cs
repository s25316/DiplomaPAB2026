using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using RADON.Dictionaries;
using System.Reflection;
using System.Text.Json.Nodes;
using DisplayAttribute = System.ComponentModel.DataAnnotations.DisplayAttribute;

namespace RADON.API.OpenApi;

public class EnumSchemaTransformer : IOpenApiSchemaTransformer
{
    public async Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type.IsEnum && context.JsonTypeInfo.Type == typeof(DictionaryType))
        {
            var enumType = context.JsonTypeInfo.Type;
            var descriptions = new List<string>();

            foreach (var name in Enum.GetNames(enumType))
            {
                var field = enumType.GetField(name);

                if (field != null)
                {
                    var displayAttr = field.GetCustomAttribute<DisplayAttribute>();

                    string? label = displayAttr?.GetName();

                    descriptions.Add($"`{name}`: {label ?? name}");
                }
            }

            if (descriptions.Any())
            {
                schema.Description = "### Typy słowników\n" + string.Join("\n\n", descriptions);
            }

            if (context.JsonTypeInfo.Type.IsEnum)
            {
                schema.Type = JsonSchemaType.String;

                var names = Enum.GetNames(context.JsonTypeInfo.Type);

                schema.Enum = names
                    .Select(name => JsonValue.Create(name))
                    .Cast<JsonNode>()
                    .ToList();
            }
        }
    }
}
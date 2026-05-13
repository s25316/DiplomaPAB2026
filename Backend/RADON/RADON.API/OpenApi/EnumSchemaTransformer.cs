using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using RADON.Contracts.Dictionaries;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Nodes;
using DisplayAttribute = System.ComponentModel.DataAnnotations.DisplayAttribute;

namespace RADON.API.OpenApi;

public class EnumSchemaTransformer : IOpenApiSchemaTransformer
{
    private static readonly ConcurrentDictionary<Type, string> DescriptionCache = new();
    private static readonly ConcurrentDictionary<Type, List<JsonNode>> EnumValuesCache = new();


    public async Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var type = context.JsonTypeInfo.Type;

        if (!type.IsEnum)
            return;

        schema.Type = JsonSchemaType.String;
        schema.Enum = GetEnumValues(type);

        // ONLY FOR DictionaryResource
        if (type == typeof(DictionaryResource))
        {
            schema.Description = GetDictionaryDescription(type);
        }
    }

    private static List<JsonNode> GetEnumValues(Type enumType)
    {
        return EnumValuesCache.GetOrAdd(enumType, t =>
            Enum.GetNames(t)
                .Select(name => (JsonNode)JsonValue.Create(name)!)
                .ToList());
    }

    private static string GetDictionaryDescription(Type enumType)
    {
        return DescriptionCache.GetOrAdd(enumType, t =>
        {
            var descriptions = t.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field =>
                {
                    var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
                    var label = displayAttr?.GetName() ?? field.Name;
                    return $"`{field.Name}`: {label}";
                });

            return "### Typy słowników\n\n" + string.Join("\n\n", descriptions);
        });
    }
}
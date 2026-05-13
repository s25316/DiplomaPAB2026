using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.Collections.Concurrent;
using System.Text.Json.Nodes;

namespace GUS.REGON.API.OpenApi;

public class EnumSchemaTransformer : IOpenApiSchemaTransformer
{
    private static readonly ConcurrentDictionary<Type, List<JsonNode>> EnumValuesCache = new();


    public async Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var type = context.JsonTypeInfo.Type;

        if (!type.IsEnum)
            return;

        schema.Type = JsonSchemaType.String;
        schema.Enum = GetEnumValues(type);

    }

    private static List<JsonNode> GetEnumValues(Type enumType)
    {
        return EnumValuesCache.GetOrAdd(enumType, t =>
            Enum.GetNames(t)
                .Select(name => (JsonNode)JsonValue.Create(name)!)
                .ToList());
    }
}
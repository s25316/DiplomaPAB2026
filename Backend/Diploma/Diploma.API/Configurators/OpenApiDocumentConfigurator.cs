using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;

namespace Diploma.API.Configurators;

public class OpenApiDocumentConfigurator(IEnumerable<GatewayOpenApiConfiguration> configurations)
{
    public JsonNode Build(Uri mainHostUri)
    {
        var stringHostUri = ExtractHostUri(mainHostUri);
        var openApiDocumentUri = $"{stringHostUri}/openapi/v1.json";
        if (!TryGetDocument(new Uri(openApiDocumentUri), out var mainDocument))
            throw new InvalidOperationException("Main app not returns openapi document.");

        if (mainDocument["paths"] is not JsonObject mainPath)
        {
            mainPath = new JsonObject();
            mainDocument["paths"] = mainPath;
        }

        if (mainDocument["components"] is not JsonObject mainComponents)
        {
            mainComponents = new JsonObject();
            mainDocument["components"] = mainComponents;
        }

        if (mainComponents["schemas"] is not JsonObject mainSchemas)
        {
            mainSchemas = new JsonObject();
            mainComponents["schemas"] = mainSchemas;
        }


        var nodes = new List<JsonNode>();
        foreach (var configuration in configurations)
        {
            var mappedDocument = Map(configuration);
            if (mappedDocument is not null)
                nodes.Add(mappedDocument);
        }


        foreach (var document in nodes)
        {
            var paths = document["paths"]?.AsObject();
            foreach (var (path, node) in paths ?? [])
            {
                mainPath[path] = node?.DeepClone();
            }

            var schemas = document["components"]?["schemas"]?.AsObject();
            foreach (var (schema, node) in schemas ?? [])
            {
                mainSchemas[schema] = node?.DeepClone();
            }
        }

        mainDocument["servers"] = new JsonArray();
        return mainDocument;
    }

    private static string ExtractHostUri(Uri hostUri)
    {
        var stringHostUri = hostUri.AbsoluteUri;

        if (!stringHostUri.EndsWith('/'))
            return stringHostUri;

        return stringHostUri[..^1];
    }

    private static bool TryGetDocument(Uri documentUri, [NotNullWhen(true)] out JsonNode? document)
    {
        document = null;

        using var client = new HttpClient();
        var response = client
            .GetAsync(documentUri.AbsoluteUri)
            .GetAwaiter()
            .GetResult();

        if (!response.IsSuccessStatusCode)
            return false;


        var stringDocument = response
            .Content
            .ReadAsStringAsync()
            .GetAwaiter()
            .GetResult();

        if (string.IsNullOrWhiteSpace(stringDocument))
            return false;


        document = JsonNode.Parse(stringDocument);
        return document is not null;
    }


    private JsonNode? Map(GatewayOpenApiConfiguration configuration)
    {
        if (!TryGetDocument(configuration.DocumentUri, out var document))
            return null;


        var paths = document["paths"]?.AsObject();
        if (paths is null)
            return document;

        var clonedPaths = new JsonObject();
        foreach (var (path, node) in paths.ToList())
        {
            if (!path.StartsWith(configuration.SourceApiPathPrefix))
                continue;

            var modifiedPath = path.Substring(configuration.SourceApiPathPrefix.Length);

            if (modifiedPath.StartsWith('/'))
                modifiedPath = modifiedPath[1..];

            var gatewayPath = $"{configuration.CurrentApiPathPrefix}/{modifiedPath}";
            clonedPaths[gatewayPath] = node?.DeepClone();
        }
        document["paths"] = clonedPaths;


        var schemaPrefix = configuration.Name;
        schemaPrefix = char.ToUpper(schemaPrefix[0]) + schemaPrefix.Substring(1);

        var schemas = document["components"]?["schemas"]?.AsObject();
        if (schemas != null)
        {
            var clonedSchemas = new JsonObject();

            foreach (var (schemaName, node) in schemas)
            {
                var uniqueSchemaName = $"{schemaPrefix}{schemaName}";
                clonedSchemas[uniqueSchemaName] = node?.DeepClone();
            }

            document["components"]?["schemas"] = clonedSchemas;
        }
        UpdateRefsInJson(document, schemaPrefix);

        return document;
    }

    private void UpdateRefsInJson(JsonNode? node, string prefix)
    {
        if (node is JsonObject obj)
        {
            if (obj.TryGetPropertyValue("$ref", out var refNode) && refNode is JsonValue val && val.TryGetValue<string>(out var refStr))
            {
                if (refStr.StartsWith("#/components/schemas/"))
                {
                    var originalSchema = refStr.Substring("#/components/schemas/".Length);
                    obj["$ref"] = $"#/components/schemas/{prefix}{originalSchema}";
                }
            }
            else
            {
                foreach (var property in obj.ToList())
                {
                    UpdateRefsInJson(property.Value, prefix);
                }
            }
        }
        else if (node is JsonArray array)
        {
            foreach (var element in array)
            {
                UpdateRefsInJson(element, prefix);
            }
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using Yarp.ReverseProxy.Configuration;

namespace Diploma.API.Configurators;

public class OpenApiDocumentConfigurator
{
    private sealed record OpenApiDocumentConfiguration
    {
        public required string Name { get; init; }
        public required Uri HostUri { get; init; }
        public required string PathPrefix { get; init; }
        public required string PathRemovePrefix { get; init; }
        public required RouteConfig RouteConfig { get; init; }
        public required ClusterConfig ClusterConfig { get; init; }
    }


    private const string ROUTE_PREFIX = "route";
    private const string CLUSTER_PREFIX = "cluster";
    private const string DESTINATION_PREFIX = "destination";

    private readonly Dictionary<string, OpenApiDocumentConfiguration> configurations = [];

    public IReadOnlyList<RouteConfig> RouteConfigs => [.. configurations.Select(i => i.Value.RouteConfig)];
    public IReadOnlyList<ClusterConfig> ClusterConfigs => [.. configurations.Select(i => i.Value.ClusterConfig)];


    public OpenApiDocumentConfigurator Add(string hostName, Uri hostUri)
    {
        if (configurations.ContainsKey(hostName))
            return this;

        if (TryGetConfiguration(hostName, hostUri, out var configuration))
            configurations[configuration.Name] = configuration;

        return this;
    }

    public JsonNode Build(Uri mainHostUri)
    {
        if (!TryGetDocument(mainHostUri, out var mainDocument))
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
        foreach (var (_, configuration) in configurations)
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


    private static string PrepareApiPath(string serviceName) => $"/api/{serviceName}";
    private static string ExtractHostUri(Uri hostUri)
    {
        var stringHostUri = hostUri.AbsoluteUri;

        if (!stringHostUri.EndsWith('/'))
            return stringHostUri;

        return stringHostUri[..^1];
    }

    private static bool TryGetDocument(Uri hostUri, [NotNullWhen(true)] out JsonNode? document)
    {
        document = null;
        var stringHostUri = ExtractHostUri(hostUri);

        using var client = new HttpClient();
        var openApiUri = $"{stringHostUri}/openapi/v1.json";

        var response = client
            .GetAsync(openApiUri)
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

    private static bool TryGetConfiguration(string hostName, Uri hostUri, [NotNullWhen(true)] out OpenApiDocumentConfiguration? configuration)
    {
        configuration = null;
        hostName = hostName.ToLowerInvariant();

        if (!TryGetDocument(hostUri, out var document))
            return false;

        var stringHostUri = ExtractHostUri(hostUri);
        var routeId = $"{hostName}-{ROUTE_PREFIX}";
        var clusterId = $"{hostName}-{CLUSTER_PREFIX}";
        var destination = $"{hostName}-{DESTINATION_PREFIX}";
        var pathPrefix = PrepareApiPath(hostName);
        var pathRemovePrefix = "/api";

        var routeConfig = new RouteConfig
        {
            RouteId = routeId,
            ClusterId = clusterId,
            Match = new RouteMatch { Path = pathPrefix + "/{**catch-all}" },
            Transforms = new[]
            {
                new Dictionary<string, string>
                {
                    { "PathRemovePrefix", pathPrefix }
                },
                new Dictionary<string, string>
                {
                    { "PathPrefix", pathRemovePrefix }
                }
            }
        };
        var clusterConfig = new ClusterConfig
        {
            ClusterId = clusterId,
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { destination, new DestinationConfig { Address = stringHostUri } }
            }
        };


        configuration = new OpenApiDocumentConfiguration
        {
            Name = hostName,
            HostUri = hostUri,
            PathPrefix = pathPrefix,
            PathRemovePrefix = pathRemovePrefix,
            RouteConfig = routeConfig,
            ClusterConfig = clusterConfig,
        };
        return true;
    }


    private JsonNode? Map(OpenApiDocumentConfiguration configuration)
    {
        if (!TryGetDocument(configuration.HostUri, out var document))
            return null;


        var paths = document["paths"]?.AsObject();
        if (paths is null)
            return document;

        var clonedPaths = new JsonObject();
        foreach (var (path, node) in paths.ToList())
        {
            if (!path.StartsWith(configuration.PathRemovePrefix))
                continue;

            var modifiedPath = path.Substring(configuration.PathRemovePrefix.Length);

            if (modifiedPath.StartsWith('/'))
                modifiedPath = modifiedPath[1..];

            var gatewayPath = $"{configuration.PathPrefix}/{modifiedPath}";
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
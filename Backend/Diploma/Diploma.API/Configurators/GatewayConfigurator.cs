using System.Text.Json.Nodes;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Diploma.API.Configurators;

public record GatewayOpenApiConfiguration
{
    public required string Name { get; init; }
    public required string SourceApiPathPrefix { get; init; }
    public required string CurrentApiPathPrefix { get; init; }
    public required Uri DocumentUri { get; init; }
}

public class GatewayConfigurator
{
    [Flags]
    public enum Type
    {
        Rest = 1,
        GraphQl = 2,
    }

    private abstract record GatewayConfiguration
    {
        public required string Name { get; init; }
        public required RouteConfig RouteConfig { get; init; }
        public required ClusterConfig ClusterConfig { get; init; }

        public sealed record Rest : GatewayConfiguration
        {
            public required string SourceApiPathPrefix { get; init; }
            public required string CurrentApiPathPrefix { get; init; }
            public required Uri DocumentUri { get; init; }
        }

        public sealed record GraphQl : GatewayConfiguration
        {
            public required string ApiPathPrefix { get; init; }
        }
    }

    private const string ROUTE_PREFIX = "route";
    private const string CLUSTER_PREFIX = "cluster";
    private const string DESTINATION_PREFIX = "destination";
    private const string REST_PREFIX = "rest";
    private const string GRAPHQL_PREFIX = "graphql";

    private readonly List<GatewayConfiguration> configurations = [];

    public IEnumerable<RouteConfig> RouteConfigs => configurations.Select(i => i.RouteConfig);
    public IEnumerable<ClusterConfig> ClusterConfigs => configurations.Select(i => i.ClusterConfig);

    public IEnumerable<GatewayOpenApiConfiguration> OpenApiConfigurations => configurations
        .OfType<GatewayConfiguration.Rest>()
        .Select(i => new GatewayOpenApiConfiguration
        {
            Name = i.Name,
            CurrentApiPathPrefix = i.CurrentApiPathPrefix,
            SourceApiPathPrefix = i.SourceApiPathPrefix,
            DocumentUri = i.DocumentUri,
        });

    public IEnumerable<string> GraphQlPathPrefixes => configurations
        .OfType<GatewayConfiguration.GraphQl>()
        .Select(i => i.ApiPathPrefix);



    public GatewayConfigurator Add(Uri hostUri, string hostName, Type type)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(hostName);


        if (type.HasFlag(Type.GraphQl))
            AddGraphQl(hostUri, hostName);

        if (type.HasFlag(Type.Rest))
            AddRestAsync(hostUri, hostName)
                .GetAwaiter()
                .GetResult();

        return this;
    }


    private static string PrepareApiPath(string serviceName) => $"/api/{serviceName}";
    private static string ExtractHostUri(Uri hostUri)
    {
        var stringHostUri = hostUri.AbsoluteUri;

        if (!stringHostUri.EndsWith('/'))
            return stringHostUri;

        return stringHostUri[..^1];
    }
    private static async Task<bool> IsExistOpenApiDocumentAsync(string uri)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
            return false;

        var stringDocument = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(stringDocument))
            return false;

        var document = JsonNode.Parse(stringDocument);
        return document is not null;
    }

    private async Task AddRestAsync(Uri hostUri, string hostName)
    {
        var stringHostUri = ExtractHostUri(hostUri);
        var openApiDocumentUri = $"{stringHostUri}/openapi/v1.json";

        var isExist = await IsExistOpenApiDocumentAsync(openApiDocumentUri);
        if (!isExist)
            return;

        var clusterId = $"{hostName}-{REST_PREFIX}-{CLUSTER_PREFIX}";

        var currentApiPathPrefix = PrepareApiPath(hostName);
        var sourceApiPathPrefix = "/api";

        var routeConfig = new RouteConfig
        {
            RouteId = $"{hostName}-{REST_PREFIX}-{ROUTE_PREFIX}",
            ClusterId = clusterId,
            Match = new RouteMatch { Path = currentApiPathPrefix + "/{**catch-all}" },
            Transforms = new[]
            {
                new Dictionary<string, string>
                {
                    { "PathRemovePrefix", currentApiPathPrefix }
                },
                new Dictionary<string, string>
                {
                    { "PathPrefix", sourceApiPathPrefix }
                }
            }
        };

        var clusterConfig = new ClusterConfig
        {
            ClusterId = clusterId,
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { $"{hostName}-{REST_PREFIX}-{DESTINATION_PREFIX}", new DestinationConfig { Address = stringHostUri } }
            }
        };

        configurations.Add(new GatewayConfiguration.Rest
        {
            Name = hostName,
            ClusterConfig = clusterConfig,
            RouteConfig = routeConfig,
            CurrentApiPathPrefix = currentApiPathPrefix,
            SourceApiPathPrefix = sourceApiPathPrefix,
            DocumentUri = new Uri(openApiDocumentUri),
        });
    }

    private void AddGraphQl(Uri hostUri, string hostName)
    {
        var stringHostUri = ExtractHostUri(hostUri);

        var pathPrefix = $"/{hostName}/graphql/";
        var clusterId = $"{hostName}-{GRAPHQL_PREFIX}-{CLUSTER_PREFIX}";

        var routeConfig = new RouteConfig
        {
            RouteId = $"{hostName}-{GRAPHQL_PREFIX}-{ROUTE_PREFIX}",
            ClusterId = clusterId,
            Match = new RouteMatch
            {
                Path = pathPrefix,
                Methods = new[] { "POST" }
            }
        }.WithTransformPathSet(new PathString("/graphql"));

        var clusterConfig = new ClusterConfig
        {
            ClusterId = clusterId,
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { $"{hostName}-{GRAPHQL_PREFIX}-{DESTINATION_PREFIX}", new DestinationConfig { Address = stringHostUri } }
            },
        };

        configurations.Add(new GatewayConfiguration.GraphQl
        {
            Name = hostName,
            ClusterConfig = clusterConfig,
            RouteConfig = routeConfig,
            ApiPathPrefix = pathPrefix
        });
    }
}
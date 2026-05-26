using Base.Models.ValueObjects.Regony;
using Diploma.API.Configurators;
using Diploma.API.GraphQL;
using HotChocolate.Types;
using Microsoft.Extensions.Caching.Memory;
using Scalar.AspNetCore;
using System.Net.Mime;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Diploma.API;

public class Program
{
    public static void Main(string[] args)
    {
        // 2. Definicja YARP w kodzie C# z filtrowaniem po metodzie HTTP (POST)
        var routes = new[]
        {
            new RouteConfig
            {
                RouteId = "s1-data-route",
                ClusterId = "s1-cluster",
                Match = new RouteMatch
                {
                    Path = "/graphql/s1",
                    Methods = new[] { "POST" } // Przechwytuje TYLKO zapytania o dane/introspekcjê
                }
            }.WithTransformPathSet(new PathString("/graphql")), // <- POPRAWKA: Pewne i czyste nadpisanie œcie¿ki dla backendu

            new RouteConfig
            {
                RouteId = "s2-data-route",
                ClusterId = "s2-cluster",
                Match = new RouteMatch
                {
                    Path = "/graphql/s2",
                    Methods = new[] { "POST" }
                }
            }.WithTransformPathSet(new PathString("/graphql")) // <- POPRAWKA: Pewne i czyste nadpisanie œcie¿ki dla backendu
        };

        var clusters = new[]
        {
            new ClusterConfig
            {
                ClusterId = "s1-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "s1-backend", new DestinationConfig { Address = "http://localhost:8081" } }
                }
            },
            new ClusterConfig
            {
                ClusterId = "s2-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "s2-backend", new DestinationConfig { Address = "http://localhost:8082" } }
                }
            }
        };


        var configurator = new OpenApiDocumentConfigurator()
            .Add("radon", new Uri("http://localhost:8081"))
            .Add("regon", new Uri("http://localhost:8082"));

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient();
        builder.Services.AddMemoryCache();
        builder.Services.AddProblemDetails();

        builder.Services.AddReverseProxy().LoadFromMemory(
            configurator.RouteConfigs.Concat(routes).ToList(),
            configurator.ClusterConfigs.Concat(clusters).ToList());
        builder.Services.AddControllers();

        builder.Services.AddOpenApi();
        builder.Services.AddOpenApi("gateway");

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(p => p
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });


        builder.Services
            .AddGraphQLServer()
            .AddQueryType(d => d.Name(OperationTypeNames.Query))
            .AddTypeExtension<ServerQuery>()
            .BindRuntimeType<Regon, RegonScalar>();

        var app = builder.Build();

        app.MapGet("api/test", () =>
        {
            return Results.Ok();
        });


        app.MapGet("/openapi/gateway.json", async (IMemoryCache cache, HttpContext context) =>
        {
            var request = context.Request;
            var baseHostUrl = $"{request.Scheme}://{request.Host}";

            string cacheKey = $"openapi-gateway-{baseHostUrl.ToLowerInvariant()}";

            string jsonResponse = cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                entry.Priority = CacheItemPriority.High;
                var document = configurator.Build(new Uri(baseHostUrl));
                return document.ToJsonString();
            }) ?? throw new InvalidOperationException($"{nameof(IMemoryCache)} not returns json.");

            return Results.Content(jsonResponse, MediaTypeNames.Application.Json);
        }).ExcludeFromDescription();

        app.UseCors();
        app.UseExceptionHandler();

        app.MapGraphQL();
        app.MapNitroApp("/graphql/s1", relativeRequestPath: "/graphql/s1");
        app.MapNitroApp("/graphql/s2", relativeRequestPath: "/graphql/s2");

        app.MapOpenApi();
        app.MapScalarApiReference();
        app.MapScalarApiReference("/scalar/gateway", options =>
        {
            options
                .WithTitle("API Gateway")
                .WithOpenApiRoutePattern("/openapi/gateway.json")
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.MapReverseProxy();

        app.Run();
    }
}
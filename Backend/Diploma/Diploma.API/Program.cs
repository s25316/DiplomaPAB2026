using Scalar.AspNetCore;
using Yarp.ReverseProxy.Configuration;

namespace Diploma.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. POPRAWIONA KONFIGURACJA TRAS YARP
        // Używamy catch-all w Match i przekazujemy przechwyconą końcówkę prosto do struktury /api/{remainder}
        var routes = new[]
        {
            new RouteConfig
            {
                RouteId = "service1-route",
                ClusterId = "service1-cluster",
                // Zmieniamy wzorzec na standardowy catch-all
                Match = new RouteMatch { Path = "/api/service1/{**catch-all}" },
                Transforms = new[]
                {
                    new Dictionary<string, string>
                    {
                        // Wycinamy "/api/service1", zachowując oryginalne ukośniki w reszcie ścieżki
                        { "PathRemovePrefix", "/api/service1" }
                    },
                    new Dictionary<string, string>
                    {
                        // Doklejamy na początku czysty przedrostek "/api", jeśli mikroserwis go wymaga
                        { "PathPrefix", "/api" }
                    }
                }
            },

            new RouteConfig
            {
                RouteId = "service2-route",
                ClusterId = "service2-cluster",
                Match = new RouteMatch { Path = "/api/service2/{**catch-all}" },
                Transforms = new[]
                {
                    new Dictionary<string, string>
                    {
                        { "PathRemovePrefix", "/api/service2" }
                    },
                    new Dictionary<string, string>
                    {
                        { "PathPrefix", "/api" }
                    }
                }
            }
        };

        var clusters = new[]
        {
            new ClusterConfig
            {
                ClusterId = "service1-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination1", new DestinationConfig { Address = "http://localhost:8081" } }
                }
            },
            new ClusterConfig
            {
                ClusterId = "service2-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination2", new DestinationConfig { Address = "http://localhost:8082" } }
                }
            }
        };

        builder.Services.AddReverseProxy().LoadFromMemory(routes, clusters);
        builder.Services.AddControllers();
        builder.Services.AddHttpClient();
        builder.Services.AddProblemDetails();

        builder.Services.AddOpenApi("v1");

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(p => p
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        var app = builder.Build();
        app.UseCors();
        app.UseExceptionHandler();

        app.MapGet("test", () =>
        {
            return Results.Ok();
        });

        // [KOD GENEROWANIA /openapi/v1.json ZOSTAJE BEZ ZMIAN - JEST POPRAWNY]
        app.MapGet("/openapi/v1.json", async (IHttpClientFactory factory) =>
        {
            var client = factory.CreateClient();

            var json1 = await client.GetStringAsync("http://localhost:8081/openapi/v1.json");
            var json2 = await client.GetStringAsync("http://localhost:8082/openapi/v1.json");

            var doc1 = System.Text.Json.Nodes.JsonNode.Parse(json1)!;
            var doc2 = System.Text.Json.Nodes.JsonNode.Parse(json2)!;

            string BuildGatewayPath(string servicePrefix, string originalPath)
            {
                var cleanPath = originalPath.StartsWith("/api")
                    ? originalPath.Substring(4)
                    : originalPath;

                return $"/api/{servicePrefix}/{cleanPath.TrimStart('/')}";
            }

            var paths1 = doc1["paths"]?.AsObject();
            if (paths1 != null)
            {
                var clonedPaths1 = new System.Text.Json.Nodes.JsonObject();
                foreach (var property in paths1.ToList())
                {
                    paths1.Remove(property.Key);
                    var gatewayPath = BuildGatewayPath("service1", property.Key);
                    clonedPaths1[gatewayPath] = property.Value;
                }
                doc1["paths"] = clonedPaths1;
            }

            var paths2 = doc2["paths"]?.AsObject();
            var mainPaths = doc1["paths"]?.AsObject();

            if (paths2 != null && mainPaths != null)
            {
                foreach (var property in paths2.ToList())
                {
                    paths2.Remove(property.Key);
                    var gatewayPath = BuildGatewayPath("service2", property.Key);
                    mainPaths[gatewayPath] = property.Value;
                }
            }

            doc1["servers"] = new System.Text.Json.Nodes.JsonArray(
                new System.Text.Json.Nodes.JsonObject { ["url"] = "http://localhost:5092" }
            );

            return Results.Content(doc1.ToJsonString(), "application/json");
        });

        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.WithTitle("Główny API Gateway")
                   .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // KRYTYCZNA POPRAWKA: Mapujemy middleware YARP, aby zaczął przetwarzać nieobsłużone ścieżki /api/...
        app.MapReverseProxy();

        app.Run();
    }
}
using Diploma.API.Configurators;
using Microsoft.Extensions.Caching.Memory;
using Scalar.AspNetCore;
using System.Net.Mime;

namespace Diploma.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configurator = new OpenApiDocumentConfigurator()
            .Add("radon", new Uri("http://localhost:8081"))
            .Add("regon", new Uri("http://localhost:8082"));

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient();
        builder.Services.AddMemoryCache();
        builder.Services.AddProblemDetails();

        builder.Services.AddReverseProxy().LoadFromMemory(configurator.RouteConfigs, configurator.ClusterConfigs);
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
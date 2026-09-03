using Diploma.API.Configurators;
using Diploma.API.Controllers.Services;
using Diploma.API.ExceptionHandlers;
using Diploma.API.Extensions;
using Diploma.Application;
using Diploma.Domain;
using Diploma.Infrastructure;
using Diploma.Infrastructure.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Net.Mime;

namespace Diploma.API;

public class Program
{
    private const string RADON_CONFIG = "Microservices:Radon";
    private const string REGON_CONFIG = "Microservices:Regon";


    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        var radon = configuration[RADON_CONFIG];
        var regon = configuration[REGON_CONFIG];

        Console.WriteLine($"RADON: {radon}");
        Console.WriteLine($"REGON: {regon}");

        ArgumentNullException.ThrowIfNullOrEmpty(radon);
        ArgumentNullException.ThrowIfNullOrEmpty(regon);

        var configurator = new GatewayConfigurator()
            .Add(new Uri(radon), "radon", GatewayConfigurator.Type.GraphQl | GatewayConfigurator.Type.Rest)
            .Add(new Uri(regon), "regon", GatewayConfigurator.Type.GraphQl | GatewayConfigurator.Type.Rest);


        builder.Services.AddLogging();
        builder.Services.AddHttpClient();
        builder.Services.AddMemoryCache();
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


        builder.Services.AddInfrastructureConfiguration(builder.Configuration);
        builder.Services.AddApplicationConfiguration();
        builder.Services.AddDomainConfiguration();
        builder.Services.AddJwtAuthorization(builder);

        builder.Services.AddTransient<IPersonsService, PersonsService>();


        builder.Services.AddReverseProxy().LoadFromMemory(
            configurator.RouteConfigs.ToList(),
            configurator.ClusterConfigs.ToList());

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


        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Components ??= new();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                return Task.CompletedTask;
            });
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler();
        }

        app.UseCors();

        foreach (var prefix in configurator.GraphQlPathPrefixes)
        {
            app.MapNitroApp(prefix, relativeRequestPath: prefix);
        }

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


        app.MapGet("/openapi/gateway.json", async (
            IOptions<BackendHostConfiguration> options,
            IMemoryCache cache,
            HttpContext context) =>
        {
            var backendUri = options.Value.Uri;
            string cacheKey = $"openapi-gateway-{backendUri.ToLowerInvariant()}";

            string jsonResponse = cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                entry.Priority = CacheItemPriority.High;

                var document = new OpenApiDocumentConfigurator(configurator.OpenApiConfigurations)
                    .Build(new Uri(backendUri));

                return document.ToJsonString();
            }) ?? throw new InvalidOperationException($"{nameof(IMemoryCache)} not returns json.");

            return Results.Content(jsonResponse, MediaTypeNames.Application.Json);
        }).ExcludeFromDescription();


        app.Run();
    }
}
using RADON.API.OpenApi;
using RADON.Application;
using RADON.Infrastructure;
using Scalar.AspNetCore;

namespace RADON.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationConfiguration();
        builder.Services.AddInfrastructureConfiguration(builder.Configuration);


        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();
        builder.Services.AddOpenApi(options =>
        {
            options.AddOperationTransformer<QueryParametersOpenApiOperationTransformer>();
            options.AddOperationTransformer<EndpointsOpenApiOperationTransformer>();

            options.AddSchemaTransformer<ResponseTypeOpenApiSchemaTransformer>();
            options.AddSchemaTransformer<EnumSchemaTransformer>();

        });

        var app = builder.Build();
        app.UseExceptionHandler();

        app.MapOpenApi();
        app.MapScalarApiReference();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
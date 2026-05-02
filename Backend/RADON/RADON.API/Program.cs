using RADON.API.OpenApi;
using Scalar.AspNetCore;

namespace RADON.API;

public class Program
{
    public static string JsonPropertyName(string name) => char.ToLowerInvariant(name[0]) + name.Substring(1);
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IRadonService, RadonService>();

        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();
        builder.Services.AddOpenApi(options =>
        {
            options.AddOperationTransformer<QueryParametersOpenApiOperation>();

            options.AddSchemaTransformer<ResponseTypeOpenApiSchemaTransformer>();
            options.AddSchemaTransformer<DictionaryResourceEnumSchemaTransformer>();
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

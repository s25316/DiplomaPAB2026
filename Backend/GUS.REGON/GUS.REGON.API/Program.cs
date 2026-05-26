using AppAny.HotChocolate.FluentValidation;
using Base.Models.ValueObjects.Regony;
using GUS.REGON.API.ExceptionHandlers;
using GUS.REGON.API.GraphQL;
using GUS.REGON.API.OpenApi;
using GUS.REGON.Application;
using GUS.REGON.Infrastructure;
using HotChocolate.Types;
using Scalar.AspNetCore;

namespace GUS.REGON.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLogging();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

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

        builder.Services
            .AddGraphQLServer()
            .AddFluentValidation()
            .AddQueryType(d => d.Name(OperationTypeNames.Query))
            .AddTypeExtension<InstitutionsQuery>()
            .AddTypeExtension<ServerQuery>()
            .BindRuntimeType<Regon, RegonScalar>()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
            .DisableIntrospection(false);

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

        app.MapGraphQL();
        app.MapOpenApi();
        app.MapScalarApiReference();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}

using AppAny.HotChocolate.FluentValidation;
using Base.Models.ValueObjects.Krsy;
using Base.Models.ValueObjects.Nipy;
using Base.Models.ValueObjects.Regony;
using FluentValidation;
using HotChocolate.Types;
using RADON.API.GraphQL;
using RADON.API.GraphQL.TypeInterceptors;
using RADON.API.OpenApi;
using RADON.API.Validators;
using RADON.Application;
using RADON.Infrastructure;
using Scalar.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace RADON.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationConfiguration();
        builder.Services.AddInfrastructureConfiguration(builder.Configuration);

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<PaginationValidator>();

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
            .AddTypeExtension<CoursesQuery>()
            .AddTypeExtension<InstitutionsQuery>()
            .AddTypeExtension<SharedQuery>()
            .TryAddTypeInterceptor<DisplayFieldsInterceptor>()
            .TryAddTypeInterceptor<DisplayQueryInterceptor>()
            .BindRuntimeType<Regon, RegonScalar>()
            .BindRuntimeType<Nip, NipScalar>()
            .BindRuntimeType<Krs, KrsScalar>();

        var app = builder.Build();
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
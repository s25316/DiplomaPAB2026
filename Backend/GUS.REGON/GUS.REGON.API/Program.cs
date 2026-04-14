using AppAny.HotChocolate.FluentValidation;
using Base.Models.ValueObjects.Regony;
using GUS.REGON.API.GraphQL;
using GUS.REGON.Application;
using GUS.REGON.Infrastructure;
using Scalar.AspNetCore;

namespace GUS.REGON.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationConfiguration();
        builder.Services.AddInfrastructureConfiguration(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();
        builder.Services.AddOpenApi();

        builder.Services
            .AddGraphQLServer()
            .AddFluentValidation()
            .AddQueryType<Query>()
            .BindRuntimeType<Regon, RegonScalar>();

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

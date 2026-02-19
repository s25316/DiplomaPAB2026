using AppAny.HotChocolate.FluentValidation;
using Base.Models.Validators;
using FluentValidation;
using GUS.TERYT.API.BackgroundServices;
using GUS.TERYT.API.GraphQL;
using GUS.TERYT.Application;
using GUS.TERYT.Infrastructure;
using GUS.TERYT.Models.Requests.Validators;
using GUS.TERYT.Models.Requests.ValueObjects.Gminy;
using GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;
using GUS.TERYT.Models.Requests.ValueObjects.Ulicy;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;
using HotChocolate;
using Scalar.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace GUS.TERYT.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddValidatorsFromAssemblyContaining<PaginationValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<WojewodztwoParametersValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<PowiatParametersValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<GminaParametersValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<MiejscowoscParametersValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UlicaParametersValidator>();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddApplicationConfiguration();
        builder.Services.AddInfrastructureConfiguration(builder.Configuration);
        builder.Services.AddHostedService<TerytSeedBackgroundService>();

        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();
        builder.Services.AddOpenApi();

        builder.Services
            .AddGraphQLServer()
            .AddFluentValidation()
            .AddQueryType<Query>()
            .AddTypeExtension<Dictionaries>()
            .BindRuntimeType<WojewodztwoId, WojewodztwoIdScalar>()
            .BindRuntimeType<PowiatId, PowiatIdScalar>()
            .BindRuntimeType<GminaId, GminaIdScalar>()
            .BindRuntimeType<GminaTypeId, GminaTypeIdScalar>()
            .BindRuntimeType<MiejscowoscId, MiejscowoscIdScalar>()
            .BindRuntimeType<MiejscowoscTypeId, MiejscowoscTypeIdScalar>()
            .BindRuntimeType<UlicaId, UlicaIdScalar>();

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
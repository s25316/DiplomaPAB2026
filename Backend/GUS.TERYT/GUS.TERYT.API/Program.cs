using AppAny.HotChocolate.FluentValidation;
using Base.Models.Validators;
using FluentValidation;
using GUS.TERYT.API.BackgroundServices;
using GUS.TERYT.Application;
using GUS.TERYT.Infrastructure;
using GUS.TERYT.Models.Requests.Parameters;
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
            .BindRuntimeType<WojewodztwoId, WojewodztwoIdScalar>();


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

public class Query
{
    [GraphQLName("getParameters")]
    public WojewodztwoParameters GetParameters([UseFluentValidation] WojewodztwoParameters parameters) => parameters;
}


public class WojewodztwoParametersValidator : AbstractValidator<WojewodztwoParameters>
{
    public WojewodztwoParametersValidator(PaginationValidator p)
    {
        RuleFor(x => x.Pagination)
            .NotNull()
            .SetValidator(p);
    }
}

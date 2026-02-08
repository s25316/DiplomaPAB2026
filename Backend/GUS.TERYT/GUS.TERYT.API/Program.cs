using GUS.TERYT.API.BackgroundServices;
using GUS.TERYT.Application;
using GUS.TERYT.Infrastructure;
using Scalar.AspNetCore;

namespace GUS.TERYT.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationConfiguration();
        builder.Services.AddInfrastructureConfiguration(builder.Configuration);
        builder.Services.AddHostedService<TerytSeedBackgroundService>();

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.MapOpenApi();
        app.MapScalarApiReference();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        try
        {
            app.MapControllers();
        }
        catch (System.Reflection.ReflectionTypeLoadException ex)
        {
            // To wypisze w oknie "Output" (Dêbu) dok³adn¹ nazwê brakuj¹cej biblioteki
            foreach (var loaderException in ex.LoaderExceptions)
            {
                System.Diagnostics.Debug.WriteLine(loaderException?.Message);
            }
            throw;
        }

        app.Run();
    }
}

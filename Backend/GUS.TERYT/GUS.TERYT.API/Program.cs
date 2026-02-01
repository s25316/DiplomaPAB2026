using GUS.TERYT.API.BackgroundServices;
using GUS.TERYT.Application;
using GUS.TERYT.Infrastructure;

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
        builder.Services.AddSwaggerGen();

        // For Tests
        builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // To ustawienie sprawia, ¿e serializator ignoruje fakt, 
        // ¿e pola s¹ prywatne i wyci¹ga z nich dane
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            IgnoreSerializableAttribute = true
        };
    });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

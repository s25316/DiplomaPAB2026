using GUS.REGON.Extensions;
using Scalar.AspNetCore;

namespace GUS.REGON.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRegonService("key", false);

        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();
        builder.Services.AddOpenApi();


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

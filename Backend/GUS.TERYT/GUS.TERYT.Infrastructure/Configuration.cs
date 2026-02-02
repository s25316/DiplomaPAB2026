using Azure.Storage.Blobs;
using GUS.TERYT.Database;
using GUS.TERYT.Database.MsSql;
using GUS.TERYT.Infrastructure.Configurations;
using GUS.TERYT.Infrastructure.Interfaces;
using GUS.TERYT.Infrastructure.InterfacesImplementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GUS.TERYT.Infrastructure;

public static class Configuration
{
    private const string SECTION_AZURE = "Azure";
    private const string SECTION_DATABASE = "Database";
    private const string SECTION_TERYT_FILES = "TerytFiles";


    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureConfiguration>(configuration.GetSection(SECTION_AZURE));
        services.Configure<DatabaseConfiguration>(configuration.GetSection(SECTION_DATABASE));
        services.Configure<TerytFilesConfiguration>(configuration.GetSection(SECTION_TERYT_FILES));


        services.AddDbContext<TerytDbContext, TerytMsSqlDbContext>((p, o) =>
        {
            var configuration = p.GetRequiredService<IOptions<DatabaseConfiguration>>().Value;
            o.UseSqlServer(configuration.ConnectionString);
        });
        services.AddSingleton<BlobServiceClient>(p =>
        {
            var options = p.GetRequiredService<IOptions<AzureConfiguration>>();
            var configuration = options.Value;
            return new BlobServiceClient(configuration.BlobConnectionString);
        });
        services.AddTransient<IBlobService, BlobService>();

        return services;
    }
}

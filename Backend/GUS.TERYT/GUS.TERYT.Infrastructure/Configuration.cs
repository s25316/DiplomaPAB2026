using Azure.Storage.Blobs;
using GUS.TERYT.Database;
using GUS.TERYT.Database.MsSql;
using GUS.TERYT.Infrastructure.Configurations;
using GUS.TERYT.Infrastructure.Interfaces;
using GUS.TERYT.Infrastructure.InterfacesImplementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GUS.TERYT.Infrastructure;

public static class Configuration
{
    private const string SECTION_AZURE = "Azure";
    private const string SECTION_TERYT_FILES = "TerytFiles";


    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureConfiguration>(configuration.GetSection(SECTION_AZURE));
        services.Configure<TerytFilesConfiguration>(configuration.GetSection(SECTION_TERYT_FILES));

        services.AddSingleton<BlobServiceClient>(p =>
        {
            var options = p.GetRequiredService<IOptions<AzureConfiguration>>();
            var configuration = options.Value;
            return new BlobServiceClient(configuration.BlobConnectionString);
        });

        services.AddDbContext<TerytDbContext, TerytMsSqlDbContext>();
        services.AddTransient<IBlobService, BlobService>();

        return services;
    }
}

// Ignore Spelling: Regon
using GUS.REGON.Application.Interfaces;
using GUS.REGON.Database;
using GUS.REGON.Database.MsSql;
using GUS.REGON.Extensions;
using GUS.REGON.Infrastructure.Configurations;
using GUS.REGON.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GUS.REGON.Infrastructure;

public static class Configuration
{
    private const string SECTION_REGON = "Regon";
    private const string SECTION_DATABASE = "Database";

    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RegonConfiguration>(configuration.GetSection(SECTION_REGON));
        services.Configure<DatabaseConfiguration>(configuration.GetSection(SECTION_DATABASE));

        services.AddRegonService(p =>
        {
            var key = p.GetRequiredService<IOptions<RegonConfiguration>>().Value.Key;
            return new RegonService(key, true);
        });
        services.AddDbContext<RegonDbContext, RegonMsSqlDbContext>((p, c) =>
        {
            var connectionString = p.GetRequiredService<IOptions<DatabaseConfiguration>>().Value.ConnectionString;
            c.UseSqlServer(connectionString);
        });


        services.AddTransient<IReportRepository, ReportRepository>();

        return services;
    }
}
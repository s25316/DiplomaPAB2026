using Diploma.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Diploma.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthorization(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        _ = services.AddAuthentication(opt =>
        {
            //Creating Default Scheme [We can use in different Controllers Different Scheme]
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var jwtConfiguration = serviceProvider.GetRequiredService<IOptions<JwtConfiguration>>().Value;

            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true, // ClockSkew
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,

                ClockSkew = TimeSpan.Zero, // Allowed Expired Tokens,ex. TimeSpan.FromMinutes(1)
                ValidIssuer = jwtConfiguration.Issuer, // Who Gives Token
                ValidAudience = jwtConfiguration.Audience, //  Who Given Token
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret)),
            };

            // Returns info about Expired Token
            opt.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Append("Token-expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });
        return services;
    }
}
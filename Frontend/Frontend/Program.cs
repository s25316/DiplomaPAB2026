using Frontend.Authentication;
using Frontend.Components;
using Frontend.Configurations;
using Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

namespace Frontend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSingleton<IOptions<BackendHostConfiguration>>(p => Options.Create(new BackendHostConfiguration
        {
            Uri = "http://localhost:5092/",
        }));

        builder.Services.AddHttpClient();
        builder.Services.AddTransient<IBackendHttpClientFactory, BackendHttpClientFactory>();

        builder.Services.AddScoped<ILocalStorage, LocalStorage>();
        builder.Services.AddScoped<IUserSessionService, UserSessionService>();
        builder.Services.AddScoped<IRegonService, RegonService>();
        builder.Services.AddScoped<UserSessionService>();
        builder.Services.AddScoped<CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<CustomAuthenticationStateProvider>());

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "CustomJwt";
            options.DefaultChallengeScheme = "CustomJwt";
        })
        .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, NoRedirectAuthenticationHandler>("CustomJwt", options => { });

        builder.Services.AddAuthorization();

        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();


        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
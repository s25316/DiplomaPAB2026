// Ignore Spelling: Regon
// Ignore Spelling: Zaloguj, Wyloguj
// Ignore Spelling: Komunikat, Uslugi, Sesji, Danych
using Base.Models.ValueObjects.Regony;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Interfaces;
using GUS.REGON.Mapping;
using GUS.REGON.Models;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.Models.Results;
using GUS.REGON.Operations.Workflows;
using GUS.REGON.Services.SessionManagers;
using GUS.REGON.Strategies;
using GUS.REGON.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GUS.REGON;

public class RegonService : IDisposable, IAsyncDisposable
{
    private readonly ILogger<RegonService> logger;
    private readonly IServiceProvider provider;
    private bool disposed = false;
    private readonly SemaphoreSlim semaphore = new(1, 1);

    public RegonService(string key, bool isProduction = true)
    {
        ServiceCollection services = new();
        Endpoint endpoint = isProduction
                ? Endpoint.Production
                : Endpoint.Testing;

        services.AddLogging(opt =>
        {
            opt.AddConsole();
            opt.SetMinimumLevel(LogLevel.Trace);
        });

        services.AddSingleton<Key>((Key)key);
        services.AddSingleton<Endpoint>(endpoint);

        // Add Requests
        services.AddSingleton<Request.Zaloguj>();
        services.AddSingleton<Request.Wyloguj>();
        services.AddSingleton<Request.GetValue>();
        services.AddSingleton<Request.DaneSzukaj>();
        services.AddSingleton<Request.DanePobierzPelnyRaport>();

        // Add Workflows Operations
        services.AddSingleton<KomunikatUslugiOperation>();
        services.AddSingleton<StatusUslugiOperation>();
        services.AddSingleton<KomunikatTrescOperation>();
        services.AddSingleton<KomunikatKodOperation>();
        services.AddSingleton<StatusSesjiOperation>();
        services.AddSingleton<StanDanychOperation>();

        services.AddSingleton<ZalogujOperation>();
        services.AddSingleton<WylogujOperation>();

        services.AddSingleton<DaneSzukajOperation>();
        services.AddSingleton<RaportJednostkiOperation>();

        // Add Strategies
        services.AddSingleton<RaportJednostkiStrategy>();
        services.AddSingleton<RaportPkdStarategy>();


        services.AddHttpClient(HttpClientNames.BASE, client =>
        {
            client.BaseAddress = endpoint.Value;
        });

        services.AddSingleton<ISessionManager>(new SessionManager(ZalogujAsync));

        provider = services.BuildServiceProvider();

        var factory = provider.GetRequiredService<ILoggerFactory>();
        logger = factory.CreateLogger<RegonService>();

        Task.Run(async () => await provider.GetRequiredService<ISessionManager>().UpdateSessionAsync())
            .GetAwaiter()
            .GetResult();
    }

    #region Getting Data Operations
    public async Task<RegonResult<IEnumerable<DaneSzukaj>>> GetDaneSzukajAsync(
        Regon regon,
        CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        try
        {
            await semaphore.WaitAsync(cancellationToken);

            var daneSzukajOperation = provider.GetRequiredService<DaneSzukajOperation>();
            var request = provider.GetRequiredService<Request.DaneSzukaj>();

            var requestEnvelope = request.Generate(regon);
            var result = await daneSzukajOperation.ExecuteAsync(requestEnvelope, cancellationToken);

            if (result.IsFailure)
            {
                return RegonResult.Failed<IEnumerable<DaneSzukaj>>(result.KomunikatKod, result.StatusUslugi);
            }

            var mappedResults = result.Value.Select(i => i.MapToAdapted());
            return RegonResult.Success(mappedResults);
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task<RegonResult<RaportJednostki>> GetRaportJednostkiAsync(
        Regon regon,
        TypJednostki typ,
        int? silosId,
        CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        try
        {
            await semaphore.WaitAsync(cancellationToken);

            var paportJednostkiOperation = provider.GetRequiredService<RaportJednostkiOperation>();
            var raportJednostkiStrategy = provider.GetRequiredService<RaportJednostkiStrategy>();
            var request = provider.GetRequiredService<Request.DanePobierzPelnyRaport>();

            var reportResult = raportJednostkiStrategy.GetReport(typ, silosId);
            if (reportResult.IsFailure)
            {
                return RegonResult.Failed<RaportJednostki>(KomunikatKod.NieZnalezionoPodmiotów);
            }

            var requestEnvelope = request.Generate(regon, reportResult.Value.Value);
            var result = await paportJednostkiOperation.ExecuteAsync(requestEnvelope, cancellationToken);

            if (result.IsFailure)
            {
                return RegonResult.Failed<RaportJednostki>(result.KomunikatKod, result.StatusUslugi);
            }

            var mappedResult = result.Value.First().MapToAdapted();
            return RegonResult.Success(mappedResult);
        }
        finally
        {
            semaphore.Release();
        }
    }
    #endregion


    #region Authorization Operations
    private async Task<RegonBaseResult<string>> ZalogujAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        var zalogujOperation = provider.GetRequiredService<ZalogujOperation>();
        return await zalogujOperation.ExecuteAsync(cancellationToken);
    }

    private async Task<RegonBaseResult<bool>> WylogujAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        var wylogujOperation = provider.GetRequiredService<WylogujOperation>();
        return await wylogujOperation.ExecuteAsync(cancellationToken);
    }
    #endregion


    #region GetValue Operations
    public async Task<string> GetKomunikatUslugiAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        return await provider.GetRequiredService<KomunikatUslugiOperation>().ExecuteAsync(cancellationToken);
    }

    public async Task<StatusUslugi> GetStatusUslugiAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        return await provider.GetRequiredService<StatusUslugiOperation>().ExecuteAsync(cancellationToken);
    }

    public async Task<RegonBaseResult<StatusSesji>> GetStatusSesjiAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        return await provider.GetRequiredService<StatusSesjiOperation>().ExecuteAsync(cancellationToken);
    }

    public async Task<RegonBaseResult<DateOnly>> GetStanDanychAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        return await provider.GetRequiredService<StanDanychOperation>().ExecuteAsync(cancellationToken);
    }
    #endregion


    #region Disposable
    ~RegonService() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        if (disposed) return;
        var loggingOutResult = Task.Run(async () => await WylogujAsync().ConfigureAwait(false))
            .GetAwaiter()
            .GetResult();
        logger.LogInformation("Logged out (Async): {Value} at {Time}", loggingOutResult.Value, DateTimeOffset.Now);
        disposed = true;
    }
    #endregion
}
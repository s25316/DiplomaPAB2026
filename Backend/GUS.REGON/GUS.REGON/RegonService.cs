// Ignore Spelling: Regon
// Ignore Spelling: Zaloguj, Wyloguj
// Ignore Spelling: Komunikat, Uslugi, Sesji, Danych
using Base.Models.ValueObjects.Regony;
using Base.Pipelines;
using Base.Pipelines.Models;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Exceptions;
using GUS.REGON.Interfaces;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.PipelineOperations;
using GUS.REGON.PipelineOperations.Base;
using GUS.REGON.PipelineOperations.Base.ElementsTo;
using GUS.REGON.Services.SessionManagers;
using GUS.REGON.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RegonResponse = GUS.REGON.Models.Responses.Response;

namespace GUS.REGON;

public class RegonService : IDisposable, IAsyncDisposable
{
    private readonly ILogger<RegonService> logger;
    private readonly IServiceProvider provider;
    private bool disposed = false;


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

        // Add Operations
        services.AddSingleton<ZalogujOperation>();
        services.AddSingleton<WylogujOperation>();

        services.AddSingleton<KomunikatUslugiOperation>();
        services.AddSingleton<StatusUslugiOperation>();
        services.AddSingleton<KomunikatTrescOperation>();
        services.AddSingleton<KomunikatKodOperation>();
        services.AddSingleton<StatusSesjiOperation>();
        services.AddSingleton<StanDanychOperation>();

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


    public async Task<IEnumerable<RegonResponse.DaneSzukaj>> GetAsync(Regon regon, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        using var client = provider
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient(HttpClientNames.BASE);

        var request = provider.GetRequiredService<Request.DaneSzukaj>();
        var sessionManager = provider.GetRequiredService<ISessionManager>();
        var requestEnvelope = request.Generate(regon);

        var result = await PipelineBuilder
            .Create(new MakeRequestOperation(client, sessionManager))
            .Add(new ResponseToEnvelopeOperation())
            .Add(new EnvelopeToDocumentOperation())
            .Add(new DocumentToElementsOperation(ElementDefinition.Dane))
            .Add(new ElementsToClassesOperation<RegonResponse.DaneSzukaj>())
            .ExecuteAsync(requestEnvelope, cancellationToken);

        if (result.IsSuccess) return result.Value;
        else return [];
    }

    private async Task<OperationResult<string>> ZalogujAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        var zalogujOperation = provider.GetRequiredService<ZalogujOperation>();
        var statusUslugi = provider.GetRequiredService<StatusUslugiOperation>();

        var zalogujResult = await zalogujOperation.ExecuteAsync(cancellationToken);
        if (zalogujResult.IsSuccess) return zalogujResult;
        if (zalogujResult.HasException) throw zalogujResult.Exception;

        var statusUslugiResult = await statusUslugi.ExecuteAsync(cancellationToken);
        if (statusUslugiResult.IsSuccess) throw new RegonException.InvalidKey(Messages.KeyErrorMessageInvalid);
        if (statusUslugiResult.HasException) throw statusUslugiResult.Exception;
        throw new NotImplementedException();
    }

    private async Task<OperationResult<bool>> WylogujAsync(CancellationToken cancellationToken = default)
    {
        var wylogujOperation = provider.GetRequiredService<WylogujOperation>();
        var wylogujResult = await wylogujOperation.ExecuteAsync(cancellationToken);
        if (wylogujResult.IsSuccess) return wylogujResult;
        if (wylogujResult.HasException) throw wylogujResult.Exception;
        return OperationResult.Success(true);
    }


    #region GetValueAsync
    public async Task<OperationResult<string>> GetKomunikatUslugiAsync(CancellationToken cancellationToken = default)
        => await provider.GetRequiredService<KomunikatUslugiOperation>().ExecuteAsync(cancellationToken);

    public async Task<OperationResult<StatusUslugi>> GetStatusUslugiAsync(CancellationToken cancellationToken = default)
        => await provider.GetRequiredService<StatusUslugiOperation>().ExecuteAsync(cancellationToken);

    public async Task<OperationResult<StatusSesji>> GetStatusSesjiAsync(CancellationToken cancellationToken = default)
        => await provider.GetRequiredService<StatusSesjiOperation>().ExecuteAsync(cancellationToken);

    public async Task<OperationResult<DateOnly>> GetStanDanychAsync(CancellationToken cancellationToken = default)
        => await provider.GetRequiredService<StanDanychOperation>().ExecuteAsync(cancellationToken);

    private async Task<OperationResult<KomunikatKod>> GetKomunikatKodAsync(CancellationToken cancellationToken = default)
        => await provider.GetRequiredService<KomunikatKodOperation>().ExecuteAsync(cancellationToken);

    private async Task<OperationResult<string>> GetKomunikatTrescAsync(CancellationToken cancellationToken = default)
        => await provider.GetRequiredService<KomunikatTrescOperation>().ExecuteAsync(cancellationToken);
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
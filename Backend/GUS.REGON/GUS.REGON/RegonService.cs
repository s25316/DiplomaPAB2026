// Ignore Spelling: Regon
// Ignore Spelling: Zaloguj, Wyloguj
// Ignore Spelling: Komunikat, Uslugi, Sesji, Danych
using Base.Models.ValueObjects.Regony;
using Base.Pipelines;
using Base.Pipelines.Interfaces;
using GUS.REGON.Configurations;
using GUS.REGON.Envelopes.Requests;
using GUS.REGON.Interfaces;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.PipelineOperations;
using GUS.REGON.PipelineOperations.ElementsTo;
using GUS.REGON.PipelineOperations.FirstValueTo;
using GUS.REGON.Services.SessionManagers;
using GUS.REGON.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using RegonResponse = GUS.REGON.Models.Responses.Response;

namespace GUS.REGON;

public class RegonService : IDisposable, IAsyncDisposable
{
    private readonly IServiceProvider provider;
    private bool disposed = false;


    public RegonService(string key, bool isProduction = true)
    {
        ServiceCollection services = new();
        Endpoint endpoint = isProduction
                ? Endpoint.Production
                : Endpoint.Testing;

        services.AddLogging();

        services.AddSingleton<Key>((Key)key);
        services.AddSingleton<Endpoint>(endpoint);

        // Add Requests
        services.AddSingleton<Request.Zaloguj>();
        services.AddSingleton<Request.Wyloguj>();
        services.AddSingleton<Request.GetValue>();
        services.AddSingleton<Request.DaneSzukaj>();
        services.AddSingleton<Request.DanePobierzPelnyRaport>();

        services.AddHttpClient(HttpClientNames.BASE, client =>
        {
            client.BaseAddress = endpoint.Value;
        });

        services.AddSingleton<ISessionManager>(new SessionManager(ZalogujAsync));

        provider = services.BuildServiceProvider();

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

        if (result.HasException) throw result.Exception;
        if (result.IsSuccess) return result.Value;
        else return [];
    }

    private async Task<string> ZalogujAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        using var client = provider
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient(HttpClientNames.BASE);

        var key = provider.GetRequiredService<Key>();
        var request = provider.GetRequiredService<Request.Zaloguj>();
        var requestEnvelope = request.Generate(key);

        var result = await PipelineBuilder
            .Create(new MakeRequestOperation(client))
            .Add(new ResponseToEnvelopeOperation())
            .Add(new EnvelopeToDocumentOperation())
            .Add(new DocumentToElementsOperation(ElementDefinition.Zaloguj))
            .Add(new ElementsToFirstValueOperation())
            .ExecuteAsync(requestEnvelope, cancellationToken);

        if (result.HasException) throw result.Exception;
        if (result.IsSuccess) return result.Value;
        else throw new Exception("????");
    }

    private async Task<OperationResult<bool>> WylogujAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        using var client = provider
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient(HttpClientNames.BASE);

        var sessionManager = provider.GetRequiredService<ISessionManager>();
        var session = sessionManager.Session;

        if (!session.IsExpired)
        {
            return OperationResult.Success(true);
        }

        var request = provider.GetRequiredService<Request.Wyloguj>();
        var requestEnvelope = request.Generate(session.SessionId);

        var result = await PipelineBuilder
            .Create(new MakeRequestOperation(client, sessionManager))
            .Add(new ResponseToEnvelopeOperation())
            .Add(new EnvelopeToDocumentOperation())
            .Add(new DocumentToElementsOperation(ElementDefinition.Wyloguj))
            .Add(new ElementsToFirstValueOperation())
            .Add(new FirstValueToBoolOperation())
            .ExecuteAsync(requestEnvelope, cancellationToken);

        if (result.HasException) throw result.Exception;
        return result;
    }


    #region GetValueAsync
    public async Task<OperationResult<string>> GetKomunikatUslugiAsync(CancellationToken cancellationToken = default) => await GetValueAsync(
        false,
        getValue => getValue.KomunikatUslugi(),
        new FirstValueToFirstValueOperation(),
        cancellationToken);

    public async Task<OperationResult<StatusUslugi>> GetStatusUslugiAsync(CancellationToken cancellationToken = default) => await GetValueAsync(
        false,
        getValue => getValue.StatusUslugi(),
        new FirstValueToEnumOperation<StatusUslugi>(),
        cancellationToken);

    public async Task<OperationResult<StatusSesji>> GetStatusSesjiAsync(CancellationToken cancellationToken = default) => await GetValueAsync(
        false,
        getValue => getValue.StatusSesji(),
        new FirstValueToEnumOperation<StatusSesji>(),
        cancellationToken);

    public async Task<OperationResult<DateOnly>> GetStanDanychAsync(CancellationToken cancellationToken = default) => await GetValueAsync(
        true,
        getValue => getValue.StanDanych(),
        new FirstValueToDateOnlyOperation(),
        cancellationToken);

    private async Task<OperationResult<KomunikatKod>> GetKomunikatKodAsync(CancellationToken cancellationToken = default) => await GetValueAsync(
        true,
        getValue => getValue.KomunikatKod(),
        new FirstValueToEnumOperation<KomunikatKod>(),
        cancellationToken);

    private async Task<OperationResult<string>> GetKomunikatTrescAsync(CancellationToken cancellationToken = default) => await GetValueAsync(
        true,
        getValue => getValue.KomunikatTresc(),
        new FirstValueToFirstValueOperation(),
        cancellationToken);

    private async Task<OperationResult<T>> GetValueAsync<T>(
        bool isAuthorize,
        Func<Request.GetValue, string> getValueFunc,
        ISyncOperation<string, T> operation,
        CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        using var client = provider
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient(HttpClientNames.BASE);

        var sessionManager = isAuthorize
            ? provider.GetRequiredService<ISessionManager>()
            : null;

        var request = provider.GetRequiredService<Request.GetValue>();
        var requestEnvelope = getValueFunc(request);

        var result = await PipelineBuilder
            .Create(new MakeRequestOperation(client, sessionManager))
            .Add(new ResponseToEnvelopeOperation())
            .Add(new EnvelopeToDocumentOperation())
            .Add(new DocumentToElementsOperation(ElementDefinition.GetValue))
            .Add(new ElementsToFirstValueOperation())
            .Add(operation)
            .ExecuteAsync(requestEnvelope, cancellationToken);

        if (result.HasException) throw result.Exception;
        return result;
    }
    #endregion


    #region Disposable
    ~RegonService() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;
        var loggingOutResult = Task.Run(async () => await WylogujAsync().ConfigureAwait(false))
            .GetAwaiter()
            .GetResult();
        Console.WriteLine($"Logged out: {loggingOutResult.Value}");
        disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (disposed) return;
        var loggingOutResult = await WylogujAsync().ConfigureAwait(false);
        Console.WriteLine($"Logged out: {loggingOutResult.Value}");
        disposed = true;
        GC.SuppressFinalize(this);
    }
    #endregion
}
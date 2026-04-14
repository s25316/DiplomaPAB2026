using GUS.REGON.Interfaces;
using GUS.REGON.Models.Results;

namespace GUS.REGON.Services.SessionManagers;

internal class SessionManager(Func<CancellationToken, Task<RegonBaseResult<string>>> zalogujAsyncFunc) :
    ISessionManager,
    IDisposable
{
    private static readonly TimeSpan lockingLifeTime = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan sessionLifeTime = TimeSpan.FromMinutes(59);

    private readonly SemaphoreSlim semaphore = new(1, 1);

    private Task? refreshTask = null;
    private DateTimeOffset? sessionUpdated = null;
    private DateTimeOffset? sessionExpired = null;
    private DateTimeOffset? sessionCanUpdated = null;
    private string? sessionId = null;
    private bool disposed = false;

    private bool CanUpdateSession =>
        string.IsNullOrWhiteSpace(sessionId) ||
        sessionExpired is null ||
        sessionUpdated is null ||
        DateTimeOffset.Now >= sessionCanUpdated;

    public SessionInfo Session => PrepareSessionInfo();


    public async Task UpdateSessionAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        Task taskToAwait;
        await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (!CanUpdateSession) return;

            if (refreshTask == null || refreshTask.IsCompleted)
            {
                refreshTask = InvokeUpdateSessionAsync(cancellationToken);
            }

            taskToAwait = refreshTask;
        }
        finally
        {
            semaphore.Release();
        }

        await taskToAwait.ConfigureAwait(false);
    }


    private SessionInfo PrepareSessionInfo()
    {
        if (string.IsNullOrWhiteSpace(sessionId) ||
            sessionExpired is null ||
            sessionUpdated is null ||
            DateTimeOffset.Now >= sessionExpired)
        {
            return new SessionInfo(false, sessionId);
        }
        return new SessionInfo(true, sessionId);
    }

    public async Task InvokeUpdateSessionAsync(CancellationToken cancellationToken = default)
    {
        if (!CanUpdateSession)
            return;

        var timeBeforeRequest = DateTimeOffset.Now;

        var result = await zalogujAsyncFunc(cancellationToken).ConfigureAwait(false);
        if (result.IsFailure)
        {
            return;
        }
        sessionId = result.Value;
        sessionUpdated = timeBeforeRequest;
        sessionExpired = sessionUpdated + sessionLifeTime;
        sessionCanUpdated = sessionUpdated + lockingLifeTime;
    }

    ~SessionManager() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;
        if (disposing)
            semaphore.Dispose();
        disposed = true;
    }
}
using System.Diagnostics.CodeAnalysis;

namespace GUS.REGON.Interfaces;

public sealed record SessionInfo
{
    [MemberNotNullWhen(false, nameof(SessionId))]
    public bool IsExpired { get; }
    public string? SessionId { get; }


    public SessionInfo(bool isExpired, string? sessionId)
    {
        IsExpired = isExpired;
        SessionId = sessionId;
    }
}

public interface ISessionManager
{
    SessionInfo Session { get; }
    Task UpdateSessionAsync(CancellationToken cancellationToken = default);
}
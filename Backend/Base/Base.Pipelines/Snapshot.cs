namespace Base.Pipelines;

public sealed record Snapshot(string OperationName, bool IsSuccess, string? ErrorMessage, object? Input, DateTimeOffset TimeStamp);
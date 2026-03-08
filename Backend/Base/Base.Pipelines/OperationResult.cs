using System.Diagnostics.CodeAnalysis;

namespace Base.Pipelines;

public static class OperationResult
{
    public static OperationResult<TItem> Success<TItem>(TItem result)
        => OperationResult<TItem>.Success(result);

    public static OperationResult<TItem> Failed<TItem>(string errorMessage, Exception? exception = null)
        => OperationResult<TItem>.Failed(errorMessage, exception);
}

public sealed class OperationResult<TItem>
{
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(ErrorMessage))]
    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsFailure => !IsSuccess;

    [MemberNotNullWhen(true, nameof(Exception))]
    public bool HasException => Exception is not null;

    public TItem? Value { get; }
    public string? ErrorMessage { get; }
    public Exception? Exception { get; }


    private OperationResult(bool isSuccess, TItem? value, string? errorMessage = null, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
        Exception = exception;
    }


    public static OperationResult<TItem> Success(TItem result)
        => new(true, result);

    public static OperationResult<TItem> Failed(string errorMessage, Exception? exception = null)
        => new(false, default, errorMessage, exception);
}
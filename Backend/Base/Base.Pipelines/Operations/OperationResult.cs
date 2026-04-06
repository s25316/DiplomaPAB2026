using System.Diagnostics.CodeAnalysis;

namespace Base.Pipelines.Operations;

public abstract record OperationError(string Message);
public record OperationResult<TItem>
{
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(Error))]
    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsFailure => !IsSuccess;

    public TItem? Value { get; }
    public OperationError? Error { get; }


    private OperationResult(bool isSuccess, TItem? value, OperationError? error = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }


    public static implicit operator OperationResult<TItem>(TItem value) => Success(value);
    public static implicit operator OperationResult<TItem>(OperationError error) => Failed(error);

    public static OperationResult<TItem> Success(TItem result)
        => new(true, result);

    public static OperationResult<TItem> Failed(OperationError error)
        => new(false, default, error);
}

public static class OperationResult
{
    public static OperationResult<TItem> Success<TItem>(TItem result)
        => OperationResult<TItem>.Success(result);

    public static OperationResult<TItem> Failed<TItem>(OperationError error)
        => OperationResult<TItem>.Failed(error);
}
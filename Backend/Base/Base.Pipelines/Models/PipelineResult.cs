using System.Diagnostics.CodeAnalysis;

namespace Base.Pipelines.Models;

public static class PipelineResult
{
    public static PipelineResult<TItem> Success<TItem>(TItem result)
        => PipelineResult<TItem>.Success(result);

    public static PipelineResult<TItem> Failed<TItem>(string errorMessage)
        => PipelineResult<TItem>.Failed(errorMessage);
}

public sealed record PipelineResult<TItem>
{
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(ErrorMessage))]
    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsFailure => !IsSuccess;

    public TItem? Value { get; }
    public string? ErrorMessage { get; }


    private PipelineResult(bool isSuccess, TItem? value, string? errorMessage = null, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }


    public static implicit operator PipelineResult<TItem>(OperationResult<TItem> operation) => operation.IsSuccess
        ? PipelineResult.Success(operation.Value)
        : PipelineResult.Failed<TItem>(operation.ErrorMessage);

    public static PipelineResult<TItem> Success(TItem result)
        => new(true, result);

    public static PipelineResult<TItem> Failed(string errorMessage)
        => new(false, default, errorMessage);
}
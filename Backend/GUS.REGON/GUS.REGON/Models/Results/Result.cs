using System.Diagnostics.CodeAnalysis;

namespace GUS.REGON.Models.Results;

public static class Result
{
    public static Result<TItem> Success<TItem>(TItem result)
        => Result<TItem>.Success(result);

    public static Result<TItem> Failed<TItem>()
        => Result<TItem>.Failed();
}

public class Result<TItem>
{
    [MemberNotNullWhen(true, nameof(Value))]
    public virtual bool IsSuccess => Value is not null;

    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsFailure => !IsSuccess;


    public TItem? Value { get; }

    protected Result(TItem? value)
    {
        Value = value;
    }


    public static Result<TItem> Success(TItem result)
        => new(result);

    public static Result<TItem> Failed()
        => new(default);
}
using System.Diagnostics.CodeAnalysis;

namespace Diploma.Domain.Base.Results;

public static class OptionalResult
{
    public static OptionalResult<T> NotFound<T>()
    where T : class
        => OptionalResult<T>.NotFound();

    public static OptionalResult<T> Success<T>(T value)
    where T : class
        => OptionalResult<T>.Success(value);
}

public sealed class OptionalResult<T>
    where T : class
{
    [MemberNotNullWhen(true, nameof(Value))]
    public bool HasValue { get; }
    public T? Value { get; }


    private OptionalResult(bool hasValue, T? value = null)
    {
        if (value is null)
        {
            HasValue = false;
            Value = null;
        }

        HasValue = hasValue;
        Value = value;
    }


    public static OptionalResult<T> NotFound() => new(false);
    public static OptionalResult<T> Success(T value) => new(true, value);

    public T GetRequiredValue()
    {
        ArgumentNullException.ThrowIfNull(Value);
        return Value;
    }
}
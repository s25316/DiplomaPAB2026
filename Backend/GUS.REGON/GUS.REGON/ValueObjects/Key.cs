using GUS.REGON.Exceptions;

namespace GUS.REGON.ValueObjects;

internal sealed record Key
{
    public string Value { get; }

    private Key(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new RegonException.InvalidKey(Messages.KeyErrorMessageEmpty);
        Value = value;
    }


    public static implicit operator Key(string value) => new(value);
    public static implicit operator string(Key value) => value.Value;

    public override string ToString() => Value;
}
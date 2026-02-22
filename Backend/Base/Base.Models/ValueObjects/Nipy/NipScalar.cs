using HotChocolate.Language;

namespace Base.Models.ValueObjects.Nipy;

public sealed class NipScalar : ScalarType<Nip, StringValueNode>
{
    public NipScalar() : base(nameof(Nip))
    {
        Description = Nip.GetDescription();
    }

    protected override StringValueNode ParseValue(Nip runtimeValue) => new(runtimeValue.ToString());
    protected override Nip ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return Nip.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        Nip item => new StringValueNode(item.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
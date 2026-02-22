// Ignore Spelling: Krsy, Krs
using HotChocolate.Language;

namespace Base.Models.ValueObjects.Krsy;

public sealed class KrsScalar : ScalarType<Krs, StringValueNode>
{
    public KrsScalar() : base(nameof(Krs))
    {
        Description = Krs.GetDescription();
    }

    protected override StringValueNode ParseValue(Krs runtimeValue) => new(runtimeValue.ToString());
    protected override Krs ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return Krs.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        Krs item => new StringValueNode(item.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
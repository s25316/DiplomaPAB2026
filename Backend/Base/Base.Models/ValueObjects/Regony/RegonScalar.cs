// Ignore Spelling: Regony, Regon
using Base.Models.ValueObjects.Krsy;
using HotChocolate.Language;

namespace Base.Models.ValueObjects.Regony;

internal class RegonScalar : ScalarType<Regon, StringValueNode>
{
    public RegonScalar() : base(nameof(Regon))
    {
        Description = Krs.GetDescription();
    }

    protected override StringValueNode ParseValue(Regon runtimeValue) => new(runtimeValue.ToString());
    protected override Regon ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return Regon.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        Regon item => new StringValueNode(item.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
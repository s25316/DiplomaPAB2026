// Ignore Spelling: Powiaty, Powiat, Wojewodztwo
using HotChocolate.Language;
using HotChocolate.Types;

namespace GUS.TERYT.Models.Requests.ValueObjects.Powiaty;

public sealed class PowiatIdScalar : ScalarType<PowiatId, StringValueNode>
{
    public PowiatIdScalar() : base(nameof(PowiatId))
    {
        Description = PowiatId.GetDescription();
    }

    protected override StringValueNode ParseValue(PowiatId runtimeValue) => new(runtimeValue.ToString());
    protected override PowiatId ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return PowiatId.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        PowiatId id => new StringValueNode(id.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
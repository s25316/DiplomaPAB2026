// Ignore Spelling: Miejscowosci, Miejscowosc
using HotChocolate.Language;
using HotChocolate.Types;

namespace GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;

public sealed class MiejscowoscIdScalar : ScalarType<MiejscowoscId, StringValueNode>
{
    public MiejscowoscIdScalar() : base(nameof(MiejscowoscId))
    {
        Description = MiejscowoscId.GetDescription();
    }

    protected override StringValueNode ParseValue(MiejscowoscId runtimeValue) => new(runtimeValue.ToString());
    protected override MiejscowoscId ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return MiejscowoscId.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        MiejscowoscId id => new StringValueNode(id.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
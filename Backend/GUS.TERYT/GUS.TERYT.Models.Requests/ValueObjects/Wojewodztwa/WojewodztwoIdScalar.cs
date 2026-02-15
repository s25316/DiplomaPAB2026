// Ignore Spelling: Wojewodztwo, Wojewodztwa, Voivodeship
using HotChocolate.Language;
using HotChocolate.Types;

namespace GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;

public sealed class WojewodztwoIdScalar : ScalarType<WojewodztwoId, StringValueNode>
{
    public WojewodztwoIdScalar() : base(nameof(WojewodztwoId))
    {
        Description = WojewodztwoId.GetDescription();
    }

    protected override StringValueNode ParseValue(WojewodztwoId runtimeValue) => new(runtimeValue.ToString());
    protected override WojewodztwoId ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return WojewodztwoId.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        WojewodztwoId id => new StringValueNode(id.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
// Ignore Spelling: Gminy, Gmina, Wojewodztwo, Powiat, Rodz
using HotChocolate.Language;
using HotChocolate.Types;

namespace GUS.TERYT.Models.Requests.ValueObjects.Gminy;

public sealed class GminaIdScalar : ScalarType<GminaId, StringValueNode>
{
    public GminaIdScalar() : base(nameof(GminaId))
    {
        Description = GminaId.GetDescription();
    }

    protected override StringValueNode ParseValue(GminaId runtimeValue) => new(runtimeValue.ToString());
    protected override GminaId ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return GminaId.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        GminaId id => new StringValueNode(id.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
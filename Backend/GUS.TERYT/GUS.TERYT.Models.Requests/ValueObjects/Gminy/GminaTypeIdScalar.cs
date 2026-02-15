// Ignore Spelling: Gminy, Gmina
using HotChocolate.Language;
using HotChocolate.Types;

namespace GUS.TERYT.Models.Requests.ValueObjects.Gminy;

public sealed class GminaTypeIdScalar : ScalarType<GminaTypeId, StringValueNode>
{
    public GminaTypeIdScalar() : base(nameof(GminaTypeId))
    {
        Description = GminaTypeId.GetDescription();
    }

    protected override StringValueNode ParseValue(GminaTypeId runtimeValue) => new(runtimeValue.ToString());
    protected override GminaTypeId ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return GminaTypeId.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        GminaTypeId id => new StringValueNode(id.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
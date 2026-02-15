// Ignore Spelling: Ulicy, Ulica
using HotChocolate.Language;
using HotChocolate.Types;

namespace GUS.TERYT.Models.Requests.ValueObjects.Ulicy;

public sealed class UlicaIdScalar : ScalarType<UlicaId, StringValueNode>
{
    public UlicaIdScalar() : base(nameof(UlicaId))
    {
        Description = UlicaId.GetDescription();
    }

    protected override StringValueNode ParseValue(UlicaId runtimeValue) => new(runtimeValue.ToString());
    protected override UlicaId ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return UlicaId.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        UlicaId id => new StringValueNode(id.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
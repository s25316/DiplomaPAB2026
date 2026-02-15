using HotChocolate.Language;
using HotChocolate.Types;

namespace GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;

public sealed class MiejscowoscTypeIdScalar : ScalarType<MiejscowoscTypeId, StringValueNode>
{
    public MiejscowoscTypeIdScalar() : base(nameof(MiejscowoscTypeId))
    {
        Description = MiejscowoscTypeId.GetDescription();
    }

    protected override StringValueNode ParseValue(MiejscowoscTypeId runtimeValue) => new(runtimeValue.ToString());
    protected override MiejscowoscTypeId ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return MiejscowoscTypeId.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        MiejscowoscTypeId id => new StringValueNode(id.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
using HotChocolate.Language;
using HotChocolate.Types;

namespace GUS.TERYT.Models.Requests.ValueObjects.Connections;

public sealed class ConnectionScalar : ScalarType<Connection, StringValueNode>
{
    public ConnectionScalar() : base(nameof(Connection))
    {
        Description = Connection.GetDescription();
    }

    protected override StringValueNode ParseValue(Connection runtimeValue) => new(runtimeValue.ToString());
    protected override Connection ParseLiteral(StringValueNode valueSyntax)
    {
        try
        {
            return Connection.Parse(valueSyntax.Value);
        }
        catch (Exception ex)
        {
            throw new SerializationException(ex.Message, this);
        }
    }

    public override IValueNode ParseResult(object? resultValue) => resultValue switch
    {
        null => NullValueNode.Default,
        Connection id => new StringValueNode(id.ToString()),
        string s => new StringValueNode(s),
        _ => throw new SerializationException($"{Messages.UnknownType}: {resultValue.GetType().FullName}", this)
    };
}
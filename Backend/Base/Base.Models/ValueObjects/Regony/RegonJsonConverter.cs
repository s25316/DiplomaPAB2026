using System.Text.Json;
using System.Text.Json.Serialization;

namespace Base.Models.ValueObjects.Regony;

public sealed class RegonJsonConverter : JsonConverter<Regon>
{
    public override Regon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? string.Empty;
        return Regon.Parse(value);
    }

    public override void Write(Utf8JsonWriter writer, Regon value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}

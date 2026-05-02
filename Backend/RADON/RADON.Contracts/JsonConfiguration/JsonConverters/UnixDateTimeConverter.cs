using System.Text.Json;
using System.Text.Json.Serialization;

namespace RADON.Contracts.JsonConfiguration.JsonConverters;

public class UnixDateTimeConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var milliseconds = (reader.TokenType) switch
        {
            JsonTokenType.Number => reader.TryGetInt64(out long numberMilliseconds)
                ? numberMilliseconds
                : throw new InvalidOperationException($"Unable parse data {reader.GetString()}."),


            JsonTokenType.String => long.TryParse(reader.GetString(), out long stringMilliseconds)
                ? stringMilliseconds
                : throw new InvalidOperationException($"Unable parse data {reader.GetString()}."),

            _ => throw new JsonException($"Expected TokenType{reader.TokenType}. Expected Number or String.")
        };

        return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteStringValue(value);
}
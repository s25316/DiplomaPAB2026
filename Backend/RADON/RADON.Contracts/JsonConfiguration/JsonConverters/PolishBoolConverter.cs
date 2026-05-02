// Ignore Spelling: tak, nie
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RADON.Contracts.JsonConfiguration.JsonConverters;

internal static class PolishBoolMapper
{
    public static bool ReadString(string? value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
        return value.ToLowerInvariant() switch
        {
            "tak" => true,
            "nie" => false,
            _ => throw new JsonException(value)
        };
    }

    public static string WtriteToString(bool value) => value switch
    {
        true => "Tak",
        false => "Nie"
    };
}

public class PolishBoolConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType switch
    {
        JsonTokenType.String => PolishBoolMapper.ReadString(reader.GetString()),
        JsonTokenType.False => false,
        JsonTokenType.True => true,
        _ => throw new JsonException($"Value '{reader.GetString()}' [{reader.TokenType}] is not a valid Polish boolean."),
    };

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        => writer.WriteStringValue(PolishBoolMapper.WtriteToString(value));
}

public class NullablePolishBoolConverter : JsonConverter<bool?>
{
    public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType switch
    {
        JsonTokenType.Null => null,
        JsonTokenType.String => PolishBoolMapper.ReadString(reader.GetString()),
        JsonTokenType.False => false,
        JsonTokenType.True => true,
        _ => throw new JsonException($"Value '{reader.GetString()}' [{reader.TokenType}] is not a valid Polish boolean."),
    };

    public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(PolishBoolMapper.WtriteToString(value.Value));
    }
}
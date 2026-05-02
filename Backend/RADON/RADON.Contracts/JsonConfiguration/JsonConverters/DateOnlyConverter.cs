// Ignore spelling: yyyy
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RADON.Contracts.JsonConfiguration.JsonConverters;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private const string DATE_FORMAT = "yyyy-MM-dd";


    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetDateTime(out var dt))
        {
            return DateOnly.FromDateTime(dt);
        }

        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            throw new JsonException("Date string is empty.");

        return DateOnly.ParseExact(value, DATE_FORMAT, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(DATE_FORMAT, CultureInfo.InvariantCulture));
}

public class NullableDateOnlyConverter : JsonConverter<DateOnly?>
{
    private const string DATE_FORMAT = "yyyy-MM-dd";


    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetDateTime(out var dt))
        {
            return DateOnly.FromDateTime(dt);
        }

        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return DateOnly.ParseExact(value, DATE_FORMAT, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(value.Value.ToString(DATE_FORMAT, CultureInfo.InvariantCulture));
    }
}
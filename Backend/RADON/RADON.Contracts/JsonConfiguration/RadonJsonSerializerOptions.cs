using RADON.Contracts.JsonConfiguration.JsonConverters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RADON.Contracts.JsonConfiguration;

public static class RadonJsonSerializerOptions
{
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        Converters =
        {
            new DateOnlyConverter(),
            new NullableDateOnlyConverter(),

            new PolishBoolConverter(),
            new NullablePolishBoolConverter(),

            new UnixDateTimeConverter(),
        },

        NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
    };
    public static JsonSerializerOptions JsonSerializerOptions => jsonSerializerOptions;
}
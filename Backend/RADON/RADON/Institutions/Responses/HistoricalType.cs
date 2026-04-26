using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class HistoricalType
{
    [JsonPropertyName("typeName")]
    public required string TypeName { get; init; }

    [JsonPropertyName("dateFrom")]
    public required string DateFrom { get; init; }
}
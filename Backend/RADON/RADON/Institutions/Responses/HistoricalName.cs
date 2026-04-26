using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class HistoricalName
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("dateFrom")]
    public required string DateFrom { get; init; }
}
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class HistoricalStatus
{
    [JsonPropertyName("statusName")]
    public required string StatusName { get; init; }

    [JsonPropertyName("dateFrom")]
    public required string DateFrom { get; init; }
}
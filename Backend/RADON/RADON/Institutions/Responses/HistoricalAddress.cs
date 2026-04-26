using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class HistoricalAddress
{
    [JsonPropertyName("country")]
    public string? Country { get; init; } = null;

    [JsonPropertyName("voivodeship")]
    public string? Voivodeship { get; init; } = null;

    [JsonPropertyName("city")]
    public string? City { get; init; } = null;

    [JsonPropertyName("postalCd")]
    public string? PostalCd { get; init; } = null;

    [JsonPropertyName("street")]
    public string? Street { get; init; } = null;

    [JsonPropertyName("bNumber")]
    public string? BNumber { get; init; } = null;

    [JsonPropertyName("lNumber")]
    public string? LNumber { get; init; } = null;

    [JsonPropertyName("dateFrom")]
    public string? DateFrom { get; init; } = null;
}
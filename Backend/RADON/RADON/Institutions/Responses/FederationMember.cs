using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class FederationMember
{
    [JsonPropertyName("institutionUuid")]
    public string? InstitutionUuid { get; init; } = null;

    [JsonPropertyName("institutionName")]
    public string? InstitutionName { get; init; } = null;

    [JsonPropertyName("dateFrom")]
    public string? DateFrom { get; init; } = null;

    [JsonPropertyName("dateTo")]
    public string? DateTo { get; init; } = null;
}
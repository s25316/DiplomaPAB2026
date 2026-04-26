using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class HistoricalSupervising
{
    [JsonPropertyName("supervisingInstitutionID")]
    public string? SupervisingInstitutionId { get; init; } = null;

    [JsonPropertyName("supervisingInstitutionName")]
    public string? SupervisingInstitutionName { get; init; } = null;

    [JsonPropertyName("dateFrom")]
    public string? DateFrom { get; init; } = null;
}
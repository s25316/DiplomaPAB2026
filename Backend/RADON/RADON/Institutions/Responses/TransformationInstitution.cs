using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class TransformationInstitution
{
    [JsonPropertyName("transformedInstitutionUuid")]
    public string? TransformedInstitutionUuid { get; init; } = null;

    [JsonPropertyName("targetInstitutionUuid")]
    public string? TargetInstitutionUuid { get; init; } = null;

    [JsonPropertyName("transformedInstitutionName")]
    public string? TransformedInstitutionName { get; init; } = null;

    [JsonPropertyName("targetInstitutionName")]
    public string? TargetInstitutionName { get; init; } = null;

    [JsonPropertyName("regon")]
    public string? Regon { get; init; } = null;

    [JsonPropertyName("nip")]
    public string? Nip { get; init; } = null;

    [JsonPropertyName("krs")]
    public string? Krs { get; init; } = null;

    [JsonPropertyName("eunNumber")]
    public string? EunNumber { get; init; } = null;

    [JsonPropertyName("panNumber")]
    public string? PanNumber { get; init; } = null;

    [JsonPropertyName("supervisingInstitutionID")]
    public string? SupervisingInstitutionId { get; init; } = null;

    [JsonPropertyName("supervisingInstitutionName")]
    public string? SupervisingInstitutionName { get; init; } = null;

    [JsonPropertyName("transformationKind")]
    public string? TransformationKind { get; init; } = null;

    [JsonPropertyName("transformationDate")]
    public string? TransformationDate { get; init; } = null;
}
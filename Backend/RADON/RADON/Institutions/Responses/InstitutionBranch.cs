using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class InstitutionBranch
{
    [JsonPropertyName("branchUuid")]
    public string? BranchUuid { get; init; } = null;

    [JsonPropertyName("branchName")]
    public string? BranchName { get; init; } = null;

    [JsonPropertyName("branchCity")]
    public string? BranchCity { get; init; } = null;
}
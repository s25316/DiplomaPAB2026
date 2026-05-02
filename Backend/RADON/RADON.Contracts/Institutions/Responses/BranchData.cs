using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Institutions.Response;

namespace RADON.Contracts.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_branch_data"]/summary' />
[Display(Name = nameof(Response.institution_branch_data), ResourceType = typeof(Response))]
public sealed class BranchData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_branch_branch_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_branch_branch_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("branchUuid")]
    public required Guid BranchUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_branch_branch_name"]/summary' />
    [Display(Name = nameof(Response.institution_branch_branch_name), ResourceType = typeof(Response))]
    [JsonPropertyName("branchName")]
    public required string BranchName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_branch_branch_city"]/summary' />
    [Display(Name = nameof(Response.institution_branch_branch_city), ResourceType = typeof(Response))]
    [JsonPropertyName("branchCity")]
    public required string BranchCity { get; init; }
}
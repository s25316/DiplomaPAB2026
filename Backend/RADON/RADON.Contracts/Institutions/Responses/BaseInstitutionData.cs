// Ignore Spelling: regon, krs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Institutions.Response;

namespace RADON.Contracts.Institutions.Responses;

public abstract class BaseInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_regon"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_regon), ResourceType = typeof(Response))]
    [JsonPropertyName("regon")]
    public required string? Regon { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_nip"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_nip), ResourceType = typeof(Response))]
    [JsonPropertyName("nip")]
    public required string? Nip { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_krs"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_krs), ResourceType = typeof(Response))]
    [JsonPropertyName("krs")]
    public required string? Krs { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_eun_number"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_eun_number), ResourceType = typeof(Response))]
    [JsonPropertyName("eunNumber")]
    public required string EunNumber { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_pan_number"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_pan_number), ResourceType = typeof(Response))]
    [JsonPropertyName("panNumber")]
    public required string PanNumber { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_supervising_institution_id"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_supervising_institution_id), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionID")]
    public required Guid SupervisingInstitutionId { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_supervising_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_supervising_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionName")]
    public required string SupervisingInstitutionName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_transformation_kind"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_transformation_kind), ResourceType = typeof(Response))]
    [JsonPropertyName("transformationKind")]
    public required string TransformationKind { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_transformation_date"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_transformation_date), ResourceType = typeof(Response))]
    [JsonPropertyName("transformationDate")]
    public required DateOnly TransformationDate { get; init; }
}
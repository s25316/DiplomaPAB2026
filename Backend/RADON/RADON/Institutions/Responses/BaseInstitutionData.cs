using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public abstract class BaseInstitutionData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_regon"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_regon), ResourceType = typeof(Response))]
    [JsonPropertyName("regon")]
    public string? Regon { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_nip"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_nip), ResourceType = typeof(Response))]
    [JsonPropertyName("nip")]
    public string? Nip { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_krs"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_krs), ResourceType = typeof(Response))]
    [JsonPropertyName("krs")]
    public string? Krs { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_eun_number"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_eun_number), ResourceType = typeof(Response))]
    [JsonPropertyName("eunNumber")]
    public string? EunNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_pan_number"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_pan_number), ResourceType = typeof(Response))]
    [JsonPropertyName("panNumber")]
    public string? PanNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_supervising_institution_id"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_supervising_institution_id), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionID")]
    public string? SupervisingInstitutionId { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_supervising_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_supervising_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionName")]
    public string? SupervisingInstitutionName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_transformation_kind"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_transformation_kind), ResourceType = typeof(Response))]
    [JsonPropertyName("transformationKind")]
    public string? TransformationKind { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_transformed_institution_transformation_date"]/summary' />
    [Display(Name = nameof(Response.institution_base_institution_transformation_date), ResourceType = typeof(Response))]
    [JsonPropertyName("transformationDate")]
    public string? TransformationDate { get; init; } = null;
}
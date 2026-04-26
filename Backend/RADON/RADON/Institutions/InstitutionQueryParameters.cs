using RADON.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RADON.Institutions;

public record InstitutionQueryParameters : IInputQueryParameters
{
    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_result_numbers_description"]/summary' />
    [DefaultValue(100)]
    [Display(Name = nameof(QueryParameter.institution_result_numbers_description), ResourceType = typeof(QueryParameter))]
    public int ResultNumbers { get; init; } = 100;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_token_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_token_description), ResourceType = typeof(QueryParameter))]
    public string? Token { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_institution_uuid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_institution_uuid_description), ResourceType = typeof(QueryParameter))]
    public string? InstitutionUuid { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_institution_uid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_institution_uid_description), ResourceType = typeof(QueryParameter))]
    public string? InstitutionUid { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_id_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_id_description), ResourceType = typeof(QueryParameter))]
    public string? Id { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_name_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_name_description), ResourceType = typeof(QueryParameter))]
    public string? Name { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_i_kind_cd_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_i_kind_cd_description), ResourceType = typeof(QueryParameter))]
    public List<string> IKindCd { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_status_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_status_code_description), ResourceType = typeof(QueryParameter))]
    public List<string> StatusCode { get; init; } = [];


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_voivodeship_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_voivodeship_code_description), ResourceType = typeof(QueryParameter))]
    public string? VoivodeshipCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_regon_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_regon_description), ResourceType = typeof(QueryParameter))]
    public string? Regon { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_supervising_institution_id_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_supervising_institution_id_description), ResourceType = typeof(QueryParameter))]
    public string? SupervisingInstitutionId { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_u_type_cd_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_u_type_cd_description), ResourceType = typeof(QueryParameter))]
    public string? UTypeCd { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_si_type_cd_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_si_type_cd_description), ResourceType = typeof(QueryParameter))]
    public string? SiTypeCd { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_pib_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_pib_description), ResourceType = typeof(QueryParameter))]
    public string? Pib { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_city_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_city_description), ResourceType = typeof(QueryParameter))]
    public string? City { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_branch_city_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_branch_city_description), ResourceType = typeof(QueryParameter))]
    public string? BranchCity { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_krs_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_krs_description), ResourceType = typeof(QueryParameter))]
    public string? Krs { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_nip_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_nip_description), ResourceType = typeof(QueryParameter))]
    public string? Nip { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_ministry_number_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_ministry_number_description), ResourceType = typeof(QueryParameter))]
    public string? MinistryNumber { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_pan_number_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_pan_number_description), ResourceType = typeof(QueryParameter))]
    public string? PanNumber { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_eun_number_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_eun_number_description), ResourceType = typeof(QueryParameter))]
    public string? EunNumber { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_i_start_date_from_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_i_start_date_from_description), ResourceType = typeof(QueryParameter))]
    public string? IStartDateFrom { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_i_start_date_to_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_i_start_date_to_description), ResourceType = typeof(QueryParameter))]
    public string? IStartDateTo { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="institution_last_refresh_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.institution_last_refresh_description), ResourceType = typeof(QueryParameter))]
    public string? LastRefresh { get; init; } = null;
}
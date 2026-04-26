using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class InstitutionReport
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("institutionUuid")]
    public string? InstitutionUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_institution_uid"]/summary' />
    [Display(Name = nameof(Response.institution_institution_uid), ResourceType = typeof(Response))]
    [JsonPropertyName("institutionUid")]
    public string? InstitutionUid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("name")]
    public string? Name { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_id"]/summary' />
    [Display(Name = nameof(Response.institution_id), ResourceType = typeof(Response))]
    [JsonPropertyName("id")]
    public string? Id { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_kind_cd"]/summary' />
    [Display(Name = nameof(Response.institution_i_kind_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("iKindCd")]
    public string? IKindCd { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_kind_name"]/summary' />
    [Display(Name = nameof(Response.institution_i_kind_name), ResourceType = typeof(Response))]
    [JsonPropertyName("iKindName")]
    public string? IKindName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_u_type_cd"]/summary' />
    [Display(Name = nameof(Response.institution_u_type_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("uTypeCd")]
    public string? UTypeCd { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_u_type_name"]/summary' />
    [Display(Name = nameof(Response.institution_u_type_name), ResourceType = typeof(Response))]
    [JsonPropertyName("uTypeName")]
    public string? UTypeName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_si_type_cd"]/summary' />
    [Display(Name = nameof(Response.institution_si_type_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("siTypeCd")]
    public string? SiTypeCd { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_si_type_name"]/summary' />
    [Display(Name = nameof(Response.institution_si_type_name), ResourceType = typeof(Response))]
    [JsonPropertyName("siTypeName")]
    public string? SiTypeName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_status"]/summary' />
    [Display(Name = nameof(Response.institution_status), ResourceType = typeof(Response))]
    [JsonPropertyName("status")]
    public string? Status { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_id"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_id), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionID")]
    public string? SupervisingInstitutionId { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionName")]
    public string? SupervisingInstitutionName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_country_cd"]/summary' />
    [Display(Name = nameof(Response.institution_country_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("countryCd")]
    public string? CountryCd { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_country"]/summary' />
    [Display(Name = nameof(Response.institution_country), ResourceType = typeof(Response))]
    [JsonPropertyName("country")]
    public string? Country { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_voivodeship"]/summary' />
    [Display(Name = nameof(Response.institution_voivodeship), ResourceType = typeof(Response))]
    [JsonPropertyName("voivodeship")]
    public string? Voivodeship { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_city"]/summary' />
    [Display(Name = nameof(Response.institution_city), ResourceType = typeof(Response))]
    [JsonPropertyName("city")]
    public string? City { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_postal_cd"]/summary' />
    [Display(Name = nameof(Response.institution_postal_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("postalCd")]
    public string? PostalCd { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_street"]/summary' />
    [Display(Name = nameof(Response.institution_street), ResourceType = typeof(Response))]
    [JsonPropertyName("street")]
    public string? Street { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_b_number"]/summary' />
    [Display(Name = nameof(Response.institution_b_number), ResourceType = typeof(Response))]
    [JsonPropertyName("bNumber")]
    public string? BNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_l_number"]/summary' />
    [Display(Name = nameof(Response.institution_l_number), ResourceType = typeof(Response))]
    [JsonPropertyName("lNumber")]
    public string? LNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_regon"]/summary' />
    [Display(Name = nameof(Response.institution_regon), ResourceType = typeof(Response))]
    [JsonPropertyName("regon")]
    public string? Regon { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_nip"]/summary' />
    [Display(Name = nameof(Response.institution_nip), ResourceType = typeof(Response))]
    [JsonPropertyName("nip")]
    public string? Nip { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_krs"]/summary' />
    [Display(Name = nameof(Response.institution_krs), ResourceType = typeof(Response))]
    [JsonPropertyName("krs")]
    public string? Krs { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_start_dt"]/summary' />
    [Display(Name = nameof(Response.institution_i_start_dt), ResourceType = typeof(Response))]
    [JsonPropertyName("iStartDT")]
    public string? IStartDt { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_liq_start_dt"]/summary' />
    [Display(Name = nameof(Response.institution_i_liq_start_dt), ResourceType = typeof(Response))]
    [JsonPropertyName("iLiqStartDT")]
    public string? ILiqStartDt { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_liq_dt"]/summary' />
    [Display(Name = nameof(Response.institution_i_liq_dt), ResourceType = typeof(Response))]
    [JsonPropertyName("iLiqDT")]
    public string? ILiqDt { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_www"]/summary' />
    [Display(Name = nameof(Response.institution_www), ResourceType = typeof(Response))]
    [JsonPropertyName("www")]
    public string? Www { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_e_mail"]/summary' />
    [Display(Name = nameof(Response.institution_e_mail), ResourceType = typeof(Response))]
    [JsonPropertyName("eMail")]
    public string? EMail { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_phone"]/summary' />
    [Display(Name = nameof(Response.institution_phone), ResourceType = typeof(Response))]
    [JsonPropertyName("phone")]
    public string? Phone { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_pib"]/summary' />
    [Display(Name = nameof(Response.institution_pib), ResourceType = typeof(Response))]
    [JsonPropertyName("pib")]
    public string? Pib { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_year_pib"]/summary' />
    [Display(Name = nameof(Response.institution_year_pib), ResourceType = typeof(Response))]
    [JsonPropertyName("yearPib")]
    public string? YearPib { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_status_code"]/summary' />
    [Display(Name = nameof(Response.institution_status_code), ResourceType = typeof(Response))]
    [JsonPropertyName("statusCode")]
    public string? StatusCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_voivodeship_code"]/summary' />
    [Display(Name = nameof(Response.institution_voivodeship_code), ResourceType = typeof(Response))]
    [JsonPropertyName("voivodeshipCode")]
    public string? VoivodeshipCode { get; init; } = null;


    [JsonPropertyName("branches")]
    public List<BranchData>? Branches { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_name"]/summary' />
    [Display(Name = nameof(Response.institution_manager_name), ResourceType = typeof(Response))]
    [JsonPropertyName("managerName")]
    public string? ManagerName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_surname"]/summary' />
    [Display(Name = nameof(Response.institution_manager_surname), ResourceType = typeof(Response))]
    [JsonPropertyName("managerSurname")]
    public string? ManagerSurname { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_other_names"]/summary' />
    [Display(Name = nameof(Response.institution_manager_other_names), ResourceType = typeof(Response))]
    [JsonPropertyName("managerOtherNames")]
    public string? ManagerOtherNames { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_surname_prefix"]/summary' />
    [Display(Name = nameof(Response.institution_manager_surname_prefix), ResourceType = typeof(Response))]
    [JsonPropertyName("managerSurnamePrefix")]
    public string? ManagerSurnamePrefix { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_employee_in_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_manager_employee_in_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("managerEmployeeInInstitutionUuid")]
    public string? ManagerEmployeeInInstitutionUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_function"]/summary' />
    [Display(Name = nameof(Response.institution_manager_function), ResourceType = typeof(Response))]
    [JsonPropertyName("managerFunction")]
    public string? ManagerFunction { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_esp_address"]/summary' />
    [Display(Name = nameof(Response.institution_esp_address), ResourceType = typeof(Response))]
    [JsonPropertyName("espAddress")]
    public string? EspAddress { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_eda_address"]/summary' />
    [Display(Name = nameof(Response.institution_eda_address), ResourceType = typeof(Response))]
    [JsonPropertyName("edaAddress")]
    public string? EdaAddress { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_pan_number"]/summary' />
    [Display(Name = nameof(Response.institution_pan_number), ResourceType = typeof(Response))]
    [JsonPropertyName("panNumber")]
    public string? PanNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_ministry_number"]/summary' />
    [Display(Name = nameof(Response.institution_ministry_number), ResourceType = typeof(Response))]
    [JsonPropertyName("ministryNumber")]
    public string? MinistryNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_eun_number"]/summary' />
    [Display(Name = nameof(Response.institution_eun_number), ResourceType = typeof(Response))]
    [JsonPropertyName("eunNumber")]
    public string? EunNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_federation_number"]/summary' />
    [Display(Name = nameof(Response.institution_federation_number), ResourceType = typeof(Response))]
    [JsonPropertyName("federationNumber")]
    public string? FederationNumber { get; init; } = null;


    [JsonPropertyName("federationComposition")]
    public List<FederationInstitutionData> FederationComposition { get; init; } = [];

    [JsonPropertyName("transformedInstitutions")]
    public List<TransformedInstitutionData> TransformedInstitutions { get; init; } = [];

    [JsonPropertyName("targetInstitutions")]
    public List<TargetInstitutionData> TargetInstitutions { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="institution_names"]/summary' />
    [Display(Name = nameof(Response.institution_names), ResourceType = typeof(Response))]
    [JsonPropertyName("names")]
    public List<NameData> Names { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institutions"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institutions), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutions")]
    public List<SupervisingInstitutionData> SupervisingInstitutions { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="institution_statuses"]/summary' />
    [Display(Name = nameof(Response.institution_statuses), ResourceType = typeof(Response))]
    [JsonPropertyName("statuses")]
    public List<StatusData> Statuses { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="institution_types"]/summary' />
    [Display(Name = nameof(Response.institution_types), ResourceType = typeof(Response))]
    [JsonPropertyName("types")]
    public List<TypeData> Types { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="institution_addresses"]/summary' />
    [Display(Name = nameof(Response.institution_addresses), ResourceType = typeof(Response))]
    [JsonPropertyName("addresses")]
    public List<AddressData> Addresses { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="institution_data_source"]/summary' />
    [Display(Name = nameof(Response.institution_data_source), ResourceType = typeof(Response))]
    [JsonPropertyName("dataSource")]
    public required string DataSource { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_last_refresh"]/summary' />
    [Display(Name = nameof(Response.institution_last_refresh), ResourceType = typeof(Response))]
    [JsonPropertyName("lastRefresh")]
    public required string LastRefresh { get; init; }
}
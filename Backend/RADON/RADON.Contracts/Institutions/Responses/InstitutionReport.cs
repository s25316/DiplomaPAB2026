using RADON.Contracts.JsonConfiguration.JsonConverters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Institutions.Response;

namespace RADON.Contracts.Institutions.Responses;

public sealed class InstitutionReport
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("institutionUuid")]
    public required Guid InstitutionUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_institution_uid"]/summary' />
    [Display(Name = nameof(Response.institution_institution_uid), ResourceType = typeof(Response))]
    [JsonPropertyName("institutionUid")]
    public required string InstitutionUid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_id"]/summary' />
    [Display(Name = nameof(Response.institution_id), ResourceType = typeof(Response))]
    [JsonPropertyName("id")]
    public int? Id { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_kind_cd"]/summary' />
    [Display(Name = nameof(Response.institution_i_kind_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("iKindCd")]
    public required string IKindCd { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_kind_name"]/summary' />
    [Display(Name = nameof(Response.institution_i_kind_name), ResourceType = typeof(Response))]
    [JsonPropertyName("iKindName")]
    public required string IKindName { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="institution_u_type_cd"]/summary' />
    [Display(Name = nameof(Response.institution_u_type_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("uTypeCd")]
    public required string? UTypeCd { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_u_type_name"]/summary' />
    [Display(Name = nameof(Response.institution_u_type_name), ResourceType = typeof(Response))]
    [JsonPropertyName("uTypeName")]
    public required string? UTypeName { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="institution_si_type_cd"]/summary' />
    [Display(Name = nameof(Response.institution_si_type_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("siTypeCd")]
    public required string? SiTypeCd { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_si_type_name"]/summary' />
    [Display(Name = nameof(Response.institution_si_type_name), ResourceType = typeof(Response))]
    [JsonPropertyName("siTypeName")]
    public required string? SiTypeName { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="institution_status"]/summary' />
    [Display(Name = nameof(Response.institution_status), ResourceType = typeof(Response))]
    [JsonPropertyName("status")]
    public required string Status { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_status_code"]/summary' />
    [Display(Name = nameof(Response.institution_status_code), ResourceType = typeof(Response))]
    [JsonPropertyName("statusCode")]
    public required string StatusCode { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_id"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_id), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionID")]
    public required Guid? SupervisingInstitutionId { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_supervising_institution_name"]/summary' />
    [Display(Name = nameof(Response.institution_supervising_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionName")]
    public required string? SupervisingInstitutionName { get; init; } = null;


    #region Address
    /// <include file='Response.xml' path='docs/members/member[@name="institution_country_cd"]/summary' />
    [Display(Name = nameof(Response.institution_country_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("countryCd")]
    public required string CountryCd { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_country"]/summary' />
    [Display(Name = nameof(Response.institution_country), ResourceType = typeof(Response))]
    [JsonPropertyName("country")]
    public required string Country { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_voivodeship"]/summary' />
    [Display(Name = nameof(Response.institution_voivodeship), ResourceType = typeof(Response))]
    [JsonPropertyName("voivodeship")]
    public required string Voivodeship { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_voivodeship_code"]/summary' />
    [Display(Name = nameof(Response.institution_voivodeship_code), ResourceType = typeof(Response))]
    [JsonPropertyName("voivodeshipCode")]
    public required string VoivodeshipCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_city"]/summary' />
    [Display(Name = nameof(Response.institution_city), ResourceType = typeof(Response))]
    [JsonPropertyName("city")]
    public required string City { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_postal_cd"]/summary' />
    [Display(Name = nameof(Response.institution_postal_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("postalCd")]
    public required string PostalCd { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_street"]/summary' />
    [Display(Name = nameof(Response.institution_street), ResourceType = typeof(Response))]
    [JsonPropertyName("street")]
    public required string? Street { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_b_number"]/summary' />
    [Display(Name = nameof(Response.institution_b_number), ResourceType = typeof(Response))]
    [JsonPropertyName("bNumber")]
    public required string BNumber { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_l_number"]/summary' />
    [Display(Name = nameof(Response.institution_l_number), ResourceType = typeof(Response))]
    [JsonPropertyName("lNumber")]
    public string? LNumber { get; init; } = null;
    #endregion


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
    public required DateOnly IStartDt { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_liq_start_dt"]/summary' />
    [Display(Name = nameof(Response.institution_i_liq_start_dt), ResourceType = typeof(Response))]
    [JsonPropertyName("iLiqStartDT")]
    public required DateOnly? ILiqStartDt { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_i_liq_dt"]/summary' />
    [Display(Name = nameof(Response.institution_i_liq_dt), ResourceType = typeof(Response))]
    [JsonPropertyName("iLiqDT")]
    public required DateOnly? ILiqDt { get; init; } = null;


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
    public required int Pib { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="institution_year_pib"]/summary' />
    [Display(Name = nameof(Response.institution_year_pib), ResourceType = typeof(Response))]
    [JsonPropertyName("yearPib")]
    public required int? YearPib { get; init; } = null;


    [JsonPropertyName("branches")]
    public List<BranchData>? Branches { get; init; } = null;


    #region Manager
    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_name"]/summary' />
    [Display(Name = nameof(Response.institution_manager_name), ResourceType = typeof(Response))]
    [JsonPropertyName("managerName")]
    public required string? ManagerName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_surname"]/summary' />
    [Display(Name = nameof(Response.institution_manager_surname), ResourceType = typeof(Response))]
    [JsonPropertyName("managerSurname")]
    public required string? ManagerSurname { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_other_names"]/summary' />
    [Display(Name = nameof(Response.institution_manager_other_names), ResourceType = typeof(Response))]
    [JsonPropertyName("managerOtherNames")]
    public required string? ManagerOtherNames { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_surname_prefix"]/summary' />
    [Display(Name = nameof(Response.institution_manager_surname_prefix), ResourceType = typeof(Response))]
    [JsonPropertyName("managerSurnamePrefix")]
    public required string? ManagerSurnamePrefix { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_employee_in_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_manager_employee_in_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("managerEmployeeInInstitutionUuid")]
    public required Guid? ManagerEmployeeInInstitutionUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_manager_function"]/summary' />
    [Display(Name = nameof(Response.institution_manager_function), ResourceType = typeof(Response))]
    [JsonPropertyName("managerFunction")]
    public required string? ManagerFunction { get; init; } = null;
    #endregion

    /// <include file='Response.xml' path='docs/members/member[@name="institution_esp_address"]/summary' />
    [Display(Name = nameof(Response.institution_esp_address), ResourceType = typeof(Response))]
    [JsonPropertyName("espAddress")]
    public required string? EspAddress { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_eda_address"]/summary' />
    [Display(Name = nameof(Response.institution_eda_address), ResourceType = typeof(Response))]
    [JsonPropertyName("edaAddress")]
    public string? EdaAddress { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_pan_number"]/summary' />
    [Display(Name = nameof(Response.institution_pan_number), ResourceType = typeof(Response))]
    [JsonPropertyName("panNumber")]
    public required string? PanNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_ministry_number"]/summary' />
    [Display(Name = nameof(Response.institution_ministry_number), ResourceType = typeof(Response))]
    [JsonPropertyName("ministryNumber")]
    public required string? MinistryNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_eun_number"]/summary' />
    [Display(Name = nameof(Response.institution_eun_number), ResourceType = typeof(Response))]
    [JsonPropertyName("eunNumber")]
    public required string? EunNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_federation_number"]/summary' />
    [Display(Name = nameof(Response.institution_federation_number), ResourceType = typeof(Response))]
    [JsonPropertyName("federationNumber")]
    public required string? FederationNumber { get; init; } = null;


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
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public required DateTimeOffset LastRefresh { get; init; }
}
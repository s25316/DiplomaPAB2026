using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

public class InstitutionReport
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.institution_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("institutionUuid")]
    public string? InstitutionUuid { get; init; } = null;

    [JsonPropertyName("institutionUid")]
    public string? InstitutionUid { get; init; } = null;

    [JsonPropertyName("name")]
    public string? Name { get; init; } = null;

    [JsonPropertyName("id")]
    public string? Id { get; init; } = null;

    [JsonPropertyName("iKindCd")]
    public string? IKindCd { get; init; } = null;

    [JsonPropertyName("iKindName")]
    public string? IKindName { get; init; } = null;

    [JsonPropertyName("uTypeCd")]
    public string? UTypeCd { get; init; } = null;

    [JsonPropertyName("uTypeName")]
    public string? UTypeName { get; init; } = null;

    [JsonPropertyName("siTypeCd")]
    public string? SiTypeCd { get; init; } = null;

    [JsonPropertyName("siTypeName")]
    public string? SiTypeName { get; init; } = null;

    [JsonPropertyName("status")]
    public string? Status { get; init; } = null;

    [JsonPropertyName("supervisingInstitutionID")]
    public string? SupervisingInstitutionId { get; init; } = null;

    [JsonPropertyName("supervisingInstitutionName")]
    public string? SupervisingInstitutionName { get; init; } = null;

    [JsonPropertyName("countryCd")]
    public string? CountryCd { get; init; } = null;

    [JsonPropertyName("country")]
    public string? Country { get; init; } = null;

    [JsonPropertyName("voivodeship")]
    public string? Voivodeship { get; init; } = null;

    [JsonPropertyName("city")]
    public string? City { get; init; } = null;

    [JsonPropertyName("postalCd")]
    public string? PostalCd { get; init; } = null;

    [JsonPropertyName("street")]
    public string? Street { get; init; } = null;

    [JsonPropertyName("bNumber")]
    public string? BNumber { get; init; } = null;

    [JsonPropertyName("lNumber")]
    public string? LNumber { get; init; } = null;

    [JsonPropertyName("regon")]
    public string? Regon { get; init; } = null;

    [JsonPropertyName("nip")]
    public string? Nip { get; init; } = null;

    [JsonPropertyName("krs")]
    public string? Krs { get; init; } = null;

    [JsonPropertyName("iStartDT")]
    public string? IStartDt { get; init; } = null;

    [JsonPropertyName("iLiqStartDT")]
    public string? ILiqStartDt { get; init; } = null;

    [JsonPropertyName("iLiqDT")]
    public string? ILiqDt { get; init; } = null;

    [JsonPropertyName("www")]
    public string? Www { get; init; } = null;

    [JsonPropertyName("eMail")]
    public string? EMail { get; init; } = null;

    [JsonPropertyName("phone")]
    public string? Phone { get; init; } = null;

    [JsonPropertyName("pib")]
    public string? Pib { get; init; } = null;

    [JsonPropertyName("yearPib")]
    public string? YearPib { get; init; } = null;

    [JsonPropertyName("statusCode")]
    public string? StatusCode { get; init; } = null;

    [JsonPropertyName("voivodeshipCode")]
    public string? VoivodeshipCode { get; init; } = null;


    [JsonPropertyName("branches")]
    public List<InstitutionBranch>? Branches { get; init; } = null;


    [JsonPropertyName("managerName")]
    public string? ManagerName { get; init; } = null;

    [JsonPropertyName("managerSurname")]
    public string? ManagerSurname { get; init; } = null;

    [JsonPropertyName("managerOtherNames")]
    public string? ManagerOtherNames { get; init; } = null;

    [JsonPropertyName("managerSurnamePrefix")]
    public string? ManagerSurnamePrefix { get; init; } = null;

    [JsonPropertyName("managerEmployeeInInstitutionUuid")]
    public string? ManagerEmployeeInInstitutionUuid { get; init; } = null;

    [JsonPropertyName("managerFunction")]
    public string? ManagerFunction { get; init; } = null;

    [JsonPropertyName("espAddress")]
    public string? EspAddress { get; init; } = null;

    [JsonPropertyName("edaAddress")]
    public string? EdaAddress { get; init; } = null;

    [JsonPropertyName("panNumber")]
    public string? PanNumber { get; init; } = null;

    [JsonPropertyName("ministryNumber")]
    public string? MinistryNumber { get; init; } = null;

    [JsonPropertyName("eunNumber")]
    public string? EunNumber { get; init; } = null;

    [JsonPropertyName("federationNumber")]
    public string? FederationNumber { get; init; } = null;


    [JsonPropertyName("federationComposition")]
    public List<FederationMember> FederationComposition { get; init; } = [];

    [JsonPropertyName("transformedInstitutions")]
    public List<TransformationInstitution> TransformedInstitutions { get; init; } = [];

    [JsonPropertyName("targetInstitutions")]
    public List<TransformationInstitution> TargetInstitutions { get; init; } = [];

    [JsonPropertyName("names")]
    public List<HistoricalName> Names { get; init; } = [];

    [JsonPropertyName("supervisingInstitutions")]
    public List<HistoricalSupervising> SupervisingInstitutions { get; init; } = [];

    [JsonPropertyName("statuses")]
    public List<HistoricalStatus> Statuses { get; init; } = [];

    [JsonPropertyName("types")]
    public List<HistoricalType> Types { get; init; } = [];

    [JsonPropertyName("addresses")]
    public List<HistoricalAddress> Addresses { get; init; } = [];


    [JsonPropertyName("dataSource")]
    public required string DataSource { get; init; }

    [JsonPropertyName("lastRefresh")]
    public required string LastRefresh { get; init; }
}
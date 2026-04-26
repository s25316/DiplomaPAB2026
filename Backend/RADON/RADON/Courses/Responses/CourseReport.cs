using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_report"]/summary' />
[Display(Name = nameof(Response.course_report), ResourceType = typeof(Response))]
public class CourseReport
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_uuid"]/summary' />
    [Display(Name = nameof(Response.course_course_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("courseUuid")]
    public string? CourseUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_code"]/summary' />
    [Display(Name = nameof(Response.course_course_code), ResourceType = typeof(Response))]
    [JsonPropertyName("courseCode")]
    public string? CourseCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_old_code"]/summary' />
    [Display(Name = nameof(Response.course_course_old_code), ResourceType = typeof(Response))]
    [JsonPropertyName("courseOldCode")]
    public string? CourseOldCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_name"]/summary' />
    [Display(Name = nameof(Response.course_course_name), ResourceType = typeof(Response))]
    [JsonPropertyName("courseName")]
    public string? CourseName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_level_code"]/summary' />
    [Display(Name = nameof(Response.course_level_code), ResourceType = typeof(Response))]
    [JsonPropertyName("levelCode")]
    public string? LevelCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_level_name"]/summary' />
    [Display(Name = nameof(Response.course_level_name), ResourceType = typeof(Response))]
    [JsonPropertyName("levelName")]
    public string? LevelName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_profile_code"]/summary' />
    [Display(Name = nameof(Response.course_profile_code), ResourceType = typeof(Response))]
    [JsonPropertyName("profileCode")]
    public string? ProfileCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_profile_name"]/summary' />
    [Display(Name = nameof(Response.course_profile_name), ResourceType = typeof(Response))]
    [JsonPropertyName("profileName")]
    public string? ProfileName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_isced_code"]/summary' />
    [Display(Name = nameof(Response.course_isced_code), ResourceType = typeof(Response))]
    [JsonPropertyName("iscedCode")]
    public string? IscedCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_isced_name"]/summary' />
    [Display(Name = nameof(Response.course_isced_name), ResourceType = typeof(Response))]
    [JsonPropertyName("iscedName")]
    public string? IscedName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_creation_date"]/summary' />
    [Display(Name = nameof(Response.course_creation_date), ResourceType = typeof(Response))]
    [JsonPropertyName("creationDate")]
    public string? CreationDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_teacher_training"]/summary' />
    [Display(Name = nameof(Response.course_teacher_training), ResourceType = typeof(Response))]
    [JsonPropertyName("teacherTraining")]
    public string? TeacherTraining { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_philological"]/summary' />
    [Display(Name = nameof(Response.course_philological), ResourceType = typeof(Response))]
    [JsonPropertyName("philological")]
    public string? Philological { get; init; } = null;


    [JsonPropertyName("philologicalLanguages")]
    public List<LanguageData> PhilologicalLanguages { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_co_led"]/summary' />
    [Display(Name = nameof(Response.course_co_led), ResourceType = typeof(Response))]
    [JsonPropertyName("coLed")]
    public string? CoLed { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_led_date_from"]/summary' />
    [Display(Name = nameof(Response.course_co_led_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("coLedDateFrom")]
    public string? CoLedDateFrom { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_led_interdisciplinary"]/summary' />
    [Display(Name = nameof(Response.course_co_led_interdisciplinary), ResourceType = typeof(Response))]
    [JsonPropertyName("coLedInterdisciplinary")]
    public string? CoLedInterdisciplinary { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_individual_studies_without_leading_discipline"]/summary' />
    [Display(Name = nameof(Response.course_individual_studies_without_leading_discipline), ResourceType = typeof(Response))]
    [JsonPropertyName("individualStudiesWithoutLeadingDiscipline")]
    public string? IndividualStudiesWithoutLeadingDiscipline { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_current_status_code"]/summary' />
    [Display(Name = nameof(Response.course_current_status_code), ResourceType = typeof(Response))]
    [JsonPropertyName("currentStatusCode")]
    public string? CurrentStatusCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_current_status_name"]/summary' />
    [Display(Name = nameof(Response.course_current_status_name), ResourceType = typeof(Response))]
    [JsonPropertyName("currentStatusName")]
    public string? CurrentStatusName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_termination_initialization_date"]/summary' />
    [Display(Name = nameof(Response.course_termination_initialization_date), ResourceType = typeof(Response))]
    [JsonPropertyName("terminationInitializationDate")]
    public string? TerminationInitializationDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_liquidation_date"]/summary' />
    [Display(Name = nameof(Response.course_liquidation_date), ResourceType = typeof(Response))]
    [JsonPropertyName("liquidationDate")]
    public string? LiquidationDate { get; init; } = null;


    [JsonPropertyName("disciplines")]
    public List<DisciplineData> Disciplines { get; init; } = [];

    [JsonPropertyName("pka")]
    public List<EvaluationPkaData> Pka { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionUuid")]
    public string? LeadingInstitutionUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_name"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionName")]
    public string? LeadingInstitutionName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_is_foreign"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_is_foreign), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionIsForeign")]
    public string? LeadingInstitutionIsForeign { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_city"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_city), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionCity")]
    public string? LeadingInstitutionCity { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_voivodeship"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_voivodeship), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionVoivodeship")]
    public string? LeadingInstitutionVoivodeship { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_voivodeship_code"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_voivodeship_code), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionVoivodeshipCode")]
    public string? LeadingInstitutionVoivodeshipCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_main_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.course_main_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("mainInstitutionUuid")]
    public string? MainInstitutionUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_main_institution_name"]/summary' />
    [Display(Name = nameof(Response.course_main_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("mainInstitutionName")]
    public string? MainInstitutionName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_main_institution_kind"]/summary' />
    [Display(Name = nameof(Response.course_main_institution_kind), ResourceType = typeof(Response))]
    [JsonPropertyName("mainInstitutionKind")]
    public string? MainInstitutionKind { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_main_institution_kind_code"]/summary' />
    [Display(Name = nameof(Response.course_main_institution_kind_code), ResourceType = typeof(Response))]
    [JsonPropertyName("mainInstitutionKindCode")]
    public string? MainInstitutionKindCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_supervising_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.course_supervising_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionUuid")]
    public string? SupervisingInstitutionUuid { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_supervising_institution_name"]/summary' />
    [Display(Name = nameof(Response.course_supervising_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionName")]
    public string? SupervisingInstitutionName { get; init; } = null;


    [JsonPropertyName("coLeadingInstitutions")]
    public List<CoLeadingInstitutionData> CoLeadingInstitutions { get; init; } = [];

    [JsonPropertyName("organizationalUnits")]
    public List<OrganizationalUnitData> OrganizationalUnits { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_legal_basis_type_code"]/summary' />
    [Display(Name = nameof(Response.course_legal_basis_type_code), ResourceType = typeof(Response))]
    [JsonPropertyName("legalBasisTypeCode")]
    public string? LegalBasisTypeCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_legal_basis_type_name"]/summary' />
    [Display(Name = nameof(Response.course_legal_basis_type_name), ResourceType = typeof(Response))]
    [JsonPropertyName("legalBasisTypeName")]
    public string? LegalBasisTypeName { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_legal_basis_number"]/summary' />
    [Display(Name = nameof(Response.course_legal_basis_number), ResourceType = typeof(Response))]
    [JsonPropertyName("legalBasisNumber")]
    public string? LegalBasisNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_legal_basis_date"]/summary' />
    [Display(Name = nameof(Response.course_legal_basis_date), ResourceType = typeof(Response))]
    [JsonPropertyName("legalBasisDate")]
    public string? LegalBasisDate { get; init; } = null;


    [JsonPropertyName("courseInstances")]
    public List<CourseInstanceData> CourseInstances { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_data_source"]/summary' />
    [Display(Name = nameof(Response.course_data_source), ResourceType = typeof(Response))]
    [JsonPropertyName("dataSource")]
    public string? DataSource { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_last_refresh"]/summary' />
    [Display(Name = nameof(Response.course_last_refresh), ResourceType = typeof(Response))]
    [JsonPropertyName("lastRefresh")]
    public string? LastRefresh { get; init; } = null;
}
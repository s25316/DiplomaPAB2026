using RADON.Contracts.JsonConfiguration.JsonConverters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Response = RADON.Contracts.Descriptions.Courses.Response;

namespace RADON.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="course_report"]/summary' />
[Display(Name = nameof(Response.course_report), ResourceType = typeof(Response))]
public sealed class CourseReport
{
    /// <include file='Response.xml' path='docs/members/member[@name="course_uuid"]/summary' />
    [Display(Name = nameof(Response.course_course_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("courseUuid")]
    public required Guid CourseUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_code"]/summary' />
    [Display(Name = nameof(Response.course_course_code), ResourceType = typeof(Response))]
    [JsonPropertyName("courseCode")]
    public required int CourseCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_old_code"]/summary' />
    [Display(Name = nameof(Response.course_course_old_code), ResourceType = typeof(Response))]
    [JsonPropertyName("courseOldCode")]
    public required int? CourseOldCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_name"]/summary' />
    [Display(Name = nameof(Response.course_course_name), ResourceType = typeof(Response))]
    [JsonPropertyName("courseName")]
    public required string CourseName { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="course_level_code"]/summary' />
    [Display(Name = nameof(Response.course_level_code), ResourceType = typeof(Response))]
    [JsonPropertyName("levelCode")]
    public required string LevelCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_level_name"]/summary' />
    [Display(Name = nameof(Response.course_level_name), ResourceType = typeof(Response))]
    [JsonPropertyName("levelName")]
    public required string LevelName { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="course_profile_code"]/summary' />
    [Display(Name = nameof(Response.course_profile_code), ResourceType = typeof(Response))]
    [JsonPropertyName("profileCode")]
    public required string ProfileCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_profile_name"]/summary' />
    [Display(Name = nameof(Response.course_profile_name), ResourceType = typeof(Response))]
    [JsonPropertyName("profileName")]
    public required string ProfileName { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="course_isced_code"]/summary' />
    [Display(Name = nameof(Response.course_isced_code), ResourceType = typeof(Response))]
    [JsonPropertyName("iscedCode")]
    public required string IscedCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_isced_name"]/summary' />
    [Display(Name = nameof(Response.course_isced_name), ResourceType = typeof(Response))]
    [JsonPropertyName("iscedName")]
    public required string IscedName { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="course_current_status_code"]/summary' />
    [Display(Name = nameof(Response.course_current_status_code), ResourceType = typeof(Response))]
    [JsonPropertyName("currentStatusCode")]
    public required string CurrentStatusCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_current_status_name"]/summary' />
    [Display(Name = nameof(Response.course_current_status_name), ResourceType = typeof(Response))]
    [JsonPropertyName("currentStatusName")]
    public required string CurrentStatusName { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="course_creation_date"]/summary' />
    [Display(Name = nameof(Response.course_creation_date), ResourceType = typeof(Response))]
    [JsonPropertyName("creationDate")]
    public required DateOnly? CreationDate { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_teacher_training"]/summary' />
    [Display(Name = nameof(Response.course_teacher_training), ResourceType = typeof(Response))]
    [JsonPropertyName("teacherTraining")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool TeacherTraining { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_philological"]/summary' />
    [Display(Name = nameof(Response.course_philological), ResourceType = typeof(Response))]
    [JsonPropertyName("philological")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool Philological { get; init; }


    [JsonPropertyName("philologicalLanguages")]
    public List<LanguageData> PhilologicalLanguages { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_co_led"]/summary' />
    [Display(Name = nameof(Response.course_co_led), ResourceType = typeof(Response))]
    [JsonPropertyName("coLed")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool CoLed { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_led_date_from"]/summary' />
    [Display(Name = nameof(Response.course_co_led_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("coLedDateFrom")]
    public required DateOnly? CoLedDateFrom { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_co_led_interdisciplinary"]/summary' />
    [Display(Name = nameof(Response.course_co_led_interdisciplinary), ResourceType = typeof(Response))]
    [JsonPropertyName("coLedInterdisciplinary")]
    [JsonConverter(typeof(NullablePolishBoolConverter))]
    public required bool? CoLedInterdisciplinary { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_individual_studies_without_leading_discipline"]/summary' />
    [Display(Name = nameof(Response.course_individual_studies_without_leading_discipline), ResourceType = typeof(Response))]
    [JsonPropertyName("individualStudiesWithoutLeadingDiscipline")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool IndividualStudiesWithoutLeadingDiscipline { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_termination_initialization_date"]/summary' />
    [Display(Name = nameof(Response.course_termination_initialization_date), ResourceType = typeof(Response))]
    [JsonPropertyName("terminationInitializationDate")]
    public required DateOnly? TerminationInitializationDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_liquidation_date"]/summary' />
    [Display(Name = nameof(Response.course_liquidation_date), ResourceType = typeof(Response))]
    [JsonPropertyName("liquidationDate")]
    public required DateOnly? LiquidationDate { get; init; } = null;


    [JsonPropertyName("disciplines")]
    public List<DisciplineData> Disciplines { get; init; } = [];

    [JsonPropertyName("pka")]
    public List<EvaluationPkaData> Pka { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionUuid")]
    public required Guid LeadingInstitutionUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_name"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionName")]
    public required string LeadingInstitutionName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_is_foreign"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_is_foreign), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionIsForeign")]
    [JsonConverter(typeof(PolishBoolConverter))]
    public required bool LeadingInstitutionIsForeign { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_city"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_city), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionCity")]
    public required string? LeadingInstitutionCity { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_voivodeship"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_voivodeship), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionVoivodeship")]
    public required string? LeadingInstitutionVoivodeship { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_leading_institution_voivodeship_code"]/summary' />
    [Display(Name = nameof(Response.course_leading_institution_voivodeship_code), ResourceType = typeof(Response))]
    [JsonPropertyName("leadingInstitutionVoivodeshipCode")]
    public required string? LeadingInstitutionVoivodeshipCode { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="course_main_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.course_main_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("mainInstitutionUuid")]
    public required Guid MainInstitutionUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_main_institution_name"]/summary' />
    [Display(Name = nameof(Response.course_main_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("mainInstitutionName")]
    public required string MainInstitutionName { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="course_main_institution_kind"]/summary' />
    [Display(Name = nameof(Response.course_main_institution_kind), ResourceType = typeof(Response))]
    [JsonPropertyName("mainInstitutionKind")]
    public required string MainInstitutionKind { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_main_institution_kind_code"]/summary' />
    [Display(Name = nameof(Response.course_main_institution_kind_code), ResourceType = typeof(Response))]
    [JsonPropertyName("mainInstitutionKindCode")]
    public required string MainInstitutionKindCode { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="course_supervising_institution_uuid"]/summary' />
    [Display(Name = nameof(Response.course_supervising_institution_uuid), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionUuid")]
    public required Guid SupervisingInstitutionUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_supervising_institution_name"]/summary' />
    [Display(Name = nameof(Response.course_supervising_institution_name), ResourceType = typeof(Response))]
    [JsonPropertyName("supervisingInstitutionName")]
    public required string SupervisingInstitutionName { get; init; }


    [JsonPropertyName("coLeadingInstitutions")]
    public List<CoLeadingInstitutionData> CoLeadingInstitutions { get; init; } = [];

    [JsonPropertyName("organizationalUnits")]
    public List<OrganizationalUnitData> OrganizationalUnits { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_legal_basis_type_code"]/summary' />
    [Display(Name = nameof(Response.course_legal_basis_type_code), ResourceType = typeof(Response))]
    [JsonPropertyName("legalBasisTypeCode")]
    public required string LegalBasisTypeCode { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_legal_basis_type_name"]/summary' />
    [Display(Name = nameof(Response.course_legal_basis_type_name), ResourceType = typeof(Response))]
    [JsonPropertyName("legalBasisTypeName")]
    public required string LegalBasisTypeName { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_legal_basis_number"]/summary' />
    [Display(Name = nameof(Response.course_legal_basis_number), ResourceType = typeof(Response))]
    [JsonPropertyName("legalBasisNumber")]
    public required string? LegalBasisNumber { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_legal_basis_date"]/summary' />
    [Display(Name = nameof(Response.course_legal_basis_date), ResourceType = typeof(Response))]
    [JsonPropertyName("legalBasisDate")]
    public required DateOnly? LegalBasisDate { get; init; } = null;


    [JsonPropertyName("courseInstances")]
    public List<CourseInstanceData> CourseInstances { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="course_data_source"]/summary' />
    [Display(Name = nameof(Response.course_data_source), ResourceType = typeof(Response))]
    [JsonPropertyName("dataSource")]
    public required string DataSource { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="course_last_refresh"]/summary' />
    [Display(Name = nameof(Response.course_last_refresh), ResourceType = typeof(Response))]
    [JsonPropertyName("lastRefresh")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public required DateTimeOffset LastRefresh { get; init; }
}
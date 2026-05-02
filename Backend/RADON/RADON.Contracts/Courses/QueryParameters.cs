using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using QueryParameter = RADON.Contracts.Descriptions.Courses.QueryParameter;

namespace RADON.Contracts.Courses;

public sealed record QueryParameters
{
    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_result_numbers_description"]/summary' />
    [DefaultValue(100)]
    [Display(Name = nameof(QueryParameter.course_result_numbers_description), ResourceType = typeof(QueryParameter))]
    public int ResultNumbers { get; init; } = 100;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_token_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_token_description), ResourceType = typeof(QueryParameter))]
    public string? Token { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_course_uuid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_course_uuid_description), ResourceType = typeof(QueryParameter))]
    public string? CourseUuid { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_course_instance_uuid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_course_instance_uuid_description), ResourceType = typeof(QueryParameter))]
    public string? CourseInstanceUuid { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_course_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_course_code_description), ResourceType = typeof(QueryParameter))]
    public string? CourseCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_course_instance_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_course_instance_code_description), ResourceType = typeof(QueryParameter))]
    public string? CourseInstanceCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_course_name_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_course_name_description), ResourceType = typeof(QueryParameter))]
    public string? CourseName { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_level_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_level_code_description), ResourceType = typeof(QueryParameter))]
    public string? LevelCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_profile_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_profile_code_description), ResourceType = typeof(QueryParameter))]
    public string? ProfileCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_current_status_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_current_status_code_description), ResourceType = typeof(QueryParameter))]
    public string? CurrentStatusCode { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_leading_institution_uuid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_leading_institution_uuid_description), ResourceType = typeof(QueryParameter))]
    public string? LeadingInstitutionUuid { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_leading_institution_name_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_leading_institution_name_description), ResourceType = typeof(QueryParameter))]
    public string? LeadingInstitutionName { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_leading_institution_is_foreign_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_leading_institution_is_foreign_description), ResourceType = typeof(QueryParameter))]
    public string? LeadingInstitutionIsForeign { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_leading_institution_voivodeship_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_leading_institution_voivodeship_code_description), ResourceType = typeof(QueryParameter))]
    public string? LeadingInstitutionVoivodeshipCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_leading_institution_city_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_leading_institution_city_description), ResourceType = typeof(QueryParameter))]
    public string? LeadingInstitutionCity { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_main_institution_uuid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_main_institution_uuid_description), ResourceType = typeof(QueryParameter))]
    public string? MainInstitutionUuid { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_main_institution_name_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_main_institution_name_description), ResourceType = typeof(QueryParameter))]
    public string? MainInstitutionName { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_main_institution_kind_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_main_institution_kind_code_description), ResourceType = typeof(QueryParameter))]
    public string? MainInstitutionKindCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_supervising_institution_uuid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_supervising_institution_uuid_description), ResourceType = typeof(QueryParameter))]
    public string? SupervisingInstitutionUuid { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_co_leading_institution_uuid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_co_leading_institution_uuid_description), ResourceType = typeof(QueryParameter))]
    public string? CoLeadingInstitutionUuid { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_co_leading_institution_name_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_co_leading_institution_name_description), ResourceType = typeof(QueryParameter))]
    public string? CoLeadingInstitutionName { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_co_leading_institution_is_foreign_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_co_leading_institution_is_foreign_description), ResourceType = typeof(QueryParameter))]
    public string? CoLeadingInstitutionIsForeign { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_organizational_unit_uuid_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_organizational_unit_uuid_description), ResourceType = typeof(QueryParameter))]
    public string? OrganizationalUnitUuid { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_organizational_unit_full_name_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_organizational_unit_full_name_description), ResourceType = typeof(QueryParameter))]
    public string? OrganizationalUnitFullName { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_discipline_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_discipline_code_description), ResourceType = typeof(QueryParameter))]
    public string? DisciplineCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_discipline_name_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_discipline_name_description), ResourceType = typeof(QueryParameter))]
    public string? DisciplineName { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_legal_basis_type_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_legal_basis_type_code_description), ResourceType = typeof(QueryParameter))]
    public string? LegalBasisTypeCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_form_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_form_code_description), ResourceType = typeof(QueryParameter))]
    public string? FormCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_title_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_title_code_description), ResourceType = typeof(QueryParameter))]
    public string? TitleCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_status_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_status_code_description), ResourceType = typeof(QueryParameter))]
    public string? StatusCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_teacher_training_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_teacher_training_description), ResourceType = typeof(QueryParameter))]
    public string? TeacherTraining { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_philological_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_philological_description), ResourceType = typeof(QueryParameter))]
    public string? Philological { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_philology_language_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_philology_language_code_description), ResourceType = typeof(QueryParameter))]
    public string? PhilologyLanguageCode { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_co_led_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_co_led_description), ResourceType = typeof(QueryParameter))]
    public string? CoLed { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_dual_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_dual_description), ResourceType = typeof(QueryParameter))]
    public string? Dual { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_bridging_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_bridging_description), ResourceType = typeof(QueryParameter))]
    public string? Bridging { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_coop_with_vocational_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_coop_with_vocational_description), ResourceType = typeof(QueryParameter))]
    public string? CoopWithVocational { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_education_language_code_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_education_language_code_description), ResourceType = typeof(QueryParameter))]
    public string? EducationLanguageCode { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="course_last_refresh_description"]/summary' />
    [DefaultValue(null)]
    [Display(Name = nameof(QueryParameter.course_last_refresh_description), ResourceType = typeof(QueryParameter))]
    public string? LastRefresh { get; init; } = null;
}
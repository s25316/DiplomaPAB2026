using System.ComponentModel.DataAnnotations;

namespace RADON.Dictionaries;

public enum DictionaryType
{
    // --- INSTITUTION ---

    /// <include file='Dictionary.xml' path='docs/members/member[@name="institution_kinds"]/summary' />
    [Display(Name = nameof(Dictionary.institution_kinds), ResourceType = typeof(Dictionary))]
    InstitutionKinds,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="institution_statuses"]/summary' />
    [Display(Name = nameof(Dictionary.institution_statuses), ResourceType = typeof(Dictionary))]
    InstitutionStatuses,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="institution_university_types"]/summary' />
    [Display(Name = nameof(Dictionary.institution_university_types), ResourceType = typeof(Dictionary))]
    InstitutionUniversityTypes,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="institution_scientific_institution_types"]/summary' />
    [Display(Name = nameof(Dictionary.institution_scientific_institution_types), ResourceType = typeof(Dictionary))]
    InstitutionScientificInstitutionTypes,


    // --- SHARED ---

    /// <include file='Dictionary.xml' path='docs/members/member[@name="shared_voivodeships"]/summary' />
    [Display(Name = nameof(Dictionary.shared_voivodeships), ResourceType = typeof(Dictionary))]
    SharedVoivodeships,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="shared_supervising_institutions"]/summary' />
    [Display(Name = nameof(Dictionary.shared_supervising_institutions), ResourceType = typeof(Dictionary))]
    SharedSupervisingInstitutions,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="shared_disciplines"]/summary' />
    [Display(Name = nameof(Dictionary.shared_disciplines), ResourceType = typeof(Dictionary))]
    SharedDisciplines,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="shared_domains"]/summary' />
    [Display(Name = nameof(Dictionary.shared_domains), ResourceType = typeof(Dictionary))]
    SharedDomains,


    // --- COURSE ---

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_levels"]/summary' />
    [Display(Name = nameof(Dictionary.course_levels), ResourceType = typeof(Dictionary))]
    CourseLevels,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_profiles"]/summary' />
    [Display(Name = nameof(Dictionary.course_profiles), ResourceType = typeof(Dictionary))]
    CourseProfiles,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_current_statuses"]/summary' />
    [Display(Name = nameof(Dictionary.course_current_statuses), ResourceType = typeof(Dictionary))]
    CourseCurrentStatuses,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_legal_basis_types"]/summary' />
    [Display(Name = nameof(Dictionary.course_legal_basis_types), ResourceType = typeof(Dictionary))]
    CourseLegalBasisTypes,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_professional_titles"]/summary' />
    [Display(Name = nameof(Dictionary.course_professional_titles), ResourceType = typeof(Dictionary))]
    CourseProfessionalTitles,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_instance_statuses"]/summary' />
    [Display(Name = nameof(Dictionary.course_instance_statuses), ResourceType = typeof(Dictionary))]
    CourseInstanceStatuses,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_instance_forms"]/summary' />
    [Display(Name = nameof(Dictionary.course_instance_forms), ResourceType = typeof(Dictionary))]
    CourseInstanceForms,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_philological_languages"]/summary' />
    [Display(Name = nameof(Dictionary.course_philological_languages), ResourceType = typeof(Dictionary))]
    CoursePhilologicalLanguages,

    /// <include file='Dictionary.xml' path='docs/members/member[@name="course_main_institution_kinds"]/summary' />
    [Display(Name = nameof(Dictionary.course_main_institution_kinds), ResourceType = typeof(Dictionary))]
    CourseMainInstitutionKinds
}
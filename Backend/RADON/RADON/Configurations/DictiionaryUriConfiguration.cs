using RADON.Base;

namespace RADON.Configurations;

internal abstract record DictiionaryUriConfiguration(string uri) : BaseUriConfiguration(new Uri(uri));

// --- INSTITUTION DICTIONARIES ---
internal sealed record DictiionaryInstitutionKindsUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.INSTITUTION_KINDS);
internal sealed record DictiionaryInstitutionStatusesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.INSTITUTION_STATUSES);
internal sealed record DictiionaryInstitutionUniversityTypesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.INSTITUTION_UNIVERSITY_TYPES);
internal sealed record DictiionaryInstitutionScientificInstitutionTypesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.INSTITUTION_SCIENTIFIC_INSTITUTION_TYPES);

// --- SHARED DICTIONARIES ---
internal sealed record DictiionarySharedVoivodeshipsUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.SHARED_VOIVODESHIPS);
internal sealed record DictiionarySharedSupervisingInstitutionsUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.SHARED_SUPERVISING_INSTITUTIONS);
internal sealed record DictiionarySharedDisciplinesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.SHARED_DISCIPLINES);
internal sealed record DictiionarySharedDomainsUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.SHARED_DOMAINS);

// --- COURSE DICTIONARIES ---
internal sealed record DictiionaryCourseLevelsUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_LEVELS);
internal sealed record DictiionaryCourseProfilesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_PROFILES);
internal sealed record DictiionaryCourseCurrentStatusesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_CURRENT_STATUSES);
internal sealed record DictiionaryCourseLegalBasisTypesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_LEGAL_BASIS_TYPES);
internal sealed record DictiionaryCourseProfessionalTitlesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_PROFESSIONAL_TITLES);
internal sealed record DictiionaryCourseInstanceStatusesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_INSTANCE_STATUSES);
internal sealed record DictiionaryCourseInstanceFormsUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_INSTANCE_FORMS);
internal sealed record DictiionaryCoursePhilologicalLanguagesUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_PHILOLOGICAL_LANGUAGES);
internal sealed record DictiionaryCourseMainInstitutionKindsUriConfiguration() : DictiionaryUriConfiguration(DictionaryUris.COURSE_MAIN_INSTITUTION_KINDS);
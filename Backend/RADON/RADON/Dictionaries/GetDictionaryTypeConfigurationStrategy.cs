using RADON.Configurations;

namespace RADON.Dictionaries;

internal interface IGetDictionaryTypeConfigurationStrategy
{
    Type Execute(DictionaryType type);
}

internal class GetDictionaryTypeConfigurationStrategy() : IGetDictionaryTypeConfigurationStrategy
{
    public Type Execute(DictionaryType type) => type switch
    {
        // --- INSTITUTION ---
        DictionaryType.InstitutionKinds => typeof(DictiionaryInstitutionKindsUriConfiguration),
        DictionaryType.InstitutionStatuses => typeof(DictiionaryInstitutionStatusesUriConfiguration),
        DictionaryType.InstitutionUniversityTypes => typeof(DictiionaryInstitutionUniversityTypesUriConfiguration),
        DictionaryType.InstitutionScientificInstitutionTypes => typeof(DictiionaryInstitutionScientificInstitutionTypesUriConfiguration),

        // --- SHARED ---
        DictionaryType.SharedVoivodeships => typeof(DictiionarySharedVoivodeshipsUriConfiguration),
        DictionaryType.SharedSupervisingInstitutions => typeof(DictiionarySharedSupervisingInstitutionsUriConfiguration),
        DictionaryType.SharedDisciplines => typeof(DictiionarySharedDisciplinesUriConfiguration),
        DictionaryType.SharedDomains => typeof(DictiionarySharedDomainsUriConfiguration),

        // --- COURSE ---
        DictionaryType.CourseLevels => typeof(DictiionaryCourseLevelsUriConfiguration),
        DictionaryType.CourseProfiles => typeof(DictiionaryCourseProfilesUriConfiguration),
        DictionaryType.CourseCurrentStatuses => typeof(DictiionaryCourseCurrentStatusesUriConfiguration),
        DictionaryType.CourseLegalBasisTypes => typeof(DictiionaryCourseLegalBasisTypesUriConfiguration),
        DictionaryType.CourseProfessionalTitles => typeof(DictiionaryCourseProfessionalTitlesUriConfiguration),
        DictionaryType.CourseInstanceStatuses => typeof(DictiionaryCourseInstanceStatusesUriConfiguration),
        DictionaryType.CourseInstanceForms => typeof(DictiionaryCourseInstanceFormsUriConfiguration),
        DictionaryType.CoursePhilologicalLanguages => typeof(DictiionaryCoursePhilologicalLanguagesUriConfiguration),
        DictionaryType.CourseMainInstitutionKinds => typeof(DictiionaryCourseMainInstitutionKindsUriConfiguration),

        _ => throw new NotImplementedException(type.ToString()),
    };
}
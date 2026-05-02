using RADON.Configurations;
using RADON.Contracts.Dictionaries;

namespace RADON.Dictionaries;

internal interface IGetDictionaryResourceConfigurationStrategy
{
    Type Execute(DictionaryResource resource);
}

internal class GetDictionaryResourceConfigurationStrategy() : IGetDictionaryResourceConfigurationStrategy
{
    public Type Execute(DictionaryResource resource) => resource switch
    {
        // --- INSTITUTION ---
        DictionaryResource.InstitutionKinds => typeof(DictiionaryInstitutionKindsUriConfiguration),
        DictionaryResource.InstitutionStatuses => typeof(DictiionaryInstitutionStatusesUriConfiguration),
        DictionaryResource.InstitutionUniversityTypes => typeof(DictiionaryInstitutionUniversityTypesUriConfiguration),
        DictionaryResource.InstitutionScientificInstitutionTypes => typeof(DictiionaryInstitutionScientificInstitutionTypesUriConfiguration),

        // --- SHARED ---
        DictionaryResource.SharedVoivodeships => typeof(DictiionarySharedVoivodeshipsUriConfiguration),
        DictionaryResource.SharedSupervisingInstitutions => typeof(DictiionarySharedSupervisingInstitutionsUriConfiguration),
        DictionaryResource.SharedDisciplines => typeof(DictiionarySharedDisciplinesUriConfiguration),
        DictionaryResource.SharedDomains => typeof(DictiionarySharedDomainsUriConfiguration),

        // --- COURSE ---
        DictionaryResource.CourseLevels => typeof(DictiionaryCourseLevelsUriConfiguration),
        DictionaryResource.CourseProfiles => typeof(DictiionaryCourseProfilesUriConfiguration),
        DictionaryResource.CourseCurrentStatuses => typeof(DictiionaryCourseCurrentStatusesUriConfiguration),
        DictionaryResource.CourseLegalBasisTypes => typeof(DictiionaryCourseLegalBasisTypesUriConfiguration),
        DictionaryResource.CourseProfessionalTitles => typeof(DictiionaryCourseProfessionalTitlesUriConfiguration),
        DictionaryResource.CourseInstanceStatuses => typeof(DictiionaryCourseInstanceStatusesUriConfiguration),
        DictionaryResource.CourseInstanceForms => typeof(DictiionaryCourseInstanceFormsUriConfiguration),
        DictionaryResource.CoursePhilologicalLanguages => typeof(DictiionaryCoursePhilologicalLanguagesUriConfiguration),
        DictionaryResource.CourseMainInstitutionKinds => typeof(DictiionaryCourseMainInstitutionKindsUriConfiguration),

        _ => throw new NotImplementedException(resource.ToString()),
    };
}
using Quartz;
using RADON.Application.Interfaces.Courses;
using RADON.Contracts.Dictionaries;
using RADON.Infrastructure.Jobs.Base;

namespace RADON.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class UpdateCourseFormJob(
    ICourseFormRespository respository,
    IRadonService radonService
) : UpdateDictionaryDataJob(respository, radonService, DictionaryResource.CourseInstanceForms);

[DisallowConcurrentExecution]
public class UpdateCourseInstanceStatusJob(
    ICourseInstanceStatusRespository respository,
    IRadonService radonService
) : UpdateDictionaryDataJob(respository, radonService, DictionaryResource.CourseInstanceStatuses);

[DisallowConcurrentExecution]
public class UpdateCourseLevelJob(
    ICourseLevelRespository respository,
    IRadonService radonService
) : UpdateDictionaryDataJob(respository, radonService, DictionaryResource.CourseLevels);

[DisallowConcurrentExecution]
public class UpdateCourseProfileJob(
    ICourseProfileRespository respository,
    IRadonService radonService
) : UpdateDictionaryDataJob(respository, radonService, DictionaryResource.CourseProfiles);

[DisallowConcurrentExecution]
public class UpdateCourseStatusJob(
    ICourseStatusRespository respository,
    IRadonService radonService
) : UpdateDictionaryDataJob(respository, radonService, DictionaryResource.CourseCurrentStatuses);

[DisallowConcurrentExecution]
public class UpdateLanguageJob(
    ILanguageRespository respository,
    IRadonService radonService
) : UpdateDictionaryDataJob(respository, radonService, DictionaryResource.CoursePhilologicalLanguages);

[DisallowConcurrentExecution]
public class UpdateProfessionalTitleJob(
    IProfessionalTitleRespository respository,
    IRadonService radonService
) : UpdateDictionaryDataJob(respository, radonService, DictionaryResource.CourseProfessionalTitles);
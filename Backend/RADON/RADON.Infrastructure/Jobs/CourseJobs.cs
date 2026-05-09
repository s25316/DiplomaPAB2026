using Quartz;
using RADON.Application.Interfaces.Courses;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Contracts.Dictionaries;
using RADON.Infrastructure.Jobs.Base;
using RADON.Models.Courses.Responses;
using RADON.Models.Dictionaries.Responses;
using CourseQueryParameters = RADON.Contracts.Courses.QueryParameters;

namespace RADON.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class UpdateCourseFormJob(
    ICourseFormRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.CourseInstanceForms);

[DisallowConcurrentExecution]
public class UpdateCourseInstanceStatusJob(
    ICourseInstanceStatusRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.CourseInstanceStatuses);

[DisallowConcurrentExecution]
public class UpdateCourseLevelJob(
    ICourseLevelRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.CourseLevels);

[DisallowConcurrentExecution]
public class UpdateCourseProfileJob(
    ICourseProfileRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.CourseProfiles);

[DisallowConcurrentExecution]
public class UpdateCourseStatusJob(
    ICourseStatusRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.CourseCurrentStatuses);

[DisallowConcurrentExecution]
public class UpdateLanguageJob(
    ILanguageRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.CoursePhilologicalLanguages);

[DisallowConcurrentExecution]
public class UpdateProfessionalTitleJob(
    IProfessionalTitleRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.CourseProfessionalTitles);


[DisallowConcurrentExecution]
public class UpdateCourseJob(
    ICourseRepository repository,
    IRadonService radonService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        string? token = null;
        int totalCount = 0;
        int actualCount = 0;

        do
        {
            var queryParameters = new CourseQueryParameters
            {
                Token = token,
                ResultNumbers = 100,
            };
            var response = await radonService.GetCoursesAsync(queryParameters);
            var items = response.Results.Select(i => new Course
            {
                CourseUuid = i.CourseUuid,
                Name = i.CourseName,

                CreationDate = i.CreationDate,
                TerminationInitializationDate = i.TerminationInitializationDate,
                LiquidationDate = i.LiquidationDate,

                TeacherTraining = i.TeacherTraining,
                Philological = i.Philological,

                InstitutionUuid = i.MainInstitutionUuid,

                SourceLastRefresh = i.LastRefresh,
                DataSource = i.DataSource,

                CourseLevel = new DictionaryItem { Code = i.LevelCode, Name = i.LevelName },
                CourseProfile = new DictionaryItem { Code = i.ProfileCode, Name = i.ProfileName },
                Isced = new DictionaryItem { Code = i.IscedCode, Name = i.IscedName },
                CourseStatus = new DictionaryItem { Code = i.CurrentStatusCode, Name = i.CurrentStatusName },

                Disciplines = i.Disciplines.Select(d => new Course.DisciplineData
                {
                    Discipline = new DictionaryItem { Code = d.DisciplineCode, Name = d.DisciplineName },
                    Percentage = d.DisciplinePercentageShare,
                    IsLeading = d.DisciplineLeading,
                }).ToList(),

                CourseInstances = i.CourseInstances.Select(ci => new CourseInstance
                {
                    CourseInstanceUuid = ci.CourseInstanceUuid,
                    Name = ci.CourseName,

                    EducationStartDate = ci.EducationStartDate,
                    LiquidationDate = ci.LiquidationDate,

                    NumberOfSemesters = ci.NumberOfSemesters,
                    Ects = ci.Ects,

                    Dual = ci.Dual,
                    Bridging = ci.Bridging,
                    CoopWithVocational = ci.CoopWithVocational,

                    CourseForm = new DictionaryItem { Code = ci.FormCode, Name = ci.FormName },
                    ProfessionalTitle = new DictionaryItem { Code = ci.TitleCode, Name = ci.TitleName },
                    Language = new DictionaryItem { Code = ci.LanguageCode, Name = ci.LanguageName },
                    CourseInstanceStatus = new DictionaryItem { Code = ci.StatusCode, Name = ci.StatusName },

                    PhilologicalLanguages = ci.PhilologicalLanguages
                    .Select(l => new DictionaryItem { Code = l.LanguageCode, Name = l.LanguageName })
                    .ToList(),
                }).ToList(),
            });

            await repository.CreateOrUpdateAsync(items);

            token = response.Pagination.Token;
            totalCount = response.Pagination.MaxCount;
            actualCount += items.Count();
        }
        while (totalCount != actualCount && !string.IsNullOrWhiteSpace(token));
    }
}
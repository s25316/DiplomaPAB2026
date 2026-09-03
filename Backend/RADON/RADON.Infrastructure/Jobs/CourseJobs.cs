using Quartz;
using RADON.Application.Interfaces;
using RADON.Application.Interfaces.Courses;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Application.Interfaces.Institutions;
using RADON.Contracts.Dictionaries;
using RADON.Infrastructure.Jobs.Base;
using RADON.Models.Courses.Responses;
using RADON.Models.Dictionaries.Responses;
using CourseQueryParameters = RADON.Contracts.Courses.QueryParameters;

namespace RADON.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class UpdateCourseFormJob(
    ICourseFormRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.CourseInstanceForms);

[DisallowConcurrentExecution]
public class UpdateCourseInstanceStatusJob(
    ICourseInstanceStatusRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.CourseInstanceStatuses);

[DisallowConcurrentExecution]
public class UpdateCourseLevelJob(
    ICourseLevelRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.CourseLevels);

[DisallowConcurrentExecution]
public class UpdateCourseProfileJob(
    ICourseProfileRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.CourseProfiles);

[DisallowConcurrentExecution]
public class UpdateCourseStatusJob(
    ICourseStatusRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.CourseCurrentStatuses);

[DisallowConcurrentExecution]
public class UpdateLanguageJob(
    ILanguageRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.CoursePhilologicalLanguages);

[DisallowConcurrentExecution]
public class UpdateProfessionalTitleJob(
    IProfessionalTitleRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.CourseProfessionalTitles);


[DisallowConcurrentExecution]
public class UpdateCourseJob(
    ICourseRepository repository,
    IInstitutionRepository institutionRepository,
    IRadonService radonService,
    IErrorLogger errorLogger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var institutions = await institutionRepository.GetAllAsync();
            var institutionDictionary = institutions.ToDictionary(k => k.InstitutionUuid);
            var ids = new HashSet<Guid>();
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
                var items = new List<Course>();
                var results = response.Results;


                foreach (var i in results)
                {
                    if (ids.Contains(i.CourseUuid))
                        continue;

                    if (!institutionDictionary.ContainsKey(i.MainInstitutionUuid))
                        continue;

                    ids.Add(i.CourseUuid);
                    items.Add(new Course
                    {
                        CourseUuid = i.CourseUuid,
                        Name = i.CourseName,

                        CreationDate = i.CreationDate,
                        TerminationInitializationDate = i.TerminationInitializationDate,
                        LiquidationDate = i.LiquidationDate,

                        IsTeacherTraining = i.TeacherTraining,
                        IsPhilological = i.Philological,

                        InstitutionUuid = i.MainInstitutionUuid,

                        LastRefresh = i.LastRefresh,
                        SourceLastRefresh = i.LastRefresh,
                        DataSource = i.DataSource,

                        Level = new DictionaryItem { Code = i.LevelCode, Name = i.LevelName },
                        Profile = new DictionaryItem { Code = i.ProfileCode, Name = i.ProfileName },
                        Isced = new DictionaryItem { Code = i.IscedCode, Name = i.IscedName },
                        Status = new DictionaryItem { Code = i.CurrentStatusCode, Name = i.CurrentStatusName },

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

                            IsDual = ci.Dual,
                            IsBridging = ci.Bridging,
                            IsCoopWithVocational = ci.CoopWithVocational,

                            Form = new DictionaryItem { Code = ci.FormCode, Name = ci.FormName },
                            ProfessionalTitle = new DictionaryItem { Code = ci.TitleCode, Name = ci.TitleName },
                            Language = new DictionaryItem { Code = ci.LanguageCode, Name = ci.LanguageName },
                            Status = new DictionaryItem { Code = ci.StatusCode, Name = ci.StatusName },

                            PhilologicalLanguages = ci.PhilologicalLanguages
                            .Select(l => new DictionaryItem { Code = l.LanguageCode, Name = l.LanguageName })
                            .ToList(),
                        }).ToList(),
                    });

                }

                await repository.CreateOrUpdateAsync(items);

                token = response.Pagination.Token;
                totalCount = response.Pagination.MaxCount;
                actualCount += items.Count();
            }
            while (totalCount != actualCount && !string.IsNullOrWhiteSpace(token));
        }
        catch (Exception ex)
        {
            await errorLogger.LogErrorAsync(ex);
        }
    }
}
using Diploma.Application.Interfaces.Database;
using Diploma.Database;
using Diploma.Database.Models.Educations;
using Diploma.Infrastructure.EducationCourses.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using DatabaseEducationCourse = Diploma.Database.Models.Educations.EducationCourse;
using DatabaseEducationCourseInstance = Diploma.Database.Models.Educations.EducationCourseInstance;
using InputEducationCourse = Diploma.Infrastructure.EducationCourses.Services.EducationCourse;

namespace Diploma.Infrastructure.Jobs.Educations;

[DisallowConcurrentExecution]
public class EducationCouseJobs(
    DiplomaDbContext databaseContext,
    IUnitOfWorkFactory unitOfWorkFactory,
    IEducationCouseService service
    ) : IJob
{
    private const int TAKING_ITEMS = 1000;

    public async Task Execute(IJobExecutionContext context)
    {
        var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var enumerator = service.GetAsync().GetAsyncEnumerator();


        await databaseContext.EducationCourseDisciplines.ExecuteDeleteAsync();

        var isEmpty = false;
        while (!isEmpty)
        {
            var inputDictionary = await GetDictionaryAsync(enumerator);
            isEmpty = inputDictionary.Count != TAKING_ITEMS;

            await CreateOrUpdateCoursesAsync(inputDictionary);
            await CreateOrUpdateCourseInstancesAsync(inputDictionary);

            inputDictionary.Clear();
            await databaseContext.SaveChangesAsync();
        }

        await enumerator.DisposeAsync();
        await databaseContext.SaveChangesAsync();
        await unitOfWork.CommitAsync();
    }

    private static async Task<Dictionary<Guid, InputEducationCourse>> GetDictionaryAsync(
        IAsyncEnumerator<InputEducationCourse> enumerator)
    {
        var inputDictionary = new Dictionary<Guid, InputEducationCourse>();

        while (inputDictionary.Count < TAKING_ITEMS && await enumerator.MoveNextAsync())
        {
            var inputItem = enumerator.Current;
            inputDictionary[inputItem.CourseUuid] = inputItem;
        }

        return inputDictionary;
    }

    private async Task CreateOrUpdateCoursesAsync(Dictionary<Guid, InputEducationCourse> dictionary)
    {
        var inputKeys = dictionary.Keys.ToHashSet();

        var databaseDictionary = await databaseContext
            .EducationCourses
            .Where(i => inputKeys.Contains(i.EducationCourseId))
            .ToDictionaryAsync(k => k.EducationCourseId);

        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var existingKeys = inputKeys.Intersect(databaseKeys);
        var notExistingKeys = inputKeys.Except(databaseKeys);

        foreach (var key in existingKeys)
        {
            var databaseItem = databaseDictionary[key];
            var inputItem = dictionary[key];

            databaseItem.CreationDate = inputItem.CreationDate;
            databaseItem.TerminationInitializationDate = inputItem.TerminationInitializationDate;
            databaseItem.LiquidationDate = inputItem.LiquidationDate;
            databaseItem.EducationInstitutionId = inputItem.InstitutionUuid;

            var courseDisciplines = inputItem
                .Disciplines
                .Select(i => new EducationCourseDiscipline
                {
                    EducationCourse = databaseItem,
                    IsLeading = i.IsLeading,
                    Percentage = i.Percentage,
                    EducationDisciplineCode = i.Discipline.Code,
                });
            await databaseContext.EducationCourseDisciplines.AddRangeAsync(courseDisciplines);
        }

        foreach (var key in notExistingKeys)
        {
            var inputItem = dictionary[key];
            var databaseItem = new DatabaseEducationCourse
            {
                EducationCourseId = inputItem.CourseUuid,
                CreationDate = inputItem.CreationDate,
                TerminationInitializationDate = inputItem.TerminationInitializationDate,
                LiquidationDate = inputItem.LiquidationDate,
                EducationInstitutionId = inputItem.InstitutionUuid,
            };

            var courseDisciplines = inputItem
                .Disciplines
                .Select(i => new EducationCourseDiscipline
                {
                    EducationCourse = databaseItem,
                    IsLeading = i.IsLeading,
                    Percentage = i.Percentage,
                    EducationDisciplineCode = i.Discipline.Code,
                });
            await databaseContext.EducationCourses.AddAsync(databaseItem);
            await databaseContext.EducationCourseDisciplines.AddRangeAsync(courseDisciplines);
        }
    }

    private async Task CreateOrUpdateCourseInstancesAsync(Dictionary<Guid, InputEducationCourse> courseDictionary)
    {
        var instanceDictionary = courseDictionary
            .Values
            .SelectMany(i => i.CourseInstances.Select(ci => new
            {
                CourseInstanceUuid = ci.CourseInstanceUuid,
                CourseId = i.CourseUuid,
                CourseInstance = ci,
            }))
            .ToDictionary(i => i.CourseInstanceUuid);
        var inputKeys = instanceDictionary.Keys.ToHashSet();

        var databaseDictionary = await databaseContext
            .EducationCourseInstances
            .Where(i => inputKeys.Contains(i.EducationCourseInstanceId))
            .ToDictionaryAsync(k => k.EducationCourseInstanceId);

        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var existingKeys = inputKeys.Intersect(databaseKeys);
        var notExistingKeys = inputKeys.Except(databaseKeys);

        foreach (var key in existingKeys)
        {
            var databaseItem = databaseDictionary[key];
            var inputItem = instanceDictionary[key];

            databaseItem.EducationStartDate = inputItem.CourseInstance.EducationStartDate;
            databaseItem.LiquidationDate = inputItem.CourseInstance.LiquidationDate;
            databaseItem.EducationCourseId = inputItem.CourseId;
        }

        foreach (var key in notExistingKeys)
        {
            var inputItem = instanceDictionary[key];
            var databaseItem = new DatabaseEducationCourseInstance
            {
                EducationCourseInstanceId = inputItem.CourseInstance.CourseInstanceUuid,
                EducationCourseId = inputItem.CourseId,
                EducationStartDate = inputItem.CourseInstance.EducationStartDate,
                LiquidationDate = inputItem.CourseInstance.LiquidationDate,
            };

            await databaseContext.EducationCourseInstances.AddAsync(databaseItem);
        }
    }
}
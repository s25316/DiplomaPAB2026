using Microsoft.EntityFrameworkCore;
using RADON.Application.Interfaces.Courses;
using RADON.Database;
using RADON.Database.Models;
using RADON.Database.Models.Courses;
using RADON.Database.Models.Shared;
using RADON.Infrastructure.QueryBuilders;
using RADON.Models.Courses;
using RADON.Models.Dictionaries.Responses;
using RADON.Models.Shared;
using DatabaseCourse = RADON.Database.Models.Courses.Course;
using DatabaseCourseInstance = RADON.Database.Models.Courses.CourseInstance;
using ResponseCourse = RADON.Models.Courses.Responses.Course;
using ResponseCourseInstance = RADON.Models.Courses.Responses.CourseInstance;

namespace RADON.Infrastructure.Repositories.Courses;

public class CourseRepository(
    RadonDbContext context,
    CourseQueryBuilder queryBuilder) : ICourseRepository
{

    public async Task<Response<ResponseCourse>> GetAsync(
        QueryParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var baseQueryBuilder = queryBuilder
            .WithCourseUuids(parameters.CourseUuids)
            .WithInstitutionUuids(parameters.InstitutionUuids)
            .WithName(parameters.Name)
            .WithIsTeacherTraining(parameters.IsTeacherTraining)
            .WithIsPhilological(parameters.IsPhilological)
            .WithLevelCodes(parameters.LevelCodes)
            .WithProfileCodes(parameters.ProfileCodes)
            .WithIscedCodes(parameters.IscedCodes)
            .WithStatusCodes(parameters.StatusCodes)
            .WithDisciplineCodes(parameters.DisciplineCodes)

            .WithCourseInstanceUuids(parameters.CourseInstanceUuids)
            .WithIsDual(parameters.IsDual)
            .WithIsBridging(parameters.IsBridging)
            .WithIsCoopWithVocational(parameters.IsCoopWithVocational)
            .WithFormCodes(parameters.FormCodes)
            .WithProfessionalTitleCodes(parameters.ProfessionalTitleCodes)
            .WithLanguageCodes(parameters.LanguageCodes)
            .WithInstanceStatusCodes(parameters.InstanceStatusCodes)
            .WithPhilologicalLanguageCodes(parameters.PhilologicalLanguageCodes);


        var baseQuery = queryBuilder.Build();
        var query = baseQueryBuilder
            .WithOrderBy(parameters.OrderBy, parameters.Order, parameters.Pagination)
            .Build();

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var dbItems = await query.ToListAsync(cancellationToken);

        return new Response<ResponseCourse>
        {
            Pagination = new ResponsePagination
            {
                Page = parameters.Pagination.Page,
                ItemsPerPage = parameters.Pagination.ItemsPerPage,
                TotalCount = totalCount,
            },

            Items = dbItems.Select(i => new ResponseCourse
            {
                CourseUuid = i.CourseUuid,
                Name = i.Name,

                InstitutionUuid = i.InstitutionUuid,

                CreationDate = i.CreationDate,
                TerminationInitializationDate = i.TerminationInitializationDate,
                LiquidationDate = i.LiquidationDate,

                IsTeacherTraining = i.IsTeacherTraining,
                IsPhilological = i.IsPhilological,

                Level = new DictionaryItem
                {
                    Code = i.CourseLevel.CourseLevelCode,
                    Name = i.CourseLevel.Name,
                },

                Profile = new DictionaryItem
                {
                    Code = i.CourseProfile.CourseProfileCode,
                    Name = i.CourseProfile.Name,
                },

                Isced = new DictionaryItem
                {
                    Code = i.Isced.IscedCode,
                    Name = i.Isced.Name,
                },

                Status = new DictionaryItem
                {
                    Code = i.CourseStatus.CourseStatusCode,
                    Name = i.CourseStatus.Name,
                },

                Disciplines = i.Disciplines.Select(d => new ResponseCourse.DisciplineData
                {
                    Discipline = new DictionaryItem
                    {
                        Code = d.Discipline.DisciplineCode,
                        Name = d.Discipline.Name,
                    },
                    Percentage = d.PercentageShare,
                    IsLeading = d.Leading,
                }).ToList(),

                CourseInstances = i.CourseInstances.Select(ci => new ResponseCourseInstance
                {
                    CourseInstanceUuid = ci.CourseInstanceUuid,
                    Name = ci.Name,

                    EducationStartDate = ci.EducationStartDate,
                    LiquidationDate = ci.LiquidationDate,

                    NumberOfSemesters = ci.NumberOfSemesters,
                    Ects = ci.Ects,

                    IsDual = ci.IsDual,
                    IsBridging = ci.IsBridging,
                    IsCoopWithVocational = ci.IsCoopWithVocational,

                    Form = new DictionaryItem
                    {
                        Code = ci.CourseForm.CourseFormCode,
                        Name = ci.CourseForm.Name,
                    },

                    ProfessionalTitle = new DictionaryItem
                    {
                        Code = ci.ProfessionalTitle.ProfessionalTitleCode,
                        Name = ci.ProfessionalTitle.Name,
                    },

                    Language = new DictionaryItem
                    {
                        Code = ci.Language.LanguageCode,
                        Name = ci.Language.Name,
                    },

                    Status = new DictionaryItem
                    {
                        Code = ci.CourseInstanceStatus.CourseInstanceStatusCode,
                        Name = ci.CourseInstanceStatus.Name,
                    },

                    PhilologicalLanguages = ci.PhilologicalLanguages.Select(l => new DictionaryItem
                    {
                        Code = l.LanguageCode,
                        Name = l.Name,
                    }).ToList(),
                }).ToList(),

                LastRefresh = i.LastRefresh,
                SourceLastRefresh = i.SourceLastRefresh,
                DataSource = i.DataSource.Name,

            }).ToList(),
        };
    }

    public async Task CreateOrUpdateAsync(IEnumerable<ResponseCourse> items, CancellationToken cancellationToken = default)
    {
        if (!items.Any())
            return;

        var institutionIds = await context.Institutions.Select(i => i.InstitutionUuid).ToHashSetAsync(cancellationToken);
        var institutionIds2 = items
            .Select(i => i.InstitutionUuid)
            .ToHashSet();

        var notExisting = institutionIds2.Except(institutionIds).ToList();


        var currentTime = DateTimeOffset.Now;
        var dataSourceDictionary = await GetDataSourcesAsync(items, cancellationToken);
        var languageDictionary = await GetLanguagesAsync(items, cancellationToken);
        await GetIscedsAsync(items, cancellationToken);
        await GetDisciplinesAsync(items, cancellationToken);


        var inputDictionary = items
            .GroupBy(k => k.CourseUuid)
            .ToDictionary(k => k.Key, v => v.First());
        var inputKeys = inputDictionary.Keys.ToHashSet();

        var databaseDictionary = await context
            .Courses

            .Include(i => i.CourseInstances)
            .ThenInclude(i => i.PhilologicalLanguages)

            .Include(i => i.Disciplines)
            .ThenInclude(i => i.Discipline)

            .Where(i => inputKeys.Contains(i.CourseUuid))
            .ToDictionaryAsync(k => k.CourseUuid, cancellationToken);
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var existingKeys = databaseKeys.Intersect(inputKeys);
        var newKeys = inputKeys.Except(databaseKeys);

        foreach (var key in newKeys)
        {
            var input = inputDictionary[key];
            var database = await CreateOrUpdateAsync(
                input,
                null,
                currentTime,
                dataSourceDictionary,
                languageDictionary,
                cancellationToken);
            await context.Courses.AddAsync(database, cancellationToken);
        }

        foreach (var key in existingKeys)
        {
            var database = databaseDictionary[key];
            var input = inputDictionary[key];
            await CreateOrUpdateAsync(
                input,
                database,
                currentTime,
                dataSourceDictionary,
                languageDictionary,
                cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Dictionary<string, DataSource>> GetDataSourcesAsync(
        IEnumerable<ResponseCourse> items,
        CancellationToken cancellationToken = default)
    {
        var databaseDictionary = await context
            .DataSources
            .ToDictionaryAsync(k => k.Name.ToUpperInvariant(), cancellationToken);

        var databaseDataSources = databaseDictionary.Keys.ToHashSet();
        var inputDataSources = items
            .Select(i => i.DataSource.ToUpperInvariant())
            .ToHashSet();

        var newDataSources = inputDataSources.Except(databaseDataSources);

        if (!newDataSources.Any())
            return databaseDictionary;

        foreach (var newDataSource in newDataSources)
        {
            var databaseItem = new DataSource
            {
                Name = newDataSource,
            };
            databaseDictionary[databaseItem.Name] = databaseItem;
            await context.DataSources.AddAsync(databaseItem, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
        return databaseDictionary;
    }

    private async Task<Dictionary<string, TDatabase>> GetDictionariesAsync<TInput, TDatabase>(
        IEnumerable<TInput> items,
        Func<CancellationToken, Task<Dictionary<string, TDatabase>>> getDatabaseDictionaryFunc,
        Func<IEnumerable<TInput>, Dictionary<string, DictionaryItem>> getInputDictionaryFunc,
        Func<DictionaryItem, TDatabase> mapFunc,
        Func<RadonDbContext, DbSet<TDatabase>> getDbSetFunc,
        CancellationToken cancellationToken = default)
        where TDatabase : class
    {
        var inputDictionary = getInputDictionaryFunc(items);
        var databaseDictionary = await getDatabaseDictionaryFunc(cancellationToken);

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var newKeys = inputKeys.Except(databaseKeys);

        if (!newKeys.Any())
            return databaseDictionary;

        foreach (var newKey in newKeys)
        {
            var newItem = inputDictionary[newKey];
            var databaseItem = mapFunc(newItem);
            databaseDictionary[newKey] = databaseItem;
            await getDbSetFunc(context).AddAsync(databaseItem, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
        return databaseDictionary;
    }

    private async Task<Dictionary<string, Isced>> GetIscedsAsync(
        IEnumerable<ResponseCourse> items,
        CancellationToken cancellationToken = default) => await GetDictionariesAsync(
            items,
            cancellationToken => context.Isceds.ToDictionaryAsync(k => k.IscedCode, cancellationToken),
            input => input.Select(i => i.Isced).ToHashSet().ToDictionary(i => i.Code),
            item => new Isced { IscedCode = item.Code, Name = item.Name },
            context => context.Isceds,
            cancellationToken);

    private async Task<Dictionary<string, Discipline>> GetDisciplinesAsync(
        IEnumerable<ResponseCourse> items,
        CancellationToken cancellationToken = default) => await GetDictionariesAsync(
            items,
            cancellationToken => context.Disciplines.ToDictionaryAsync(k => k.DisciplineCode, cancellationToken),
            input => input.SelectMany(i => i.Disciplines.Select(d => d.Discipline)).ToHashSet().ToDictionary(i => i.Code),
            item => new Discipline { DisciplineCode = item.Code, Name = item.Name },
            context => context.Disciplines,
            cancellationToken);

    private async Task<Dictionary<string, Language>> GetLanguagesAsync(
        IEnumerable<ResponseCourse> items,
        CancellationToken cancellationToken = default) => await GetDictionariesAsync(
            items,
            cancellationToken => context.Languages.ToDictionaryAsync(k => k.LanguageCode, cancellationToken),
            input => input.SelectMany(i => i.CourseInstances.SelectMany(d => d.PhilologicalLanguages.Concat([d.Language]))).ToHashSet().ToDictionary(i => i.Code),
            item => new Language { LanguageCode = item.Code, Name = item.Name },
            context => context.Languages,
            cancellationToken);

    private async Task<DatabaseCourse> CreateOrUpdateAsync(
        ResponseCourse item,
        DatabaseCourse? database,
        DateTimeOffset currentTime,
        Dictionary<string, DataSource> dataSourceDictionary,
        Dictionary<string, Language> languageDictionary,
        CancellationToken cancellationToken = default)
    {
        database ??= new DatabaseCourse { CourseUuid = item.CourseUuid };

        database.Name = item.Name;

        database.CreationDate = item.CreationDate;
        database.TerminationInitializationDate = item.TerminationInitializationDate;
        database.LiquidationDate = item.LiquidationDate;

        database.IsTeacherTraining = item.IsTeacherTraining;
        database.IsPhilological = item.IsPhilological;

        database.LastRefresh = currentTime;
        database.SourceLastRefresh = item.LastRefresh;
        database.DataSource = dataSourceDictionary[item.DataSource.ToUpperInvariant()];

        database.CourseLevelCode = item.Level.Code;
        database.CourseProfileCode = item.Profile.Code;
        database.CourseStatusCode = item.Status.Code;
        database.IscedCode = item.Isced.Code;

        database.InstitutionUuid = item.InstitutionUuid;

        await CreateOrUpdateDisciplinesAsync(item, database, cancellationToken);
        await CreateOrUpdateCourseInstancesAsync(item, database, languageDictionary, cancellationToken);

        return database;
    }

    private async Task CreateOrUpdateDisciplinesAsync(
        ResponseCourse item,
        DatabaseCourse database,
        CancellationToken cancellationToken = default)
    {
        var databaseDictionary = database.Disciplines.ToDictionary(k => k.DisciplineCode);
        var inputDictionary = item.Disciplines.ToDictionary(k => k.Discipline.Code);

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var newKeys = inputKeys.Except(databaseKeys);
        var existingKeys = databaseKeys.Intersect(inputKeys);
        var removedKeys = databaseKeys.Except(inputKeys);

        foreach (var key in newKeys)
        {
            var input = inputDictionary[key];
            await context.CourseDisciplines.AddAsync(new CourseDiscipline
            {
                Course = database,
                DisciplineCode = input.Discipline.Code,
                PercentageShare = input.Percentage,
                Leading = input.IsLeading
            }, cancellationToken);
        }

        foreach (var key in existingKeys)
        {
            var inputItem = inputDictionary[key];
            var databaseItem = databaseDictionary[key];
            databaseItem.PercentageShare = inputItem.Percentage;
            databaseItem.Leading = inputItem.IsLeading;
        }

        foreach (var key in removedKeys)
        {
            context.CourseDisciplines.Remove(databaseDictionary[key]);
        }
    }

    private async Task CreateOrUpdateCourseInstancesAsync(
        ResponseCourse item,
        DatabaseCourse database,
        Dictionary<string, Language> languageDictionary,
        CancellationToken cancellationToken = default)
    {
        var databaseDictionary = database.CourseInstances.ToDictionary(k => k.CourseInstanceUuid);
        var inputDictionary = item.CourseInstances.ToDictionary(k => k.CourseInstanceUuid);

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var newKeys = inputKeys.Except(databaseKeys);
        var existingKeys = databaseKeys.Intersect(inputKeys);
        var removedKeys = databaseKeys.Except(inputKeys);

        if (removedKeys.Any())
            throw new InvalidOperationException($"Must be removed: [{string.Join(", ", removedKeys)}]");

        foreach (var key in newKeys)
        {
            var input = inputDictionary[key];
            var databaseInstance = CreateOrUpdateCourseInstance(input, database, null, languageDictionary);
            await context.CourseInstances.AddAsync(databaseInstance, cancellationToken);
        }

        foreach (var key in existingKeys)
        {
            var input = inputDictionary[key];
            var databaseInstance = databaseDictionary[key];
            _ = CreateOrUpdateCourseInstance(input, database, databaseInstance, languageDictionary);
        }
    }

    private static DatabaseCourseInstance CreateOrUpdateCourseInstance(
        ResponseCourseInstance item,
        DatabaseCourse databaseCourse,
        DatabaseCourseInstance? database,
        Dictionary<string, Language> languageDictionary)
    {
        database ??= new DatabaseCourseInstance()
        {
            CourseInstanceUuid = item.CourseInstanceUuid,
            Course = databaseCourse,
        };

        database.Name = item.Name;

        database.EducationStartDate = item.EducationStartDate;
        database.LiquidationDate = item.LiquidationDate;

        database.NumberOfSemesters = item.NumberOfSemesters;
        database.Ects = item.Ects;

        database.IsDual = item.IsDual;
        database.IsBridging = item.IsBridging;
        database.IsCoopWithVocational = item.IsCoopWithVocational;

        database.CourseFormCode = item.Form.Code;
        database.ProfessionalTitleCode = item.ProfessionalTitle.Code;
        database.LanguageCode = item.Language.Code;
        database.CourseInstanceStatusCode = item.Status.Code;

        var databaseDictionary = database.PhilologicalLanguages.ToDictionary(k => k.LanguageCode);
        var inputDictionary = item.PhilologicalLanguages.ToDictionary(k => k.Code);

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var newKeys = inputKeys.Except(databaseKeys);
        var removedKeys = databaseKeys.Except(inputKeys);

        foreach (var key in newKeys)
        {
            database.PhilologicalLanguages.Add(languageDictionary[key]);
        }

        foreach (var key in removedKeys)
        {
            var databaseLanguage = databaseDictionary[key];
            database.PhilologicalLanguages.Remove(databaseLanguage);
        }

        return database;
    }
}
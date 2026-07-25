using Diploma.Application.Interfaces.Database;
using Diploma.Database;
using Diploma.Infrastructure.EducationDisciplines.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using EducationDiscipline = Diploma.Database.Models.Educations.EducationDiscipline;

namespace Diploma.Infrastructure.Jobs.Educations;

[DisallowConcurrentExecution]
public class EducationDisciplineJob(
    DiplomaDbContext databaseContext,
    IUnitOfWorkFactory unitOfWorkFactory,
    IEducationDisciplineService service
    ) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var unitOfWork = await unitOfWorkFactory.CreateAsync();


        var items = await service.GetAsync();
        var inputDictionary = items
            .ToDictionary(k => k.Code);

        var databaseDictionary = await databaseContext
            .EducationDisciplines
            .ToDictionaryAsync(k => k.Code);

        var inputKeys = inputDictionary.Keys.ToHashSet();
        var databaseKeys = databaseDictionary.Keys.ToHashSet();

        var existingKeys = inputKeys.Intersect(databaseKeys);
        var notExistingKeys = inputKeys.Except(databaseKeys);

        foreach (var key in existingKeys)
        {
            var databaseItem = databaseDictionary[key];
            var inputItem = inputDictionary[key];

            databaseItem.Name = inputItem.Name;
        }

        foreach (var key in notExistingKeys)
        {
            var inputItem = inputDictionary[key];
            await databaseContext.EducationDisciplines.AddAsync(new EducationDiscipline
            {
                Code = inputItem.Code,
                Name = inputItem.Name,
            });
        }

        await databaseContext.SaveChangesAsync();
        await unitOfWork.CommitAsync();
    }
}
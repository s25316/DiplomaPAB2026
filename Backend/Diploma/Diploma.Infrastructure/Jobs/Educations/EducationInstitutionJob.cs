using Diploma.Application.Interfaces.Database;
using Diploma.Database;
using Diploma.Infrastructure.EducationInstitutions.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using InputEducationInstitution = Diploma.Infrastructure.EducationInstitutions.Services.EducationInstitution;

namespace Diploma.Infrastructure.Jobs.Educations;

[DisallowConcurrentExecution]
public class EducationInstitutionJob(
    DiplomaDbContext databaseContext,
    IUnitOfWorkFactory unitOfWorkFactory,
    IEducationInstitutionService service
    ) : IJob
{
    private const int TAKING_ITEMS = 1000;

    public async Task Execute(IJobExecutionContext context)
    {
        var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var enumerator = service.GetAsync().GetAsyncEnumerator();

        var isEmpty = false;
        while (!isEmpty)
        {
            var inputDictionary = await GetDictionaryAsync(enumerator);
            isEmpty = inputDictionary.Count < TAKING_ITEMS;

            var inputKeys = inputDictionary.Keys.ToHashSet();

            var databaseDictionary = await databaseContext
                .EducationInstitutions
                .Where(i => inputKeys.Contains(i.EducationInstitutionId))
                .ToDictionaryAsync(k => k.EducationInstitutionId);

            var databaseKeys = databaseDictionary.Keys.ToHashSet();

            var existingKeys = inputKeys.Intersect(databaseKeys);
            var notExistingKeys = inputKeys.Except(databaseKeys);

            foreach (var key in existingKeys)
            {
                var databaseItem = databaseDictionary[key];
                var inputItem = inputDictionary[key];

                databaseItem.StartDate = inputItem.StartDate;
                databaseItem.LiquidationStartDate = inputItem.LiquidationStartDate;
                databaseItem.LiquidationDate = inputItem.LiquidationDate;
            }

            foreach (var key in notExistingKeys)
            {
                var inputItem = inputDictionary[key];
                await databaseContext.EducationInstitutions.AddAsync(new Database.Models.Educations.EducationInstitution
                {
                    EducationInstitutionId = inputItem.InstitutionUuid,
                    StartDate = inputItem.StartDate,
                    LiquidationStartDate = inputItem.LiquidationStartDate,
                    LiquidationDate = inputItem.LiquidationDate,
                });
            }
            await databaseContext.SaveChangesAsync();
        }

        await databaseContext.SaveChangesAsync();
        await unitOfWork.CommitAsync();
    }

    private static async Task<Dictionary<Guid, InputEducationInstitution>> GetDictionaryAsync(
        IAsyncEnumerator<InputEducationInstitution> enumerator)
    {
        var inputDictionary = new Dictionary<Guid, InputEducationInstitution>();

        while (inputDictionary.Count < TAKING_ITEMS && await enumerator.MoveNextAsync())
        {
            var inputItem = enumerator.Current;
            inputDictionary[inputItem.InstitutionUuid] = inputItem;
        }

        return inputDictionary;
    }
}
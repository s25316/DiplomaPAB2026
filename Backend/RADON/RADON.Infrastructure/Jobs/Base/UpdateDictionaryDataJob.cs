using Quartz;
using RADON.Application.Interfaces;
using RADON.Application.Interfaces.Base;
using RADON.Contracts.Dictionaries;
using RADON.Models.Dictionaries.Responses;

namespace RADON.Infrastructure.Jobs.Base;

[DisallowConcurrentExecution]
public abstract class UpdateDictionaryDataJob(
    IRadonDictionaryRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger,
    DictionaryResource dictionaryResource) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var radonData = await radonService.GetDictionariesAsync(dictionaryResource);
            var dictionaryItems = radonData.Select(i => new DictionaryItem { Code = i.Code, Name = i.NamePl });
            await repository.CreateOrUpdateAsync(dictionaryItems);
        }
        catch (Exception ex)
        {
            await errorLogger.LogErrorAsync(ex);
        }
    }
}
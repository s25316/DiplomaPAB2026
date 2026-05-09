using Quartz;
using RADON.Application.Interfaces.Base;
using RADON.Contracts.Dictionaries;
using RADON.Models.Responses.Dictionaries;

namespace RADON.Infrastructure.Jobs.Base;

[DisallowConcurrentExecution]
public abstract class UpdateDictionaryDataJob(
    IRadonDictionaryRepository repository,
    IRadonService radonService,
    DictionaryResource dictionaryResource) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var radonData = await radonService.GetDictionariesAsync(dictionaryResource);
        var dictionaryItems = radonData.Select(i => new DictionaryItem(i.Code, i.NamePl));
        await repository.CreateOrUpdateAsync(dictionaryItems);
    }
}
using Quartz;
using RADON.Application.Interfaces.Shared;
using RADON.Contracts.Dictionaries;
using RADON.Infrastructure.Jobs.Base;

namespace RADON.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class UpdateDisciplineJob(
    IDisciplineRespository respository,
    IRadonService radonService
) : UpdateDictionaryDataJob(respository, radonService, DictionaryResource.SharedDisciplines);
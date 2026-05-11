using Quartz;
using RADON.Application.Interfaces;
using RADON.Application.Interfaces.Shared.Dictionaries;
using RADON.Contracts.Dictionaries;
using RADON.Infrastructure.Jobs.Base;

namespace RADON.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class UpdateDisciplineJob(
    IDisciplineRespository respository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(respository, radonService, errorLogger, DictionaryResource.SharedDisciplines);
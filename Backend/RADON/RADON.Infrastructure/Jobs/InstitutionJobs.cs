using Base.Models.ValueObjects.Krsy;
using Base.Models.ValueObjects.Nipy;
using Base.Models.ValueObjects.Regony;
using Quartz;
using RADON.Application.Interfaces;
using RADON.Application.Interfaces.Institutions;
using RADON.Application.Interfaces.Institutions.Dictionaries;
using RADON.Contracts.Dictionaries;
using RADON.Contracts.Institutions;
using RADON.Contracts.Institutions.Responses;
using RADON.Infrastructure.Jobs.Base;
using RADON.Models.Dictionaries.Responses;
using static RADON.Models.Institutions.Responses.Institution;
using ResponseInstitution = RADON.Models.Institutions.Responses.Institution;

namespace RADON.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class UpdateInstitutionKindJob(
    IInstitutionKindRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.InstitutionKinds);

[DisallowConcurrentExecution]
public class UpdateInstitutionStatusJob(
    IInstitutionStatusRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.InstitutionStatuses);

[DisallowConcurrentExecution]
public class UpdateUniversityTypeJob(
    IUniversityTypeRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.InstitutionUniversityTypes);

[DisallowConcurrentExecution]
public class UpdateScientificInstitutionTypeJob(
    IScientificInstitutionTypeRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger
) : UpdateDictionaryDataJob(repository, radonService, errorLogger, DictionaryResource.InstitutionScientificInstitutionTypes);


[DisallowConcurrentExecution]
public class UpdateInstitutionJob(
    IInstitutionRepository repository,
    IRadonService radonService,
    IErrorLogger errorLogger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var dictionary = new Dictionary<Guid, InstitutionReport>();
            int allItemsCount;
            string? token = null;
            int lastTotalCount = 0;

            do
            {
                var response = await radonService.GetInstitutionsAsync(new QueryParameters() { Token = token });
                allItemsCount = response.Pagination.MaxCount;
                var results = response.Results;

                foreach (var item in results)
                    dictionary[item.InstitutionUuid] = item;

                if (dictionary.Count == lastTotalCount)
                    break;

                lastTotalCount = dictionary.Count;
                token = response.Pagination.Token;
            } while (dictionary.Count != allItemsCount || !string.IsNullOrWhiteSpace(token));

            var items = dictionary.Values.Select(i => new ResponseInstitution
            {
                InstitutionUuid = i.InstitutionUuid,

                Regon = string.IsNullOrWhiteSpace(i.Regon) ? null : Regon.Parse(i.Regon).To14SCharacters(),
                Nip = string.IsNullOrWhiteSpace(i.Nip) ? null : Nip.Parse(i.Nip).Value,
                Krs = string.IsNullOrWhiteSpace(i.Krs) ? null : Krs.Parse(i.Krs).Value,

                StartDate = i.IStartDt,
                LiquidationStartDate = i.ILiqStartDt,
                LiquidationDate = i.ILiqDt,

                Www = i.Www,
                Email = i.EMail,
                Phone = i.Phone,

                LastRefresh = i.LastRefresh,
                SourceLastRefresh = i.LastRefresh,
                DataSource = i.DataSource,

                InstitutionKind = new DictionaryItem
                {
                    Code = i.IKindCd,
                    Name = i.IKindName
                },

                Names = i.Names.Select(n => new NameSnapshot
                {
                    Name = n.Name,
                    Date = n.DateFrom
                }).ToList(),

                Types = i.Types.Select(t => new TypeSnapshot
                {
                    Type = new DictionaryItem { Code = string.Empty, Name = t.TypeName },
                    Date = t.DateFrom,
                }).ToList(),

                Statuses = i.Statuses.Select(s => new StatusSnapshot
                {
                    Status = new DictionaryItem { Code = string.Empty, Name = s.StatusName },
                    Date = s.DateFrom,
                }).ToList(),
            });

            await repository.CreateOrUpdateAsync(items);
        }
        catch (Exception ex)
        {
            await errorLogger.LogErrorAsync(ex);
        }
    }
}
using Quartz;
using RADON.Application.Interfaces;
using RADON.Contracts.Dictionaries;
using RADON.Contracts.Institutions;
using RADON.Contracts.Institutions.Responses;
using RADON.Infrastructure.Repositories.Institutions;
using RADON.Models.Responses.Dictionaries;
using static RADON.Models.Responses.Institutions.Institution;
using ResponseInstitution = RADON.Models.Responses.Institutions.Institution;

namespace RADON.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public abstract class CreateOrUpdateDictionaryInstitutionDataJob(
    IRadonDictionaryRespository respository,
    IRadonService radonService,
    DictionaryResource dictionaryResource) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var radonData = await radonService.GetDictionariesAsync(dictionaryResource);
        var dictionaryItems = radonData.Select(i => new DictionaryItem(i.Code, i.NamePl));
        await respository.CreateOrUpdateAsync(dictionaryItems);
    }
};

[DisallowConcurrentExecution]
public class CreateOrUpdateInstitutionKindsJob(
    IInstitutionKindRespository respository,
    IRadonService radonService
) : CreateOrUpdateDictionaryInstitutionDataJob(respository, radonService, DictionaryResource.InstitutionKinds);

[DisallowConcurrentExecution]
public class CreateOrUpdateInstitutionStatusesJob(
    IInstitutionStatusRespository respository,
    IRadonService radonService
) : CreateOrUpdateDictionaryInstitutionDataJob(respository, radonService, DictionaryResource.InstitutionStatuses);

[DisallowConcurrentExecution]
public class CreateOrUpdateUniversityTypesJob(
    IUniversityTypeRespository respository,
    IRadonService radonService
) : CreateOrUpdateDictionaryInstitutionDataJob(respository, radonService, DictionaryResource.InstitutionUniversityTypes);

[DisallowConcurrentExecution]
public class CreateOrUpdateScientificInstitutionTypesJob(
    IScientificInstitutionTypeRespository respository,
    IRadonService radonService
) : CreateOrUpdateDictionaryInstitutionDataJob(respository, radonService, DictionaryResource.InstitutionScientificInstitutionTypes);

[DisallowConcurrentExecution]
public class CreateOrUpdateInstitutionsJob(
    IInstitutionRespository respository,
    IRadonService radonService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var radonInstitutions = new List<InstitutionReport>();
        int allItemsCount;
        string? token = null;

        do
        {
            var response = await radonService.GetInstitutionsAsync(new QueryParameters() { Token = token });
            allItemsCount = response.Pagination.MaxCount;
            radonInstitutions.AddRange(response.Results);
            token = response.Pagination.Token;
        } while (radonInstitutions.Count != allItemsCount || !string.IsNullOrWhiteSpace(token));

        var items = radonInstitutions.Select(i => new ResponseInstitution
        {
            InstitutionUuid = i.InstitutionUuid,

            Regon = i.Regon,
            Nip = i.Nip,
            Krs = i.Krs,

            StartDate = i.IStartDt,
            LiquidationStartDate = i.ILiqStartDt,
            LiquidationDate = i.ILiqDt,

            Www = i.Www,
            Email = i.EMail,
            Phone = i.Phone,

            SourceLastRefresh = i.LastRefresh,
            DataSource = i.DataSource,

            InstitutionKind = new DictionaryItem(i.IKindCd, i.IKindName),
            Names = i.Names.Select(n => new NameSnapshot(n.Name, n.DateFrom)).ToList(),
            Types = i.Types.Select(t => new TypeSnapshot(new DictionaryItem(string.Empty, t.TypeName), t.DateFrom)).ToList(),
            Statuses = i.Statuses.Select(s => new StatusSnapshot(new DictionaryItem(string.Empty, s.StatusName), s.DateFrom)).ToList(),
        });

        await respository.CreateOrUpdateAsync(items);
    }
}
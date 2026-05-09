using Quartz;
using RADON.Application.Interfaces.Institutions;
using RADON.Application.Interfaces.Institutions.Dictionaries;
using RADON.Contracts.Dictionaries;
using RADON.Contracts.Institutions;
using RADON.Contracts.Institutions.Responses;
using RADON.Infrastructure.Jobs.Base;
using RADON.Models.Responses.Dictionaries;
using static RADON.Models.Responses.Institutions.Institution;
using ResponseInstitution = RADON.Models.Responses.Institutions.Institution;

namespace RADON.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class UpdateInstitutionKindJob(
    IInstitutionKindRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.InstitutionKinds);

[DisallowConcurrentExecution]
public class UpdateInstitutionStatusJob(
    IInstitutionStatusRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.InstitutionStatuses);

[DisallowConcurrentExecution]
public class UpdateUniversityTypeJob(
    IUniversityTypeRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.InstitutionUniversityTypes);

[DisallowConcurrentExecution]
public class UpdateScientificInstitutionTypeJob(
    IScientificInstitutionTypeRepository repository,
    IRadonService radonService
) : UpdateDictionaryDataJob(repository, radonService, DictionaryResource.InstitutionScientificInstitutionTypes);


[DisallowConcurrentExecution]
public class UpdateInstitutionJob(
    IInstitutionRepository repository,
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

        await repository.CreateOrUpdateAsync(items);
    }
}
using HotChocolate;
using HotChocolate.Types;
using RADON.Application.Interfaces.Institutions;
using RADON.Application.Interfaces.Institutions.Dictionaries;
using RADON.Models.Dictionaries.Responses;
using RADON.Models.Institutions;
using RADON.Models.Institutions.Responses;
using RADON.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace RADON.API.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class InstitutionsQuery
{
    [Display(Name = nameof(ApiDescription.GetInstitutions_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<Response<Institution>> GetInstitutions(
        [Service] IInstitutionRepository repository,
        QueryParameters queryParameters,
        CancellationToken cancellationToken)
        => await repository.GetAsync(queryParameters, cancellationToken);


    [Display(Name = nameof(ApiDescription.GetInstitutionKinds_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetInstitutionKinds(
        [Service] IInstitutionKindRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetInstitutionStatuses_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetInstitutionStatuses(
        [Service] IInstitutionStatusRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetScientificInstitutionTypes_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetScientificInstitutionTypes(
        [Service] IScientificInstitutionTypeRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetUniversityTypes_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetUniversityTypes(
        [Service] IUniversityTypeRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);
}
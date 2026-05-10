using HotChocolate;
using HotChocolate.Types;
using RADON.Application.Interfaces.Shared.Dictionaries;
using RADON.Models.Dictionaries.Responses;
using System.ComponentModel.DataAnnotations;

namespace RADON.API.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class SharedQuery
{
    [Display(Name = nameof(ApiDescription.GetDisciplines_Description), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetDisciplines(
        [Service] IDisciplineRespository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);
}
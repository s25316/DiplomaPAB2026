using GUS.REGON.Application.Interfaces;
using GUS.REGON.Models;
using GUS.REGON.Models.Responses;
using HotChocolate;
using HotChocolate.Types;
using System.ComponentModel.DataAnnotations;

namespace GUS.REGON.API.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class InstitutionsQuery
{
    [Display(Name = nameof(ApiDescription.GetInstitutions_Summary), ResourceType = typeof(ApiDescription))]
    [GraphQLName("getInstitutions")]
    public async Task<IEnumerable<Report.Full>> GetWojewodztwaAsync(
        [Service] IRequestRepository repository,
        QueryParameters parameters,
        CancellationToken cancellationToken
    ) => await repository.GetAsync(parameters, cancellationToken);
}
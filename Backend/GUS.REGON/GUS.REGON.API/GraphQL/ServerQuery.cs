using GUS.REGON.Models.Responses;
using HotChocolate;
using HotChocolate.Types;
using System.ComponentModel.DataAnnotations;

namespace GUS.REGON.API.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class ServerQuery
{
    [Display(Name = nameof(ApiDescription.GetCurrentTime_Summary), ResourceType = typeof(ApiDescription))]
    [GraphQLName("getCurrentTime")]
    public ServerTimeResponse GetCurrentTime() => new();
}
using HotChocolate.Types;
using RADON.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace RADON.API.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class ServerQuery
{
    [Display(Name = nameof(ApiDescription.GetCurrentTime_Summary), ResourceType = typeof(ApiDescription))]
    public ServerTimeResponse GetCurrentTime() => new();
}
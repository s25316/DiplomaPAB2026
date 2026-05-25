using HotChocolate;
using HotChocolate.Types;

namespace Diploma.API.GraphQL;


[ExtendObjectType(OperationTypeNames.Query)]
public class ServerQuery
{
    [GraphQLName("getCurrentTime")]
    public DateTimeOffset GetCurrentTime() => DateTimeOffset.Now;
}
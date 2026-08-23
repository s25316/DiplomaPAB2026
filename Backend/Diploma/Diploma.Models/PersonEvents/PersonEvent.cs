using Diploma.Models.Shared;

namespace Diploma.Models.PersonEvents;

public sealed class PersonEventQueryParameters : BaseQueryParameters;

public abstract record PersonEventQueryResult
{
    public abstract record Failure : PersonEventQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(Response<PersonEventDto> Response) : PersonEventQueryResult;
}

public sealed class PersonEventDto
{
    public required Guid PersonEventId { get; init; }
    public required int Code { get; init; }
    public required string Name { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
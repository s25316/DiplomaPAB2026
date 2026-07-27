using Diploma.Models.Shared;

namespace Diploma.Models.PersonUris;

public class PersonUriQueryParameters : BaseQueryParameters
{
    public enum PersonUriOrderBy
    {
        Name = 1,
        CreatedAt = 2,
    }

    public required PersonUriOrderBy OrderBy { get; init; }
    public required Order Order { get; init; } = Order.Ascending;
}

public abstract record PersonUriQueryResult
{
    public sealed record ProfileInactive : PersonUriQueryResult;
    public sealed record Success(Response<PersonUriDto> Response) : PersonUriQueryResult;
}

public record PersonUriDto
{
    public required Guid UriId { get; init; }
    public required string Uri { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
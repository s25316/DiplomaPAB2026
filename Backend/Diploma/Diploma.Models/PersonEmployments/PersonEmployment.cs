using Diploma.Models.Shared;

namespace Diploma.Models.PersonEmployments;

public class PersonEmploymentQueryParameters : BaseQueryParameters
{
    public enum PersonEmploymentOrderBy
    {
        From = 1,
        Position = 2,
    }

    public required PersonEmploymentOrderBy OrderBy { get; init; }
    public required Order Order { get; init; } = Order.Ascending;
}

public abstract record PersonEmploymentQueryResult
{
    public abstract record Failure : PersonEmploymentQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(Response<PersonEmploymentDto> Response) : PersonEmploymentQueryResult;
}

public record PersonEmploymentDto
{
    public required Guid EmploymentId { get; init; }
    public required string Regon { get; init; }
    public required string Position { get; init; }
    public required string Description { get; init; }
    public required DateOnly From { get; init; }
    public required DateOnly? To { get; init; }
}
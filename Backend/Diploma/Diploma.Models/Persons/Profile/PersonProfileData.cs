namespace Diploma.Models.Persons.Profile;

public abstract record PersonProfileDataQueryResult
{
    public abstract record Failure : PersonProfileDataQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(PersonProfileDataDto Response) : PersonProfileDataQueryResult;
}

public sealed record PersonProfileDataDto
{
    public required string? Title { get; init; }
    public required string? Summary { get; init; }
}
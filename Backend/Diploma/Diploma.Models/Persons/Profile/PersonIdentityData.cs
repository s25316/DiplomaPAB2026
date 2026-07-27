namespace Diploma.Models.Persons.Profile;

public abstract record PersonIdentityDataQueryResult
{
    public abstract record Failure : PersonIdentityDataQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(PersonIdentityDataDto Response) : PersonIdentityDataQueryResult;
}

public sealed record PersonIdentityDataDto
{
    public required string? Name { get; init; }
    public required string? Surname { get; init; }
}
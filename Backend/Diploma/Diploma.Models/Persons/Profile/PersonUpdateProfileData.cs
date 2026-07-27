namespace Diploma.Models.Persons.Profile;

public sealed record PersonUpdateProfileDataRequest
{
    public required string? Title { get; init; }
    public required string? Summary { get; init; }
}

public abstract record PersonUpdateProfileDataResult
{
    public sealed record Success : PersonUpdateProfileDataResult;
    public sealed record Failure : PersonUpdateProfileDataResult;
}
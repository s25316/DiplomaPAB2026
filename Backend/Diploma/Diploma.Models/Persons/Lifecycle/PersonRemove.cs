namespace Diploma.Models.Persons.Lifecycle;

public abstract record PersonRemoveResult
{
    public sealed record Success : PersonRemoveResult;
    public sealed record Failure : PersonRemoveResult;
}
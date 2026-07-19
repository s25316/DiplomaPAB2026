namespace Diploma.Models.Persons.Lifecycle;

public abstract record PersonRestoreResult
{
    public sealed record Success : PersonRestoreResult;
    public sealed record Failure : PersonRestoreResult;
}
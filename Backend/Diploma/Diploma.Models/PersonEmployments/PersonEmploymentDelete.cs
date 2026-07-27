namespace Diploma.Models.PersonEmployments;

public abstract record PersonEmploymentDeleteResult
{
    public sealed record Success : PersonEmploymentDeleteResult;
    public abstract record Failure : PersonEmploymentDeleteResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}
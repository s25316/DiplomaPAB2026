namespace Diploma.Models.Projects;

public abstract record ProjectDeleteResult
{
    public sealed record Success : ProjectDeleteResult;
    public abstract record Failure : ProjectDeleteResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}
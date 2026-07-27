namespace Diploma.Models.PersonEducations;

public abstract record PersonEducationDeleteResult
{
    public sealed record Success : PersonEducationDeleteResult;
    public abstract record Failure : PersonEducationDeleteResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}
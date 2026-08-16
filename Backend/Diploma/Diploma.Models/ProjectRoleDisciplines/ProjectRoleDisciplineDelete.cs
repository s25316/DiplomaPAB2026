namespace Diploma.Models.ProjectRoleDisciplines;

public abstract record ProjectRoleDisciplineDeleteResult
{
    public sealed record Success : ProjectRoleDisciplineDeleteResult;
    public abstract record Failure : ProjectRoleDisciplineDeleteResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}
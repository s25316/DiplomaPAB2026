namespace Diploma.Models.ProjectRoles;

public abstract record ProjectRoleDeleteResult
{
    public sealed record Success : ProjectRoleDeleteResult;
    public abstract record Failure : ProjectRoleDeleteResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}
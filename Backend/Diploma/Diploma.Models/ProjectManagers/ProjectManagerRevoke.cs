namespace Diploma.Models.ProjectManagers;

public abstract record ProjectManagerRevokeResult
{
    public sealed record Success : ProjectManagerRevokeResult;
    public abstract record Failure : ProjectManagerRevokeResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}
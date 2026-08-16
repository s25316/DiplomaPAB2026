namespace Diploma.Models.ProjectRoleEducationInstitutions;

public abstract record ProjectRoleEducationInstitutionDeleteResult
{
    public sealed record Success : ProjectRoleEducationInstitutionDeleteResult;
    public abstract record Failure : ProjectRoleEducationInstitutionDeleteResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}
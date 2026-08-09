using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.ProjectManagers.Aggregates;

public abstract record ProjectManagerResult
{
    public abstract record Grant : ProjectManagerResult
    {
        public sealed record Success() : Grant;
        public abstract record Failure() : Grant
        {
            public sealed record RecordExist() : Failure;
            public sealed record ProjectNotExist() : Failure;
        }
    }
}

public interface IProjectManagerRepository
{
    Task<OptionalResult<ProjectManager>> GetAsync(
        ProjectManagerId projectManagerId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ProjectManager>> GetAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ProjectManager>> GetAsync(
        PersonId personId,
        ProjectId projectId,
        CancellationToken cancellationToken = default);

    Task<ProjectManagerResult.Grant> GrantAsync(
        PersonId personId,
        ProjectManager item,
        CancellationToken cancellationToken = default);

    Task<ExistingResult> RevokeAsync(
        PersonId personId,
        ProjectManager item,
        CancellationToken cancellationToken = default);
}
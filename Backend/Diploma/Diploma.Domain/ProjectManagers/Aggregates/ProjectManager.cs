using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Shared.ProjectManagerRoles;

namespace Diploma.Domain.ProjectManagers.Aggregates;

public record ProjectManagerId : BaseEntityId<Guid>
{
    public static implicit operator Guid(ProjectManagerId value) => value.Value;
    public static implicit operator ProjectManagerId(Guid value) => new() { Value = value };
}
public partial class ProjectManager : BaseEntity<ProjectManagerId>
{
    public PersonId PersonId { get; protected set; } = null!;
    public ProjectId ProjectId { get; protected set; } = null!;
    public ProjectManagerRole ProjectManagerRole { get; protected set; } = null!;

    public DateTimeOffset Grant { get; protected set; }
    public DateTimeOffset? Revoke { get; protected set; }
}
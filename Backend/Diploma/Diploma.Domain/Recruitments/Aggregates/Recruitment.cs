using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Shared.RecruitmentStatuses;

namespace Diploma.Domain.Recruitments.Aggregates;

public sealed record RecruitmentId : BaseEntityId<Guid>
{
    public static implicit operator Guid(RecruitmentId value) => value.Value;
    public static implicit operator RecruitmentId(Guid value) => new() { Value = value };
}
public partial class Recruitment : BaseEntity<RecruitmentId>
{
    public PersonId PersonId { get; private set; } = null!;
    public ProjectId ProjectId { get; private set; } = null!;
    public RecruitmentStatus RecruitmentStatus { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }

    private readonly IList<ProjectRoleId> projectRoleIds = [];
    public IEnumerable<ProjectRoleId> ProjectRoleIds => projectRoleIds;


    public static Recruitment Create(
        PersonId personId,
        ProjectId projectId,
        IEnumerable<ProjectRoleId> projectRoleIds
        )
    {
        var item = new Recruitment();

        item.PersonId = personId;
        item.ProjectId = projectId;
        item.RecruitmentStatus = RecruitmentStatus.None;
        item.CreatedAt = DateTimeOffset.Now;

        foreach (var roleId in projectRoleIds)
            item.projectRoleIds.Add(roleId);

        return item;
    }
}
using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.ProjectRoles.Aggregates;

public partial class ProjectRole
{
    public class Builder : BaseEntityBulder<ProjectRole, ProjectRoleId>
    {
        public Builder WithId(ProjectRoleId item)
        {
            With(i => i.Id = item);
            return this;
        }

        public Builder WithLastSnapshotId(ProjectRoleId item)
        {
            With(i => i.LastSnapshotId = item);
            return this;
        }

        public Builder WithProjectId(ProjectId item)
        {
            With(i => i.ProjectId = item);
            return this;
        }

        public Builder WithTitle(string item)
        {
            With(i => i.Title = item);
            return this;
        }

        public Builder WithDescription(string item)
        {
            With(i => i.Description = item);
            return this;
        }

        public Builder WithIsAvailableRecruitment(bool item)
        {
            With(i => i.IsAvailableRecruitment = item);
            return this;
        }
    }
}
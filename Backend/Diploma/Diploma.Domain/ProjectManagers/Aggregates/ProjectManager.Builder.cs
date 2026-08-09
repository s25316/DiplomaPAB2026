using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Shared.ProjectManagerRoles;

namespace Diploma.Domain.ProjectManagers.Aggregates;

public partial class ProjectManager
{
    public class Builder : BaseEntityBulder<ProjectManager, ProjectManagerId>
    {
        public Builder WithId(ProjectManagerId item)
        {
            With(i => i.Id = item);
            return this;
        }

        public Builder WithPersonId(PersonId item)
        {
            With(i => i.PersonId = item);
            return this;
        }

        public Builder WithProjectId(ProjectId item)
        {
            With(i => i.ProjectId = item);
            return this;
        }

        public Builder WithProjectManagerRole(ProjectManagerRole item)
        {
            With(i => i.ProjectManagerRole = item);
            return this;
        }

        public Builder WithGrant(DateTimeOffset item)
        {
            With(i => i.Grant = item);
            return this;
        }

        public Builder WithRevoke(DateTimeOffset? item)
        {
            With(i => i.Revoke = item);
            return this;
        }
    }
}
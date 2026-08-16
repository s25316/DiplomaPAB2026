using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.ProjectRoleDisciplines.Aggregates;

public partial class ProjectRoleDiscipline
{
    public class Builder : BaseEntityBulder<ProjectRoleDiscipline, ProjectRoleDisciplineId>
    {
        public Builder WithId(ProjectRoleDisciplineId item)
        {
            With(i => i.Id = item);
            return this;
        }

        public Builder WithProjectId(ProjectId item)
        {
            With(i => i.ProjectId = item);
            return this;
        }

        public Builder WithProjectRoleId(ProjectRoleId item)
        {
            With(i => i.ProjectRoleId = item);
            return this;
        }

        public Builder WithDisciplineCode(string item)
        {
            With(i => i.DisciplineCode = item);
            return this;
        }

        public Builder WithCreatedAt(DateTimeOffset item)
        {
            With(i => i.CreatedAt = item);
            return this;
        }
    }
}
using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.EducationInstitutions.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.ProjectRoleEducationInstitutions.Aggregates;

public partial class ProjectRoleEducationInstitution
{
    public class Builder : BaseEntityBulder<ProjectRoleEducationInstitution, ProjectRoleEducationInstitutionId>
    {
        public Builder WithId(ProjectRoleEducationInstitutionId item)
        {
            With(i => i.Id = item);
            return this;
        }

        public Builder WithProjectRoleId(ProjectRoleId item)
        {
            With(i => i.ProjectRoleId = item);
            return this;
        }

        public Builder WithProjectId(ProjectId item)
        {
            With(i => i.ProjectId = item);
            return this;
        }

        public Builder WithEducationInstitutionId(EducationInstitutionId item)
        {
            With(i => i.EducationInstitutionId = item);
            return this;
        }

        public Builder WithCreatedAt(DateTimeOffset item)
        {
            With(i => i.CreatedAt = item);
            return this;
        }
    }
}
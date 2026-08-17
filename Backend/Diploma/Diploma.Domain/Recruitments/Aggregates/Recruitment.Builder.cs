using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Shared.RecruitmentStatuses;

namespace Diploma.Domain.Recruitments.Aggregates;

public partial class Recruitment
{
    public class Builder : BaseEntityBulder<Recruitment, RecruitmentId>
    {
        public Builder WithId(RecruitmentId item)
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

        public Builder WithRecruitmentStatus(RecruitmentStatus item)
        {
            With(i => i.RecruitmentStatus = item);
            return this;
        }

        public Builder WithCreatedAt(DateTimeOffset item)
        {
            With(i => i.CreatedAt = item);
            return this;
        }

        public Builder With(IEnumerable<ProjectRoleId> items)
        {
            With(i =>
            {
                foreach (var item in items)
                    i.projectRoleIds.Add(item);
            });
            return this;
        }
    }
}
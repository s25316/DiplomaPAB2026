using Diploma.Domain.Base.Aggregates;

namespace Diploma.Domain.Projects.Aggregates;

public partial class Project
{
    public class Builder : BaseEntityBulder<Project, ProjectId>
    {
        public Builder WithId(ProjectId item)
        {
            With(i => i.Id = item);
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

        public Builder WithIsVisible(bool item)
        {
            With(i => i.IsVisible = item);
            return this;
        }

        public Builder WithCreatedAt(DateTimeOffset item)
        {
            With(i => i.CreatedAt = item);
            return this;
        }
    }
}
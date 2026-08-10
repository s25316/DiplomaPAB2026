using Diploma.Domain.Base.Aggregates;

namespace Diploma.Domain.Projects.Aggregates;

public sealed record ProjectId : BaseEntityId<Guid>
{
    public static implicit operator Guid(ProjectId value) => value.Value;
    public static implicit operator ProjectId(Guid value) => new() { Value = value };
}
public partial class Project : BaseEntity<ProjectId>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsVisible { get; protected set; } = false;
    public DateTimeOffset CreatedAt { get; protected set; }


    public void ChangeVisibility(bool? value)
    {
        IsVisible = value ?? !IsVisible;
    }

    public static Project Create(string title, string description)
    {
        var item = new Project
        {
            Title = title,
            Description = description,
            IsVisible = false,
            CreatedAt = DateTimeOffset.Now,
        };

        return item;
    }
}
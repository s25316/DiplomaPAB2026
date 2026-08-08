namespace Diploma.Shared.ProjectEvents;

public sealed class ProjectEvent
{
    private enum ProjectEventKind
    {
        ProjectCreated = 1,
        ProjectUpdated = 2,
        ProjectRemoved = 3,

        ProjectRoleCreated = 11,
        ProjectRoleUpdated = 12,
        ProjectRoleRemoved = 13,

        GrandRole = 21,
        RevokeRole = 22,
    }


    public int Id { get; }
    public string Name { get; }


    private ProjectEvent(ProjectEventKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static ProjectEvent()
    {
        All = [
            ProjectCreated, ProjectUpdated, ProjectRemoved,
            ProjectRoleCreated, ProjectRoleUpdated, ProjectRoleRemoved,
            GrandRole, RevokeRole
        ];
    }


    public static readonly IEnumerable<ProjectEvent> All;
    public static ProjectEvent FromId(int id) => All.FirstOrDefault(v => v.Id == id)
        ?? throw new NotImplementedException($"Invalid {nameof(ProjectEvent)} Id: {id}");

    public static readonly ProjectEvent ProjectCreated = new(ProjectEventKind.ProjectCreated, "");
    public static readonly ProjectEvent ProjectUpdated = new(ProjectEventKind.ProjectUpdated, "");
    public static readonly ProjectEvent ProjectRemoved = new(ProjectEventKind.ProjectRemoved, "");

    public static readonly ProjectEvent ProjectRoleCreated = new(ProjectEventKind.ProjectRoleCreated, "");
    public static readonly ProjectEvent ProjectRoleUpdated = new(ProjectEventKind.ProjectRoleUpdated, "");
    public static readonly ProjectEvent ProjectRoleRemoved = new(ProjectEventKind.ProjectRoleRemoved, "");

    public static readonly ProjectEvent GrandRole = new(ProjectEventKind.GrandRole, "");
    public static readonly ProjectEvent RevokeRole = new(ProjectEventKind.RevokeRole, "");
}
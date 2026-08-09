namespace Diploma.Shared.ProjectManagerRoles;

public sealed record ProjectManagerRole
{
    private enum ProjectManagerRoleKind
    {
        Creator = 1,
        Admin = 2,
        Moderator = 3,
        Recruiter = 4,
        RoleManager = 5,
    }

    public int Id { get; }
    public string Name { get; }


    private ProjectManagerRole(ProjectManagerRoleKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static ProjectManagerRole()
    {
        All = [
            Creator, Admin,
            Moderator , Recruiter, RoleManager
        ];
    }


    public static readonly IEnumerable<ProjectManagerRole> All;
    public static ProjectManagerRole FromId(int id) => All.FirstOrDefault(v => v.Id == id)
       ?? throw new NotImplementedException($"Invalid {nameof(ProjectManagerRole)} Id: {id}");


    public static readonly ProjectManagerRole Creator = new(ProjectManagerRoleKind.Creator, "Twórca");
    public static readonly ProjectManagerRole Admin = new(ProjectManagerRoleKind.Admin, "Administrator");
    public static readonly ProjectManagerRole Moderator = new(ProjectManagerRoleKind.Moderator, "Moderator");
    public static readonly ProjectManagerRole Recruiter = new(ProjectManagerRoleKind.Recruiter, "Rekruter");
    public static readonly ProjectManagerRole RoleManager = new(ProjectManagerRoleKind.RoleManager, "Zarządca ról");
}
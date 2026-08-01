namespace Diploma.Shared.ProjectManagers;

public sealed record ProjectManager
{
    private enum ProjectManagerKind
    {
        Creator = 1,
        Admin = 2,
        Moderator = 3,
        Recruiter = 4,
        RoleManager = 5,
    }

    public int Id { get; }
    public string Name { get; }


    private ProjectManager(ProjectManagerKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static ProjectManager()
    {
        All = [
            Creator, Admin,
            Moderator , Recruiter, RoleManager
        ];
    }


    public static readonly IEnumerable<ProjectManager> All;
    public static ProjectManager FromId(int id) => All.FirstOrDefault(v => v.Id == id)
       ?? throw new NotImplementedException($"Invalid {nameof(ProjectManager)} Id: {id}");


    public static readonly ProjectManager Creator = new(ProjectManagerKind.Creator, "Twórca");
    public static readonly ProjectManager Admin = new(ProjectManagerKind.Admin, "Administrator");
    public static readonly ProjectManager Moderator = new(ProjectManagerKind.Moderator, "Moderator");
    public static readonly ProjectManager Recruiter = new(ProjectManagerKind.Recruiter, "Rekruter");
    public static readonly ProjectManager RoleManager = new(ProjectManagerKind.RoleManager, "Zarządca ról");
}
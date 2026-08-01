namespace Diploma.Shared.ProjectVisibilities;

public sealed record ProjectVisibility
{
    private enum ProjectVisibilityKind
    {
        Private = 1,
        Public = 2,
    }

    public int Id { get; }
    public string Name { get; }


    private ProjectVisibility(ProjectVisibilityKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static ProjectVisibility()
    {
        All = [
            Private, Public,
        ];
    }


    public static readonly IEnumerable<ProjectVisibility> All;
    public static ProjectVisibility FromId(int id) => All.FirstOrDefault(v => v.Id == id)
       ?? throw new NotImplementedException($"Invalid {nameof(ProjectVisibility)} Id: {id}");


    public static readonly ProjectVisibility Private = new(ProjectVisibilityKind.Private, "Prywatne");
    public static readonly ProjectVisibility Public = new(ProjectVisibilityKind.Public, "Publiczne");
}
using Diploma.Shared.PersonOperations;

namespace Diploma.Shared.Semesters;

public sealed record Semester
{
    private enum SemesterKind
    {
        Summer = 1,
        Winter = 2,
    }

    public int Id { get; }
    public string Name { get; }


    private Semester(SemesterKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static Semester()
    {
        All = [
            Summer, Winter,
        ];
    }


    public static readonly IEnumerable<Semester> All;
    public static Semester FromId(int id) => All.FirstOrDefault(v => v.Id == id)
       ?? throw new NotImplementedException($"Invalid {nameof(PersonOperation)} Id: {id}");


    public static readonly Semester Summer = new(SemesterKind.Summer, "Letni");
    public static readonly Semester Winter = new(SemesterKind.Winter, "Zimowy");
}
namespace Diploma.Shared.RecruitmentStatuses;

public class RecruitmentStatus
{
    private enum RecruitmentStatusKind
    {
        None = 1,
        Rejected = 2,
        Accepted = 3,
    }

    public int Id { get; }
    public string Name { get; }


    private RecruitmentStatus(RecruitmentStatusKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static RecruitmentStatus()
    {
        All = [
            None, Rejected, Accepted
        ];
    }


    public static readonly IEnumerable<RecruitmentStatus> All;
    public static RecruitmentStatus FromId(int id) => All.FirstOrDefault(v => v.Id == id)
       ?? throw new NotImplementedException($"Invalid {nameof(RecruitmentStatus)} Id: {id}");

    public static readonly RecruitmentStatus None = new(RecruitmentStatusKind.None, "W trakcie rekrutacji");
    public static readonly RecruitmentStatus Rejected = new(RecruitmentStatusKind.Rejected, "Odrzucony");
    public static readonly RecruitmentStatus Accepted = new(RecruitmentStatusKind.Accepted, "Zaakceptowany");
}
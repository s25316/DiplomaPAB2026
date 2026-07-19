namespace Diploma.Shared.Verifications;

public sealed record Verification
{
    private enum VerificationKind
    {
        None = 1,
        Code = 2,
        MagicLink = 3,
    }


    public int Id { get; }
    public string Name { get; }


    private Verification(VerificationKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static Verification()
    {
        All = [
            None,
            Code,
            MagicLink,
        ];
    }


    public static readonly IEnumerable<Verification> All;
    public static Verification FromId(int id) => All.FirstOrDefault(v => v.Id == id)
        ?? throw new NotImplementedException($"Invalid {nameof(Verification)} Id: {id}");


    public static readonly Verification None = new(VerificationKind.None, "None");
    public static readonly Verification Code = new(VerificationKind.Code, "Code");
    public static readonly Verification MagicLink = new(VerificationKind.MagicLink, "MagicLink");
}
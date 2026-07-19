namespace Diploma.Shared.PersonOperations;

public sealed class PersonOperation
{
    private enum PersonOperationKind
    {
        ProfileCreatedAndActivation = 1,
        ProfileActivated = 2,
        ProfileRemovedAndSendRestoringLink = 3,
        ProfileRestored = 4,
        ProfileAnonymized = 5,

        LogInSucess = 11,
        LogInUnsucess = 12,

        InitiateUpdatingLogin = 1001,
        UpdatedLogin = 1002,
        InitiateUpdatingPassword = 1003,
        UpdatedPassword = 1004,
    }

    public int Id { get; }
    public string Name { get; }


    private PersonOperation(PersonOperationKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static PersonOperation()
    {
        All = [
            ProfileCreatedAndActivation, ProfileActivated, ProfileRemovedAndSendRestoringLink, ProfileRestored, ProfileAnonymized,
            LogInSucess, LogInUnsucess,
            InitiateUpdatingLogin, UpdatedLogin,
            InitiateUpdatingPassword, UpdatedPassword,
        ];
    }


    public static readonly IEnumerable<PersonOperation> All;
    public static PersonOperation FromId(int id) => All.FirstOrDefault(v => v.Id == id)
       ?? throw new NotImplementedException($"Invalid {nameof(PersonOperation)} Id: {id}");


    public static readonly PersonOperation ProfileCreatedAndActivation = new(PersonOperationKind.ProfileCreatedAndActivation, "");
    public static readonly PersonOperation ProfileActivated = new(PersonOperationKind.ProfileActivated, "");
    public static readonly PersonOperation ProfileRemovedAndSendRestoringLink = new(PersonOperationKind.ProfileRemovedAndSendRestoringLink, "");
    public static readonly PersonOperation ProfileRestored = new(PersonOperationKind.ProfileRestored, "");
    public static readonly PersonOperation ProfileAnonymized = new(PersonOperationKind.ProfileAnonymized, "");

    public static readonly PersonOperation LogInSucess = new(PersonOperationKind.LogInSucess, "");
    public static readonly PersonOperation LogInUnsucess = new(PersonOperationKind.LogInUnsucess, "");

    public static readonly PersonOperation InitiateUpdatingLogin = new(PersonOperationKind.InitiateUpdatingLogin, "");
    public static readonly PersonOperation UpdatedLogin = new(PersonOperationKind.UpdatedLogin, "");
    public static readonly PersonOperation InitiateUpdatingPassword = new(PersonOperationKind.InitiateUpdatingPassword, "");
    public static readonly PersonOperation UpdatedPassword = new(PersonOperationKind.UpdatedPassword, "");
}
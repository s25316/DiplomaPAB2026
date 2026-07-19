namespace Diploma.Shared.PersonEvents;

public sealed class PersonEvent
{
    private enum PersonEventKind
    {
        Created = 1,
        Activated = 2,
        Removed = 3,
        Restored = 4,
        Anonymized = 5,

        LogInSucess = 11,
        LogInUnsucess = 12,
        LogOut = 13,

        UpdateLogin = 1002,
        UpdatePassword = 1004,

        UpdateIdentityData = 1011,
        UpdateProfileData = 1012,

        CreateUri = 1021,
        UpdateUri = 1022,
        DeleteUri = 1023,

        CreateEmployment = 1031,
        UpdateEmployment = 1032,
        DeleteEmployment = 1033,

        CreateEducation = 1041,
        UpdateEducation = 1042,
        DeleteEducation = 1043,
    }


    public int Id { get; }
    public string Name { get; }


    private PersonEvent(PersonEventKind @enum, string name)
    {
        Id = (int)@enum;
        Name = name;
    }

    static PersonEvent()
    {
        All = [
            Created, Activated, Removed, Restored, Anonymized,
            LogInSucess, LogInUnsucess, LogOut,
            UpdateLogin, UpdatePassword,
            UpdateIdentityData, UpdateProfileData,
            CreateUri, UpdateUri, DeleteUri,
            CreateEmployment, UpdateEmployment, DeleteEmployment,
            CreateEducation, UpdateEducation, DeleteEducation,
        ];
    }


    public static readonly IEnumerable<PersonEvent> All;
    public static PersonEvent FromId(int id) => All.FirstOrDefault(v => v.Id == id)
        ?? throw new NotImplementedException($"Invalid {nameof(PersonEvent)} Id: {id}");


    public static readonly PersonEvent Created = new(PersonEventKind.Created, "");
    public static readonly PersonEvent Activated = new(PersonEventKind.Activated, "");
    public static readonly PersonEvent Removed = new(PersonEventKind.Removed, "");
    public static readonly PersonEvent Restored = new(PersonEventKind.Restored, "");
    public static readonly PersonEvent Anonymized = new(PersonEventKind.Anonymized, "");

    public static readonly PersonEvent LogInSucess = new(PersonEventKind.LogInSucess, "");
    public static readonly PersonEvent LogInUnsucess = new(PersonEventKind.LogInUnsucess, "");
    public static readonly PersonEvent LogOut = new(PersonEventKind.LogOut, "");

    public static readonly PersonEvent UpdateLogin = new(PersonEventKind.UpdateLogin, "");
    public static readonly PersonEvent UpdatePassword = new(PersonEventKind.UpdatePassword, "");

    public static readonly PersonEvent UpdateIdentityData = new(PersonEventKind.UpdateIdentityData, "");
    public static readonly PersonEvent UpdateProfileData = new(PersonEventKind.UpdateProfileData, "");

    public static readonly PersonEvent CreateUri = new(PersonEventKind.CreateUri, "");
    public static readonly PersonEvent UpdateUri = new(PersonEventKind.UpdateUri, "");
    public static readonly PersonEvent DeleteUri = new(PersonEventKind.DeleteUri, "");

    public static readonly PersonEvent CreateEmployment = new(PersonEventKind.CreateEmployment, "");
    public static readonly PersonEvent UpdateEmployment = new(PersonEventKind.UpdateEmployment, "");
    public static readonly PersonEvent DeleteEmployment = new(PersonEventKind.DeleteEmployment, "");

    public static readonly PersonEvent CreateEducation = new(PersonEventKind.CreateEducation, "");
    public static readonly PersonEvent UpdateEducation = new(PersonEventKind.UpdateEducation, "");
    public static readonly PersonEvent DeleteEducation = new(PersonEventKind.DeleteEducation, "");
}
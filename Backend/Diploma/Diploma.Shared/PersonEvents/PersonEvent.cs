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


    public static readonly PersonEvent Created = new(PersonEventKind.Created, "Utworzenie konta");
    public static readonly PersonEvent Activated = new(PersonEventKind.Activated, "Aktywacja konta");
    public static readonly PersonEvent Removed = new(PersonEventKind.Removed, "Usunięcie konta");
    public static readonly PersonEvent Restored = new(PersonEventKind.Restored, "Przywrócenie konta");
    public static readonly PersonEvent Anonymized = new(PersonEventKind.Anonymized, "Anonimizacja danych");

    public static readonly PersonEvent LogInSucess = new(PersonEventKind.LogInSucess, "Udane logowanie");
    public static readonly PersonEvent LogInUnsucess = new(PersonEventKind.LogInUnsucess, "Nieudane logowanie");
    public static readonly PersonEvent LogOut = new(PersonEventKind.LogOut, "Wylogowanie");

    public static readonly PersonEvent UpdateLogin = new(PersonEventKind.UpdateLogin, "Zmiana login");
    public static readonly PersonEvent UpdatePassword = new(PersonEventKind.UpdatePassword, "Zmiana hasła");

    public static readonly PersonEvent UpdateIdentityData = new(PersonEventKind.UpdateIdentityData, "Aktualizacja danych tożsamości");
    public static readonly PersonEvent UpdateProfileData = new(PersonEventKind.UpdateProfileData, "Aktualizacja danych profilu");

    public static readonly PersonEvent CreateUri = new(PersonEventKind.CreateUri, "Dodanie linku");
    public static readonly PersonEvent UpdateUri = new(PersonEventKind.UpdateUri, "Modyfikacja linku");
    public static readonly PersonEvent DeleteUri = new(PersonEventKind.DeleteUri, "Usunięcie linku");

    public static readonly PersonEvent CreateEmployment = new(PersonEventKind.CreateEmployment, "Dodanie historii zatrudnienia");
    public static readonly PersonEvent UpdateEmployment = new(PersonEventKind.UpdateEmployment, "Modyfikacja historii zatrudnienia");
    public static readonly PersonEvent DeleteEmployment = new(PersonEventKind.DeleteEmployment, "Usunięcie historii zatrudnienia");

    public static readonly PersonEvent CreateEducation = new(PersonEventKind.CreateEducation, "Dodanie wykształcenia");
    public static readonly PersonEvent UpdateEducation = new(PersonEventKind.UpdateEducation, "Modyfikacja wykształcenia");
    public static readonly PersonEvent DeleteEducation = new(PersonEventKind.DeleteEducation, "Usunięcie wykształcenia");
}
namespace Frontend.Components.Profiles.Layout.Educations;

public class PersonEducationFormModel
{
    public Guid EducationCourseId { get; set; }
    public Guid? EducationCourseInstanceId { get; set; }

    // Pola startu (edytowalne)
    public int StartYear { get; set; }
    public int StartSemestrId { get; set; }

    // Pola końca (opcjonalne, edytowalne)
    public int? EndYear { get; set; }
    public int? EndSemestrId { get; set; }
}

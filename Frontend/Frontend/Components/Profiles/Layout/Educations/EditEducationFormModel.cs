using Frontend.Components.Shared;

namespace Frontend.Components.Profiles.Layout.Educations;

[ValidEducationPeriod]
public class EditEducationFormModel
{
    public int StartYear { get; set; }
    public int StartSemestrId { get; set; }
    public int? EndYear { get; set; }
    public int? EndSemestrId { get; set; }
}
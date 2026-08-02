using Frontend.Components.Shared;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Components.Profiles.Layout.Educations;

[ValidEducationPeriod]
public class EditEducationFormModel
{
    [Required(ErrorMessage = "Rok startu jest wymagany.")]
    public int StartYear { get; set; }

    [Required(ErrorMessage = "Semestr startu jest wymagany.")]
    [Range(1, int.MaxValue, ErrorMessage = "Wybierz poprawny semestr startowy.")]
    public int StartSemestrId { get; set; }

    public int? EndYear { get; set; }
    public int? EndSemestrId { get; set; }
}
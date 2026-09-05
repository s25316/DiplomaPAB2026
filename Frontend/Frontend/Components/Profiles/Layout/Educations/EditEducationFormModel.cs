using Frontend.Components.Shared;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Components.Profiles.Layout.Educations;

[ValidEducationPeriod]
public class EditEducationFormModel
{
    [Required(ErrorMessage = "Rok startu jest wymagany.")]
    [Range(1900, 2100, ErrorMessage = "Wybierz poprawny rok startowy.")]
    public int? StartYear { get; set; }

    [Required(ErrorMessage = "Semestr startu jest wymagany.")]
    [Range(1, int.MaxValue, ErrorMessage = "Wybierz poprawny semestr startowy.")]
    public int? StartSemestrId { get; set; }

    [Range(1900, 2100, ErrorMessage = "Wybierz poprawny rok końcowy.")]
    public int? EndYear { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Wybierz poprawny semestr końcowy.")]
    public int? EndSemestrId { get; set; }
}
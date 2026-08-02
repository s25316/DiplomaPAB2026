using RADON.Models.Shared;
using System.ComponentModel.DataAnnotations;
using static RADON.Models.Courses.QueryParameters;

namespace Frontend.Components.Shared.Radons.Courses;

public class RadonCourseFilterViewModel
{
    [Display(Name = "Nazwa kierunku")]
    public string? Name { get; set; }

    [Display(Name = "Kształcenie nauczycieli")]
    public bool? IsTeacherTraining { get; set; }

    [Display(Name = "Kierunek filologiczny")]
    public bool? IsPhilological { get; set; }

    [Display(Name = "Studia dualne")]
    public bool? IsDual { get; set; }

    [Display(Name = "Studia pomostowe")]
    public bool? IsBridging { get; set; }

    [Display(Name = "Współpraca z zawodowymi")]
    public bool? IsCoopWithVocational { get; set; }

    // Filtry słownikowe
    public string? SelectedLevelCode { get; set; }
    public string? SelectedProfileCode { get; set; }
    public string? SelectedIscedCode { get; set; }
    public string? SelectedStatusCode { get; set; }
    public string? SelectedInstanceStatusCode { get; set; }
    public string? SelectedFormCode { get; set; }
    public string? SelectedLanguageCode { get; set; }
    public string? SelectedProfessionalTitleCode { get; set; }
    public string? SelectedDisciplineCode { get; set; } // Nowy filtr dyscypliny

    [Display(Name = "Sortowanie")]
    public QueryParametersOrderBy OrderBy { get; set; } = QueryParametersOrderBy.Name;

    [Display(Name = "Kierunek sortowania")]
    public Order Order { get; set; } = Order.Ascending;

    [Range(1, int.MaxValue, ErrorMessage = "Strona musi być większa od zera.")]
    public int Page { get; set; } = 1;

    [Range(1, 1000, ErrorMessage = "Liczba elementów na stronie musi być z zakresu 1-1000.")]
    public int ItemsPerPage { get; set; } = 100;

    public RADON.Models.Courses.QueryParameters ToQueryParameters()
    {
        var parameters = new RADON.Models.Courses.QueryParameters
        {
            Name = Name,
            IsTeacherTraining = IsTeacherTraining,
            IsPhilological = IsPhilological,
            IsDual = IsDual,
            IsBridging = IsBridging,
            IsCoopWithVocational = IsCoopWithVocational,
            OrderBy = OrderBy,
            Order = Order,
            Pagination = new QueryParametersPagination
            {
                Page = Page,
                ItemsPerPage = ItemsPerPage
            }
        };

        if (!string.IsNullOrEmpty(SelectedLevelCode))
            parameters.LevelCodes.Add(SelectedLevelCode);

        if (!string.IsNullOrEmpty(SelectedProfileCode))
            parameters.ProfileCodes.Add(SelectedProfileCode);

        if (!string.IsNullOrEmpty(SelectedIscedCode))
            parameters.IscedCodes.Add(SelectedIscedCode);

        if (!string.IsNullOrEmpty(SelectedStatusCode))
            parameters.StatusCodes.Add(SelectedStatusCode);

        if (!string.IsNullOrEmpty(SelectedInstanceStatusCode))
            parameters.InstanceStatusCodes.Add(SelectedInstanceStatusCode);

        if (!string.IsNullOrEmpty(SelectedFormCode))
            parameters.FormCodes.Add(SelectedFormCode);

        if (!string.IsNullOrEmpty(SelectedLanguageCode))
            parameters.LanguageCodes.Add(SelectedLanguageCode);

        if (!string.IsNullOrEmpty(SelectedProfessionalTitleCode))
            parameters.ProfessionalTitleCodes.Add(SelectedProfessionalTitleCode);

        if (!string.IsNullOrEmpty(SelectedDisciplineCode))
            parameters.DisciplineCodes.Add(SelectedDisciplineCode);

        return parameters;
    }
}
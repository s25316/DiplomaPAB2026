using Base.Models.ValueObjects.Regony;
using RADON.Models.Shared;
using System.ComponentModel.DataAnnotations;
using static RADON.Models.Institutions.QueryParameters;

namespace Frontend.Components.Shared.Radons.Institutions;

public class RadonInstitutionFilterViewModel
{
    [Display(Name = "Nazwa instytucji")]
    public string? Name { get; set; }

    [Display(Name = "REGON")]
    [RegularExpression(@"^(\d{9}|\d{14})$", ErrorMessage = "Numer REGON musi składać się z dokładnie 9 lub 14 cyfr.")]
    public string? RegonString { get; set; }

    [Display(Name = "Rodzaj instytucji")]
    public string? SelectedKindCode { get; set; }

    [Display(Name = "Status instytucji")]
    public string? SelectedStatusCode { get; set; }

    [Display(Name = "Typ uczelni")]
    public string? SelectedUniversityTypeCode { get; set; }

    [Display(Name = "Typ instytucji naukowej")]
    public string? SelectedScientificInstitutionTypeCode { get; set; }

    [Display(Name = "Sortowanie")]
    public QueryParametersOrderBy OrderBy { get; set; } = QueryParametersOrderBy.Name;


    [Display(Name = "Kierunek sortowania")]
    public Order Order { get; set; } = Order.Ascending;

    [Range(1, int.MaxValue, ErrorMessage = "Strona musi być większa od zera.")]
    public int Page { get; set; } = 1;

    [Range(1, 1000, ErrorMessage = "Liczba elementów na stronie musi być z zakresu 1-1000.")]
    public int ItemsPerPage { get; set; } = 100;

    // Metoda pakująca dane do Twojego oryginalnego obiektu QueryParameters
    public RADON.Models.Institutions.QueryParameters ToQueryParameters()
    {
        var parameters = new RADON.Models.Institutions.QueryParameters
        {
            Name = Name,
            OrderBy = OrderBy,
            Order = Order,
            Pagination = new QueryParametersPagination
            {
                Page = Page,
                ItemsPerPage = ItemsPerPage
            }
        };

        if (!string.IsNullOrEmpty(SelectedKindCode))
        {
            parameters.KindCodes.Add(SelectedKindCode);
        }

        if (!string.IsNullOrEmpty(SelectedStatusCode))
        {
            parameters.StatusCodes.Add(SelectedStatusCode);
        }

        if (!string.IsNullOrEmpty(SelectedUniversityTypeCode))
        {
            parameters.UniversityTypeCodes.Add(SelectedUniversityTypeCode);
        }

        if (!string.IsNullOrEmpty(SelectedScientificInstitutionTypeCode))
        {
            parameters.ScientificInstitutionTypeCodes.Add(SelectedScientificInstitutionTypeCode);
        }

        if (!string.IsNullOrEmpty(RegonString) && Regon.TryParse(RegonString, out var regon))
        {
            parameters.Regon = regon;
        }

        return parameters;
    }
}

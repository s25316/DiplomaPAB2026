using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.PersonEducations;

public sealed record EducationSemestrRequestDto
{
    [Required]
    [Range(1, 2)]
    public required int SemestrId { get; init; }

    [Required]
    [Range(1900, int.MaxValue, ErrorMessage = "Rok musi być w zakresie od 1900.")]
    public required int Year { get; init; }
}

public sealed record EducationSemestrResponseDto
{
    public required SemesterResponseDto Semester { get; init; }
    public required int Year { get; init; }
}
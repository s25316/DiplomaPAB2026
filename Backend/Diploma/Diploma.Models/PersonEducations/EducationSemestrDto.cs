using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.PersonEducations;

public sealed record EducationSemestrRequestDto
{
    [Range(1, 2)]
    public required int SemestrId { get; init; }

    [Range(1990, int.MaxValue)]
    public required int Year { get; init; }
}

public sealed record EducationSemestrResponseDto
{
    public required SemesterResponseDto Semester { get; init; }
    public required int Year { get; init; }
}
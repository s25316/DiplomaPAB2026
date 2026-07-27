namespace Diploma.Models.Educations;

public sealed record EducationDisciplineDto
{
    public required string Code { get; init; }
    public required string Name { get; init; }
}
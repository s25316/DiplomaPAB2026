namespace Diploma.Domain.Educations.ValueObjects;

public sealed record class EducationDiscipline
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}
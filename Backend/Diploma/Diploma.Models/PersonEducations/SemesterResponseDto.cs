namespace Diploma.Models.PersonEducations;

public sealed record SemesterResponseDto
{
    public required int SemestrId { get; init; }
    public required string Name { get; init; }
}
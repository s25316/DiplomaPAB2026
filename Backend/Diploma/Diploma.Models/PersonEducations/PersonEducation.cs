using Diploma.Models.Shared;

namespace Diploma.Models.PersonEducations;

public class PersonEducationQueryParameters
{
    public enum PersonEducationOrderBy
    {
        Semestr = 1,
    }

    public required PersonEducationOrderBy OrderBy { get; init; }
    public required Order Order { get; init; } = Order.Ascending;
}

public abstract record PersonEducationQueryResult
{
    public abstract record Failure : PersonEducationQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(IEnumerable<PersonEducationDto> Response) : PersonEducationQueryResult;
}

public sealed record PersonEducationDto
{
    public required Guid EducationId { get; init; }
    public required Guid EducationCourseId { get; init; }
    public required Guid? EducationCourseInstanceId { get; init; } = null;
    public required EducationSemestrResponseDto Start { get; init; }
    public required EducationSemestrResponseDto? End { get; init; } = null;
}
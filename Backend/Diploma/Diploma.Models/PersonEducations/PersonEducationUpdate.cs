using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.PersonEducations;

public sealed record PersonEducationUpdateRequest
{
    [Required]
    public required EducationSemestrRequestDto Start { get; init; }
    public required EducationSemestrRequestDto? End { get; init; } = null;
}

public abstract record PersonEducationUpdateResult
{
    public sealed record Success : PersonEducationUpdateResult;
    public abstract record Failure : PersonEducationUpdateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record OverLimit(int MaxCount) : Failure;
        public sealed record NotFoundCourseInstance(Guid CourseInstanceId, Guid? CourseId = null) : Failure;
        public sealed record InvalidCourseInstanceDates(DateOnly? StartDate, DateOnly? EndDate) : Failure;
        public sealed record NotFoundCourse(Guid Id) : Failure;
        public sealed record InvalidCourseDates(DateOnly? StartDate, DateOnly? EndDate) : Failure;
    };
}
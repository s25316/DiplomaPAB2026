using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.EducationCourses.Aggregates;

namespace Diploma.Domain.EducationCourseInstances.Aggregates;

public sealed record EducationCourseInstanceId : BaseEntityId<Guid>
{
    public static implicit operator Guid(EducationCourseInstanceId value) => value.Value;
    public static implicit operator EducationCourseInstanceId(Guid value) => new() { Value = value };
}
public sealed class EducationCourseInstance : BaseEntity<EducationCourseInstanceId>
{
    public required EducationCourseId EducationCourseId { get; init; }
    public required DateOnly EducationStartDate { get; init; }
    public required DateOnly? LiquidationDate { get; init; } = null;
}
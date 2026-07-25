using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.EducationDisciplines.ValueObjects;
using Diploma.Domain.EducationInstitutions.Aggregates;

namespace Diploma.Domain.EducationCourses.Aggregates;

public sealed record class EducationCourseId : BaseEntityId<Guid>
{
    public static implicit operator Guid(EducationCourseId value) => value.Value;
    public static implicit operator EducationCourseId(Guid value) => new() { Value = value };
}
public class EducationCourse : BaseEntity<EducationCourseId>
{
    public sealed record CourseDiscipline
    {
        public required EducationDiscipline Discipline { get; init; }
        public required int Percentage { get; init; }
        public required bool IsLeading { get; init; }
    }


    public required EducationInstitutionId EducationInstitutionId { get; init; }
    public required DateOnly? CreationDate { get; init; } = null;
    public required DateOnly? TerminationInitializationDate { get; init; } = null;
    public required DateOnly? LiquidationDate { get; init; } = null;
    public required IList<CourseDiscipline> Disciplines { get; init; } = [];
}
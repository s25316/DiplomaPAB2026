using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.EducationCourseInstances.Aggregates;
using Diploma.Domain.EducationCourses.Aggregates;
using Diploma.Domain.PersonEducations.ValueObjects;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.PersonEducations.Aggregates;

public sealed record PersonEducationId : BaseEntityId<Guid>
{
    public static implicit operator Guid(PersonEducationId value) => value.Value;
    public static implicit operator PersonEducationId(Guid value) => new() { Value = value };
}
public partial class PersonEducation : BaseEntity<PersonEducationId>
{
    public PersonId PersonId { get; protected set; } = null!;
    public PersonEducationId LastSnapshotId { get; protected set; } = null!;
    public EducationCourseId EducationCourseId { get; private set; } = null!;
    public EducationCourseInstanceId? EducationCourseInstanceId { get; private set; } = null!;
    public EducationSemestr Start { get; set; } = null!;
    public EducationSemestr? End { get; set; } = null;


    public static PersonEducation Create(
        PersonId personId,
        EducationCourseId courseId,
        EducationCourseInstanceId? ecourseInstanceId,
        EducationSemestr start,
        EducationSemestr? end)
    {
        var personEducation = new PersonEducation();

        personEducation.PersonId = personId;
        personEducation.EducationCourseId = courseId;
        personEducation.EducationCourseInstanceId = ecourseInstanceId;
        personEducation.Start = start;
        personEducation.End = end;

        return personEducation;
    }
}
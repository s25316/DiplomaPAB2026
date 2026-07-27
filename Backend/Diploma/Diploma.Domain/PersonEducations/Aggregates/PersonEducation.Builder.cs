using Diploma.Domain.Base.Aggregates;
using Diploma.Domain.EducationCourseInstances.Aggregates;
using Diploma.Domain.EducationCourses.Aggregates;
using Diploma.Domain.PersonEducations.ValueObjects;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.PersonEducations.Aggregates;

public partial class PersonEducation : BaseEntity<PersonEducationId>
{
    public class Builder : BaseEntityBulder<PersonEducation, PersonEducationId>
    {
        public Builder WithId(PersonEducationId value)
        {
            With(i => i.Id = value);
            return this;
        }

        public Builder WithLastSnapshotId(PersonEducationId value)
        {
            With(i => i.LastSnapshotId = value);
            return this;
        }

        public Builder WithPersonId(PersonId value)
        {
            With(i => i.PersonId = value);
            return this;
        }

        public Builder WithEducationCourseId(EducationCourseId value)
        {
            With(i => i.EducationCourseId = value);
            return this;
        }

        public Builder WithEducationCourseInstanceId(EducationCourseInstanceId? value)
        {
            With(i => i.EducationCourseInstanceId = value);
            return this;
        }

        public Builder WithStart(EducationSemestr value)
        {
            With(i => i.Start = value);
            return this;
        }

        public Builder WithEnd(EducationSemestr? value)
        {
            With(i => i.End = value);
            return this;
        }
    }
}
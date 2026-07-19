using Diploma.Database.Models.Persons.PersonEvents.Audits;

namespace Diploma.Database.Models.Educations;

public class EducationCourseInstance
{
    public Guid EducationCourseInstanceId { get; set; }


    public Guid EducationCourseId { get; set; }
    public virtual EducationCourse EducationCourse { get; set; } = null!;

    public virtual ICollection<PersonEducation> PersonEducations { get; set; } = [];
}
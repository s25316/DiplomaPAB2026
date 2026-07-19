using Diploma.Database.Models.Persons.PersonEvents.Audits;

namespace Diploma.Database.Models.Educations;

public class EducationCourse
{
    public Guid EducationCourseId { get; set; }


    public Guid EducationInstitutionId { get; set; }
    public virtual EducationInstitution EducationInstitution { get; set; } = null!;

    public virtual ICollection<EducationCourseInstance> EducationCourseInstances { get; set; } = [];
    public virtual ICollection<PersonEducation> PersonEducations { get; set; } = [];
}
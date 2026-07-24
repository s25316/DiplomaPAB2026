using Diploma.Database.Models.Persons.PersonEvents.Audits;

namespace Diploma.Database.Models.Educations;

public class EducationCourse
{
    public Guid EducationCourseId { get; set; }
    public DateOnly? CreationDate { get; set; }
    public DateOnly? TerminationInitializationDate { get; set; } = null;
    public DateOnly? LiquidationDate { get; set; } = null;


    public Guid EducationInstitutionId { get; set; }
    public virtual EducationInstitution EducationInstitution { get; set; } = null!;

    public virtual ICollection<EducationCourseDiscipline> EducationCourseDisciplines { get; set; } = [];
    public virtual ICollection<EducationCourseInstance> EducationCourseInstances { get; set; } = [];
    public virtual ICollection<PersonEducation> PersonEducations { get; set; } = [];
}
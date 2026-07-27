using Diploma.Database.Models.Educations;

namespace Diploma.Database.Models.Persons.PersonEvents.Audits;

public class PersonEducation
{
    public Guid PersonEducationId { get; set; }
    public int YearStart { get; set; }
    public int? YearEnd { get; set; } = null;


    public Guid PersonEventId { get; set; }
    public virtual PersonEvent PersonEvent { get; set; } = null!;

    public int SemesterStartId { get; set; }
    public virtual EducationSemester SemesterStart { get; set; } = null!;

    public int? SemesterEndId { get; set; }
    public virtual EducationSemester? SemesterEnd { get; set; } = null;

    public Guid EducationCourseId { get; set; }
    public virtual EducationCourse EducationCourse { get; set; } = null!;

    public Guid? EducationCourseInstanceId { get; set; } = null;
    public virtual EducationCourseInstance? EducationCourseInstance { get; set; } = null;

    public Guid? RootId { get; set; } = null;
    public virtual PersonEducation? Root { get; set; } = null;
    public virtual ICollection<PersonEducation> History { get; set; } = [];

    public Guid? NextId { get; set; }
    public virtual PersonEducation? Next { get; set; } = null;
    public virtual PersonEducation? Previous { get; set; } = null;
}
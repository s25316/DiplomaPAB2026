using Diploma.Database.Models.Educations;
using Diploma.Database.Models.Persons;
using Diploma.Database.Models.Persons.PersonEvents;
using Diploma.Database.Models.Persons.PersonEvents.Audits;
using Diploma.Database.Models.Persons.PersonOperations;
using Diploma.Database.Models.Projects;
using Diploma.Database.Models.Projects.ProjectEvents;
using Diploma.Database.Models.Projects.ProjectManagers;
using Diploma.Database.Models.Projects.ProjectRoles;
using Diploma.Database.Models.Projects.Recruitments;
using Diploma.Database.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Database;

// DbContextOptions
public class DiplomaDbContext(/*DbContextOptions options*/) : DbContext(/*options*/)
{
    public DbSet<Error> Errors { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<VerificationMethod> VerificationMethods { get; set; }


    public DbSet<EducationCourse> EducationCourses { get; set; }
    public DbSet<EducationSemester> EducationSemesters { get; set; }
    public DbSet<EducationDiscipline> EducationDisciplines { get; set; }
    public DbSet<EducationCourseDiscipline> EducationCourseDisciplines { get; set; }
    public DbSet<EducationCourseInstance> EducationCourseInstances { get; set; }
    public DbSet<EducationInstitution> EducationInstitutions { get; set; }


    public DbSet<Person> People { get; set; }

    public DbSet<PersonEvent> PersonEvents { get; set; }
    public DbSet<PersonEventType> PersonEventTypes { get; set; }

    public DbSet<PersonIdentity> PersonIdentities { get; set; }
    public DbSet<PersonProfile> PersonProfiles { get; set; }
    public DbSet<PersonRefreshToken> PersonRefreshTokens { get; set; }
    public DbSet<PersonEducation> PersonEducations { get; set; }
    public DbSet<PersonEmployment> PersonEmployments { get; set; }
    public DbSet<PersonUri> PersonUris { get; set; }

    public DbSet<PersonOperation> PersonOperations { get; set; }
    public DbSet<PersonOperationType> PersonOperationTypes { get; set; }


    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectData> ProjectDatas { get; set; }

    public DbSet<ProjectEvent> ProjectEvents { get; set; }
    public DbSet<ProjectEventType> ProjectEventTypes { get; set; }

    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<ProjectManagerType> ProjectManagerTypes { get; set; }

    public DbSet<ProjectRole> ProjectRoles { get; set; }
    public DbSet<ProjectRoleData> ProjectRoleDatas { get; set; }
    public DbSet<ProjectRoleEducationDiscipline> ProjectRoleEducationDisciplines { get; set; }
    public DbSet<ProjectRoleEducationInstitution> ProjectRoleEducationInstitutions { get; set; }


    public DbSet<Recruitment> Recruitments { get; set; }
    public DbSet<RecruitmentMessage> RecruitmentMessages { get; set; }
    public DbSet<RecruitmentProjectRole> RecruitmentProjectRoles { get; set; }
    public DbSet<RecruitmentStatus> RecruitmentStatuses { get; set; }
}
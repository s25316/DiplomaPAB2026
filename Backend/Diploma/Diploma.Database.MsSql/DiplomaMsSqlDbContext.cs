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
using Diploma.Database.MsSql.Configurations.Educations;
using Diploma.Database.MsSql.Configurations.Persons;
using Diploma.Database.MsSql.Configurations.Persons.PersonEvents;
using Diploma.Database.MsSql.Configurations.Persons.PersonEvents.Audits;
using Diploma.Database.MsSql.Configurations.Persons.PersonOperations;
using Diploma.Database.MsSql.Configurations.Projects;
using Diploma.Database.MsSql.Configurations.Projects.ProjectEvents;
using Diploma.Database.MsSql.Configurations.Projects.ProjectManagers;
using Diploma.Database.MsSql.Configurations.Projects.ProjectRoles;
using Diploma.Database.MsSql.Configurations.Projects.Recruitments;
using Diploma.Database.MsSql.Configurations.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Database.MsSql;

/*
PowerShell
dotnet tool install --global dotnet-ef

dotnet add Diploma.Database.MsSql package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add First `
  --project Diploma.Database.MsSql `
  --startup-project Diploma.API `
  --context DiplomaMsSqlDbContext `
  --framework net10.0

dotnet ef database update `
  --project Diploma.Database.MsSql `
  --startup-project Diploma.API `
  --context DiplomaMsSqlDbContext `
  --framework net10.0
 */

// Add-Migration First -Project Diploma.Database.MsSql -Context DiplomaMsSqlDbContext
// Update-Database -Project Diploma.Database.MsSql -Context DiplomaMsSqlDbContext
public class DiplomaMsSqlDbContext(/*DbContextOptions options*/) : DiplomaDbContext(/*options*/)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost,1436;Initial Catalog=Diploma;User ID=sa;Password=YourStrong!Passw0rd;Trust Server Certificate=True");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Error>(new ErrorEFConfiguration());
        modelBuilder.ApplyConfiguration<EmailMessage>(new EmailMessageEFConfiguration());
        modelBuilder.ApplyConfiguration<VerificationMethod>(new VerificationMethodEFConfiguration());


        modelBuilder.ApplyConfiguration<EducationCourse>(new EducationCourseEFConfiguration());
        modelBuilder.ApplyConfiguration<EducationSemester>(new EducationSemesterEFConfiguration());
        modelBuilder.ApplyConfiguration<EducationDiscipline>(new EducationDisciplineEFConfiguration());
        modelBuilder.ApplyConfiguration<EducationCourseDiscipline>(new EducationCourseDisciplineEFConfiguration());
        modelBuilder.ApplyConfiguration<EducationCourseInstance>(new EducationCourseInstanceEFConfiguration());
        modelBuilder.ApplyConfiguration<EducationInstitution>(new EducationInstitutionEFConfiguration());


        modelBuilder.ApplyConfiguration<Person>(new PersonEFConfiguration());

        modelBuilder.ApplyConfiguration<PersonEvent>(new PersonEventEFConfiguration());
        modelBuilder.ApplyConfiguration<PersonEventType>(new PersonEventTypeEFConfiguration());

        modelBuilder.ApplyConfiguration<PersonIdentity>(new PersonIdentityEFConfiguration());
        modelBuilder.ApplyConfiguration<PersonProfile>(new PersonProfileEFConfiguration());
        modelBuilder.ApplyConfiguration<PersonRefreshToken>(new PersonRefreshTokenEFConfiguration());
        modelBuilder.ApplyConfiguration<PersonEducation>(new PersonEducationEFConfiguration());
        modelBuilder.ApplyConfiguration<PersonEmployment>(new PersonEmploymentEFConfiguration());
        modelBuilder.ApplyConfiguration<PersonUri>(new PersonUriEFConfiguration());

        modelBuilder.ApplyConfiguration<PersonOperation>(new PersonOperationEFConfiguration());
        modelBuilder.ApplyConfiguration<PersonOperationType>(new PersonOperationTypeEFConfiguration());


        modelBuilder.ApplyConfiguration<Project>(new ProjectEFConfiguration());

        modelBuilder.ApplyConfiguration<ProjectEvent>(new ProjectEventEFConfiguration());
        modelBuilder.ApplyConfiguration<ProjectEventType>(new ProjectEventTypeEFConfiguration());

        modelBuilder.ApplyConfiguration<ProjectManager>(new ProjectManagerEFConfiguration());
        modelBuilder.ApplyConfiguration<ProjectManagerType>(new ProjectManagerTypeEFConfiguration());

        modelBuilder.ApplyConfiguration<ProjectRole>(new ProjectRoleEFConfiguration());
        modelBuilder.ApplyConfiguration<ProjectRoleEducationCourseDiscipline>(new ProjectRoleEducationCourseDisciplineEFConfiguration());
        modelBuilder.ApplyConfiguration<ProjectRoleEducationInstitution>(new ProjectRoleEducationInstitutionEFConfiguration());

        modelBuilder.ApplyConfiguration<Recruitment>(new RecruitmentEFConfiguration());
        modelBuilder.ApplyConfiguration<RecruitmentMessage>(new RecruitmentMessageEFConfiguration());
        modelBuilder.ApplyConfiguration<RecruitmentProjectRole>(new RecruitmentProjectRoleEFConfiguration());
        modelBuilder.ApplyConfiguration<RecruitmentStatus>(new RecruitmentStatusEFConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
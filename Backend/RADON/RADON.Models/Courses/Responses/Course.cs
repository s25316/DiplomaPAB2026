using RADON.Models.Dictionaries.Responses;
using System.ComponentModel.DataAnnotations;
using Response = RADON.Models.Descriptions.Courses.Response;

namespace RADON.Models.Courses.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="Course"]/summary' />
[Display(Name = nameof(Response.Course), ResourceType = typeof(Response))]
public sealed class Course
{
    /// <include file='Response.xml' path='docs/members/member[@name="DisciplineData"]/summary' />
    [Display(Name = nameof(Response.DisciplineData), ResourceType = typeof(Response))]
    public record DisciplineData
    {
        /// <include file='Response.xml' path='docs/members/member[@name="DisciplineData_Discipline"]/summary' />
        [Display(Name = nameof(Response.DisciplineData_Discipline), ResourceType = typeof(Response))]
        public required DictionaryItem Discipline { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="DisciplineData_Percentage"]/summary' />
        [Display(Name = nameof(Response.DisciplineData_Percentage), ResourceType = typeof(Response))]
        public required int Percentage { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="DisciplineData_IsLeading"]/summary' />
        [Display(Name = nameof(Response.DisciplineData_IsLeading), ResourceType = typeof(Response))]
        public required bool IsLeading { get; init; }
    };


    /// <include file='Response.xml' path='docs/members/member[@name="Course_CourseUuid"]/summary' />
    [Display(Name = nameof(Response.Course_CourseUuid), ResourceType = typeof(Response))]
    public required Guid CourseUuid { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="Course_Name"]/summary' />
    [Display(Name = nameof(Response.Course_Name), ResourceType = typeof(Response))]
    public required string Name { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="Course_InstitutionUuid"]/summary' />
    [Display(Name = nameof(Response.Course_InstitutionUuid), ResourceType = typeof(Response))]
    public required Guid InstitutionUuid { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="Course_CreationDate"]/summary' />
    [Display(Name = nameof(Response.Course_CreationDate), ResourceType = typeof(Response))]
    public required DateOnly? CreationDate { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="Course_TerminationInitializationDate"]/summary' />
    [Display(Name = nameof(Response.Course_TerminationInitializationDate), ResourceType = typeof(Response))]
    public required DateOnly? TerminationInitializationDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="Course_LiquidationDate"]/summary' />
    [Display(Name = nameof(Response.Course_LiquidationDate), ResourceType = typeof(Response))]
    public required DateOnly? LiquidationDate { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="Course_IsTeacherTraining"]/summary' />
    [Display(Name = nameof(Response.Course_IsTeacherTraining), ResourceType = typeof(Response))]
    public required bool IsTeacherTraining { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="Course_IsPhilological"]/summary' />
    [Display(Name = nameof(Response.Course_IsPhilological), ResourceType = typeof(Response))]
    public required bool IsPhilological { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="Course_Level"]/summary' />
    [Display(Name = nameof(Response.Course_Level), ResourceType = typeof(Response))]
    public required DictionaryItem Level { get; init; } = null!;

    /// <include file='Response.xml' path='docs/members/member[@name="Course_Profile"]/summary' />
    [Display(Name = nameof(Response.Course_Profile), ResourceType = typeof(Response))]
    public required DictionaryItem Profile { get; init; } = null!;

    /// <include file='Response.xml' path='docs/members/member[@name="Course_Isced"]/summary' />
    [Display(Name = nameof(Response.Course_Isced), ResourceType = typeof(Response))]
    public required DictionaryItem Isced { get; init; } = null!;

    /// <include file='Response.xml' path='docs/members/member[@name="Course_Status"]/summary' />
    [Display(Name = nameof(Response.Course_Status), ResourceType = typeof(Response))]
    public required DictionaryItem Status { get; init; } = null!;


    /// <include file='Response.xml' path='docs/members/member[@name="Course_Disciplines"]/summary' />
    [Display(Name = nameof(Response.Course_Disciplines), ResourceType = typeof(Response))]
    public required ICollection<DisciplineData> Disciplines { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="Course_CourseInstances"]/summary' />
    [Display(Name = nameof(Response.Course_CourseInstances), ResourceType = typeof(Response))]
    public required ICollection<CourseInstance> CourseInstances { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="Course_LastRefresh"]/summary' />
    [Display(Name = nameof(Response.Course_LastRefresh), ResourceType = typeof(Response))]
    public required DateTimeOffset LastRefresh { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="Course_SourceLastRefresh"]/summary' />
    [Display(Name = nameof(Response.Course_SourceLastRefresh), ResourceType = typeof(Response))]
    public required DateTimeOffset SourceLastRefresh { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="Course_DataSource"]/summary' />
    [Display(Name = nameof(Response.Course_DataSource), ResourceType = typeof(Response))]
    public required string DataSource { get; init; } = null!;
}
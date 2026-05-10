using HotChocolate;
using Microsoft.AspNetCore.Mvc;
using RADON.Models.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using QueryParameter = RADON.Models.Descriptions.Courses.QueryParameter;

namespace RADON.Models.Courses;

/// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters"]/summary' />
[Display(Name = nameof(QueryParameter.QueryParameters), ResourceType = typeof(QueryParameter))]
[GraphQLName("CourseQueryParameters")]
public sealed class QueryParameters : BaseQueryParameters
{
    [GraphQLName("CourseQueryParametersOrderBy")]
    public enum QueryParametersOrderBy
    {
        Name = 1,
        CreationDate = 2,
        InstanceStartDate = 3,
    }


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_CourseUuids"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_CourseUuids), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "CourseUuid")]
    public ICollection<Guid> CourseUuids { get; set; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_InstitutionUuids"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_InstitutionUuids), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "InstitutionUuid")]
    public ICollection<Guid> InstitutionUuids { get; set; } = [];


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_Name"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_Name), ResourceType = typeof(QueryParameter))]
    public string? Name { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_IsTeacherTraining"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_IsTeacherTraining), ResourceType = typeof(QueryParameter))]
    public bool? IsTeacherTraining { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_IsPhilological"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_IsPhilological), ResourceType = typeof(QueryParameter))]
    public bool? IsPhilological { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_LevelCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_LevelCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "LevelCode")]
    public ICollection<string> LevelCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_ProfileCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_ProfileCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "ProfileCode")]
    public ICollection<string> ProfileCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_IscedCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_IscedCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "IscedCode")]
    public ICollection<string> IscedCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_StatusCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_StatusCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "StatusCode")]
    public ICollection<string> StatusCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_DisciplineCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_DisciplineCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "DisciplineCode")]
    public ICollection<string> DisciplineCodes { get; init; } = [];



    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_CourseInstanceUuids"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_CourseInstanceUuids), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "CourseInstanceUuid")]
    public ICollection<Guid> CourseInstanceUuids { get; set; } = [];


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_IsDual"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_IsDual), ResourceType = typeof(QueryParameter))]
    public bool? IsDual { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_IsBridging"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_IsBridging), ResourceType = typeof(QueryParameter))]
    public bool? IsBridging { get; init; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_IsCoopWithVocational"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_IsCoopWithVocational), ResourceType = typeof(QueryParameter))]
    public bool? IsCoopWithVocational { get; init; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_FormCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_FormCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "FormCode")]
    public ICollection<string> FormCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_ProfessionalTitleCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_ProfessionalTitleCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "ProfessionalTitleCode")]
    public ICollection<string> ProfessionalTitleCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_LanguageCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_LanguageCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "LanguageCode")]
    public ICollection<string> LanguageCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_InstanceStatusCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_InstanceStatusCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "InstanceStatusCode")]
    public ICollection<string> InstanceStatusCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_PhilologicalLanguageCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_PhilologicalLanguageCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "PhilologicalLanguageCode")]
    public ICollection<string> PhilologicalLanguageCodes { get; init; } = [];


    [DefaultValue(QueryParametersOrderBy.Name)]
    public required QueryParametersOrderBy OrderBy { get; init; } = QueryParametersOrderBy.Name;

    [DefaultValue(Order.Ascending)]
    public required Order Order { get; init; } = Order.Ascending;
}
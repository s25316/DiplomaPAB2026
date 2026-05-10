using Microsoft.AspNetCore.Mvc;
using RADON.Models.Shared;
using System.ComponentModel;

namespace RADON.Models.Courses;

public sealed class QueryParameters : BaseQueryParameters
{
    public enum QueryParametersOrderBy
    {
        Name = 1,
        CreationDate = 2,
        InstanceStartDate = 3,
    }


    [FromQuery(Name = "CourseUuid")]
    public ICollection<Guid> CourseUuids { get; set; } = [];

    [FromQuery(Name = "InstitutionUuid")]
    public ICollection<Guid> InstitutionUuids { get; set; } = [];

    public string? Name { get; init; } = null;

    public bool? IsTeacherTraining { get; init; } = null;
    public bool? IsPhilological { get; init; } = null;

    [FromQuery(Name = "LevelCode")]
    public ICollection<string> LevelCodes { get; init; } = [];

    [FromQuery(Name = "ProfileCode")]
    public ICollection<string> ProfileCodes { get; init; } = [];

    [FromQuery(Name = "IscedCode")]
    public ICollection<string> IscedCodes { get; init; } = [];

    [FromQuery(Name = "StatusCode")]
    public ICollection<string> StatusCodes { get; init; } = [];

    [FromQuery(Name = "DisciplineCode")]
    public ICollection<string> DisciplineCodes { get; init; } = [];



    [FromQuery(Name = "CourseInstanceUuid")]
    public ICollection<Guid> CourseInstanceUuids { get; set; } = [];

    public bool? IsDual { get; init; } = null;
    public bool? IsBridging { get; init; } = null;
    public bool? IsCoopWithVocational { get; init; } = null;

    [FromQuery(Name = "FormCode")]
    public ICollection<string> FormCodes { get; init; } = [];

    [FromQuery(Name = "ProfessionalTitleCode")]
    public ICollection<string> ProfessionalTitleCodes { get; init; } = [];

    [FromQuery(Name = "LanguageCode")]
    public ICollection<string> LanguageCodes { get; init; } = [];

    [FromQuery(Name = "InstanceStatusCode")]
    public ICollection<string> InstanceStatusCodes { get; init; } = [];

    [FromQuery(Name = "PhilologicalLanguageCode")]
    public ICollection<string> PhilologicalLanguageCodes { get; init; } = [];


    [DefaultValue(QueryParametersOrderBy.Name)]
    public required QueryParametersOrderBy OrderBy { get; init; } = QueryParametersOrderBy.Name;

    [DefaultValue(Order.Ascending)]
    public required Order Order { get; init; } = Order.Ascending;
}
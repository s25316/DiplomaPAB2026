using Base.Models.ValueObjects.Krsy;
using Base.Models.ValueObjects.Nipy;
using Base.Models.ValueObjects.Regony;
using Microsoft.AspNetCore.Mvc;
using RADON.Models.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using QueryParameter = RADON.Models.Descriptions.Institutions.QueryParameter;

namespace RADON.Models.Institutions;

/// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters"]/summary' />
[Display(Name = nameof(QueryParameter.QueryParameters), ResourceType = typeof(QueryParameter))]
public sealed class QueryParameters : BaseQueryParameters
{
    public enum QueryParametersOrderBy
    {
        Name = 1,
        StartDate = 2,
    }


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_InstitutionUuids"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_InstitutionUuids), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "InstitutionUuid")]
    public ICollection<Guid> InstitutionUuids { get; set; } = [];



    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_Name"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_Name), ResourceType = typeof(QueryParameter))]
    public string? Name { get; set; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_Regon"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_Regon), ResourceType = typeof(QueryParameter))]
    public Regon? Regon { get; set; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_Nip"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_Nip), ResourceType = typeof(QueryParameter))]
    public Nip? Nip { get; set; } = null;

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_Krs"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_Krs), ResourceType = typeof(QueryParameter))]
    public Krs? Krs { get; set; } = null;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_KindCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_KindCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "KindCode")]
    public ICollection<string> KindCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_UniversityTypeCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_UniversityTypeCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "UniversityTypeCode")]
    public ICollection<string> UniversityTypeCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_ScientificInstitutionTypeCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_ScientificInstitutionTypeCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "ScientificInstitutionTypeCode")]
    public ICollection<string> ScientificInstitutionTypeCodes { get; init; } = [];

    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_StatusCodes"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_StatusCodes), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "StatusCode")]
    public ICollection<string> StatusCodes { get; init; } = [];


    [DefaultValue(QueryParametersOrderBy.Name)]
    public required QueryParametersOrderBy OrderBy { get; init; } = QueryParametersOrderBy.Name;

    [DefaultValue(Order.Ascending)]
    public required Order Order { get; init; } = Order.Ascending;
}
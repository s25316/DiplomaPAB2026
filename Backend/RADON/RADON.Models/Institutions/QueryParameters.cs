using Base.Models.ValueObjects.Krsy;
using Base.Models.ValueObjects.Nipy;
using Base.Models.ValueObjects.Regony;
using Microsoft.AspNetCore.Mvc;
using RADON.Models.Shared;
using System.ComponentModel;

namespace RADON.Models.Institutions;

public sealed class QueryParameters : BaseQueryParameters
{
    public enum QueryParametersOrderBy
    {
        Name = 1,
        StartDate = 2,
    }


    [FromQuery(Name = "InstitutionUuid")]
    public ICollection<Guid> InstitutionUuids { get; set; } = [];

    public string? Name { get; set; } = null;
    public Regon? Regon { get; set; } = null;
    public Nip? Nip { get; set; } = null;
    public Krs? Krs { get; set; } = null;


    [FromQuery(Name = "KindCode")]
    public ICollection<string> KindCodes { get; init; } = [];

    [FromQuery(Name = "UniversityTypeCode")]
    public ICollection<string> UniversityTypeCodes { get; init; } = [];

    [FromQuery(Name = "ScientificInstitutionTypeCode")]
    public ICollection<string> ScientificInstitutionTypeCodes { get; init; } = [];

    [FromQuery(Name = "StatusCode")]
    public ICollection<string> StatusCodes { get; init; } = [];


    [DefaultValue(QueryParametersOrderBy.Name)]
    public required QueryParametersOrderBy OrderBy { get; init; } = QueryParametersOrderBy.Name;

    [DefaultValue(Order.Ascending)]
    public required Order Order { get; init; } = Order.Ascending;
}
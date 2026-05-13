using Base.Models.ValueObjects.Regony;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using QueryParameter = GUS.REGON.Models.Descriptions.QueryParameter;

namespace GUS.REGON.Models;

/// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters"]/summary' />
[Display(Name = nameof(QueryParameter.QueryParameters), ResourceType = typeof(QueryParameter))]
public sealed class QueryParameters
{
    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParameters_Regons"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParameters_Regons), ResourceType = typeof(QueryParameter))]
    [FromQuery(Name = "Regon")]
    public ICollection<Regon> Regons { get; init; } = [];
}
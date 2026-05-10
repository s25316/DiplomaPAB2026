using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using QueryParameter = RADON.Models.Descriptions.Shared.QueryParameter;

namespace RADON.Models.Shared;

public enum Order
{
    Ascending = 1,
    Descending = 2,
}

/// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParametersPagination"]/summary' />
[Display(Name = nameof(QueryParameter.QueryParametersPagination), ResourceType = typeof(QueryParameter))]
public record QueryParametersPagination
{
    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParametersPagination_Page"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParametersPagination_Page), ResourceType = typeof(QueryParameter))]
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;


    /// <include file='QueryParameter.xml' path='docs/members/member[@name="QueryParametersPagination_ItemsPerPage"]/summary' />
    [Display(Name = nameof(QueryParameter.QueryParametersPagination_ItemsPerPage), ResourceType = typeof(QueryParameter))]
    [DefaultValue(100)]
    public int ItemsPerPage { get; init; } = 100;
}

/// <include file='QueryParameter.xml' path='docs/members/member[@name="BaseQueryParameters"]/summary' />
[Display(Name = nameof(QueryParameter.BaseQueryParameters), ResourceType = typeof(QueryParameter))]
public abstract class BaseQueryParameters
{
    /// <include file='QueryParameter.xml' path='docs/members/member[@name="BaseQueryParameters_Pagination"]/summary' />
    [Display(Name = nameof(QueryParameter.BaseQueryParameters_Pagination), ResourceType = typeof(QueryParameter))]
    public QueryParametersPagination Pagination { get; init; } = new QueryParametersPagination();
}
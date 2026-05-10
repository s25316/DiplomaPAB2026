using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Response = RADON.Models.Descriptions.Shared.Response;

namespace RADON.Models.Shared;

/// <include file='Response.xml' path='docs/members/member[@name="ResponsePagination"]/summary' />
[Display(Name = nameof(Response.ResponsePagination), ResourceType = typeof(Response))]
public record ResponsePagination
{
    /// <include file='Response.xml' path='docs/members/member[@name="ResponsePagination_Page"]/summary' />
    [Display(Name = nameof(Response.ResponsePagination_Page), ResourceType = typeof(Response))]
    [DefaultValue(1)]
    public required int Page { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="ResponsePagination_ItemsPerPage"]/summary' />
    [Display(Name = nameof(Response.ResponsePagination_ItemsPerPage), ResourceType = typeof(Response))]
    [DefaultValue(100)]
    public required int ItemsPerPage { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="ResponsePagination_TotalCount"]/summary' />
    [Display(Name = nameof(Response.ResponsePagination_TotalCount), ResourceType = typeof(Response))]
    [DefaultValue(0)]
    public required int TotalCount { get; init; }
}

/// <include file='Response.xml' path='docs/members/member[@name="ResponseT"]/summary' />
[Display(Name = nameof(Response.ResponseT), ResourceType = typeof(Response))]
public sealed class Response<TItem>
    where TItem : class
{
    /// <include file='Response.xml' path='docs/members/member[@name="Response_Items"]/summary' />
    [Display(Name = nameof(Response.Response_Items), ResourceType = typeof(Response))]
    public required IList<TItem> Items { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="Response_Pagination"]/summary' />
    [Display(Name = nameof(Response.Response_Pagination), ResourceType = typeof(Response))]
    public required ResponsePagination Pagination { get; init; }
}
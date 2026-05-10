using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RADON.Models.Shared;

public enum Order
{
    Ascending = 1,
    Descending = 2,
}

public record QueryParametersPagination
{
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [DefaultValue(100)]
    public int ItemsPerPage { get; init; } = 100;
}

public abstract class BaseQueryParameters
{
    public QueryParametersPagination Pagination { get; init; } = new QueryParametersPagination();
}
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
    public required int Page { get; init; } = 1;

    [DefaultValue(100)]
    public required int ItemsPerPage { get; init; } = 100;
}

public abstract class BaseQueryParameters
{
    public required QueryParametersPagination Pagination { get; init; }
}
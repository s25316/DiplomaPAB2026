using Base.Models.ValidationAttributes;
using System.ComponentModel;

namespace Base.Models.Interfaces.Repositories;

public class Pagination
{
    [Page]
    [DefaultValue(1)]
    public int Page { init; get; } = 1;

    [ItemsPerPage]
    [DefaultValue(100)]
    public int ItemsPerPage { init; get; } = 100;
}
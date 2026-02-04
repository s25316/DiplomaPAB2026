using Base.Models.ValidationAttributes;
using System.ComponentModel;

namespace Base.Models;

public class Pagination
{
    [Page]
    [DefaultValue(1)]
    public int Page { set; get; } = 1;

    [ItemsPerPage]
    [DefaultValue(100)]
    public int ItemsPerPage { set; get; } = 100;
}
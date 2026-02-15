namespace Base.Models.Interfaces.Repositories;

public class Pagination
{
    public int Page { init; get; } = 1;

    public int ItemsPerPage { init; get; } = 100;
}
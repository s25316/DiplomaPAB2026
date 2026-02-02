namespace GUS.TERYT.Models;

public sealed class Response<T>
{
    public IEnumerable<T> Items { get; init; } = [];
    public int TotalCount { get; init; } = 0;
}

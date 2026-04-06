using GUS.REGON.Models.Responses.Enums;

namespace GUS.REGON.Models.Results;

public static class RegonBaseResult
{
    public static RegonBaseResult<TItem> Success<TItem>(TItem result)
        => RegonBaseResult<TItem>.Success(result);

    public static RegonBaseResult<TItem> Failed<TItem>(StatusUslugi statusUslugi)
        => RegonBaseResult<TItem>.Failed(statusUslugi);
}

public class RegonBaseResult<TItem> : Result<TItem>
{
    public StatusUslugi StatusUslugi { get; }

    protected RegonBaseResult(TItem? value, StatusUslugi statusUslugi) : base(value)
    {
        StatusUslugi = statusUslugi;
    }


    public static RegonBaseResult<TItem> Success(TItem result)
        => new(result, StatusUslugi.UslugaDostepna);

    public static RegonBaseResult<TItem> Failed(StatusUslugi statusUslugi = StatusUslugi.UslugaDostepna)
        => new(default, statusUslugi);
}
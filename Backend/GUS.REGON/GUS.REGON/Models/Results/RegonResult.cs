using GUS.REGON.Models.Responses.Enums;

namespace GUS.REGON.Models.Results;

public static class RegonResult
{
    public static RegonResult<TItem> Success<TItem>(TItem result)
        => RegonResult<TItem>.Success(result);

    public static RegonResult<TItem> Failed<TItem>(KomunikatKod? komunikatKod, StatusUslugi statusUslugi = StatusUslugi.UslugaDostepna)
        => RegonResult<TItem>.Failed(komunikatKod, statusUslugi);
}

public class RegonResult<TItem> : RegonBaseResult<TItem>
{
    public override bool IsSuccess => StatusUslugi == StatusUslugi.UslugaDostepna && KomunikatKod is null;
    public KomunikatKod? KomunikatKod { get; }


    protected RegonResult(TItem? value, KomunikatKod? komunikatKod, StatusUslugi statusUslugi = StatusUslugi.UslugaDostepna) : base(value, statusUslugi)
    {
        KomunikatKod = komunikatKod;
    }


    public static new RegonResult<TItem> Success(TItem result)
        => new(result, null, StatusUslugi.UslugaDostepna);

    public static RegonResult<TItem> Failed(KomunikatKod? komunikatKod, StatusUslugi statusUslugi = StatusUslugi.UslugaDostepna)
        => new(default, komunikatKod, statusUslugi);
}
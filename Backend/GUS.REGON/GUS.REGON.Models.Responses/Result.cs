namespace GUS.REGON.Models.Responses;

using Base.Models.ValueObjects.Regony;

public class Result
{
    public required Regon Regon { get; init; }
    public Report? Report { get; init; } = null;
}
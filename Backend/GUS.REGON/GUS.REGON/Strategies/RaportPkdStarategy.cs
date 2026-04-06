using GUS.REGON.Configurations;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.Models.Results;

namespace GUS.REGON.Strategies;

internal class RaportPkdStarategy
{
    internal Result<Report> GetReport(TypJednostki typ)
    {
        return (typ) switch
        {
            TypJednostki.F => Result<Report>.Success(Reports.DzialalnosciFizycznej),
            TypJednostki.P => Result<Report>.Success(Reports.DzialalnosciPrawnej),
            TypJednostki.LF => Result<Report>.Success(Reports.DzialalnosciLokalnejFizycznej),
            TypJednostki.LP => Result<Report>.Success(Reports.DzialalnosciLokalnejPrawnej),
            _ => Result<Report>.Failed(),
        };
    }
}
using GUS.REGON.Configurations;
using GUS.REGON.Models.Responses.Enums;
using GUS.REGON.Models.Results;

namespace GUS.REGON.Strategies;

internal class RaportJednostkiStrategy
{
    internal Result<Report> GetReport(TypJednostki typ, int? silosId)
    {
        return (typ, silosId) switch
        {
            { typ: TypJednostki.F, silosId: 1 } => Result<Report>.Success(Reports.DzialalnoscFizycznejCeidg),
            { typ: TypJednostki.F, silosId: 2 } => Result<Report>.Success(Reports.DzialalnoscFizycznejRolnicza),
            { typ: TypJednostki.F, silosId: 3 } => Result<Report>.Success(Reports.DzialalnoscFizycznejPozostala),
            { typ: TypJednostki.F, silosId: 4 } => Result<Report>.Success(Reports.DzialalnoscFizycznejWKrupgn),
            { typ: TypJednostki.P } => Result<Report>.Success(Reports.Prawna),
            { typ: TypJednostki.LF } => Result<Report>.Success(Reports.LokalnaFizycznej),
            { typ: TypJednostki.LP } => Result<Report>.Success(Reports.LokalnaPrawnej),
            _ => Result<Report>.Failed(),
        };
    }
}
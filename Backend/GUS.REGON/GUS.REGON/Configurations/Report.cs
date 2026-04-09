// Ignore Spelling: Raport, Raporty, Dzialalnosci, Wspolnicy
// Ignore Spelling: Fizycznej,  Lokalno Prawnej, 

namespace GUS.REGON.Configurations;

internal abstract record Report(string Value, string Description)
{
    public sealed record TypJednostki() : Report("PublDaneRaportTypJednostki", Messages.PublDaneRaportTypJednostki);


    #region Raporty Dzialalnosci Fizycznej
    public sealed record FizycznaOsoba() : Report("PublDaneRaportFizycznaOsoba", Messages.PublDaneRaportFizycznaOsoba);
    public sealed record LokalneFizycznej() : Report("PublDaneRaportLokalneFizycznej", Messages.PublDaneRaportLokalneFizycznej); // List

    /// <summary>
    /// Raport Dzialalnosci Fizycznej PKD
    /// </summary>
    public sealed record DzialalnosciFizycznej() : Report("PublDaneRaportDzialalnosciFizycznej", Messages.PublDaneRaportDzialalnosciFizycznej);

    /// <summary>
    /// Raport Dzialalnosci Fizycznej, SilosId 1
    /// </summary>
    public sealed record DzialalnoscFizycznejCeidg() : Report("PublDaneRaportDzialalnoscFizycznejCeidg", Messages.PublDaneRaportDzialalnoscFizycznejCeidg);

    /// <summary>
    /// Raport Dzialalnosci Fizycznej, SilosId 2
    /// </summary>
    public sealed record DzialalnoscFizycznejRolnicza() : Report("PublDaneRaportDzialalnoscFizycznejRolnicza", Messages.PublDaneRaportDzialalnoscFizycznejRolnicza);

    /// <summary>
    /// Raport Dzialalnosci Fizycznej, SilosId 3
    /// </summary>
    public sealed record DzialalnoscFizycznejPozostala() : Report("PublDaneRaportDzialalnoscFizycznejPozostala", Messages.PublDaneRaportDzialalnoscFizycznejPozostala);

    /// <summary>
    /// Raport Dzialalnosci Fizycznej, SilosId 4
    /// </summary>
    public sealed record DzialalnoscFizycznejWKrupgn() : Report("PublDaneRaportDzialalnoscFizycznejWKrupgn", Messages.PublDaneRaportDzialalnoscFizycznejWKrupgn);
    #endregion


    #region  Raporty Dzialalnosci Prawnej
    /// <summary>
    /// Raport Dzialalnosci Prawnej PKD
    /// </summary>
    public sealed record DzialalnosciPrawnej() : Report("PublDaneRaportDzialalnosciPrawnej", Messages.PublDaneRaportDzialalnosciPrawnej);

    /// <summary>
    /// Raport Dzialalnosci Prawnej
    /// </summary>
    public sealed record Prawna() : Report("PublDaneRaportPrawna", Messages.PublDaneRaportPrawna);
    public sealed record LokalnePrawnej() : Report("PublDaneRaportLokalnePrawnej", Messages.PublDaneRaportLokalnePrawnej); // List
    public sealed record WspolnicyPrawnej() : Report("PublDaneRaportWspolnicyPrawnej", Messages.PublDaneRaportWspolnicyPrawnej); // Wspolnicy
    #endregion


    #region Raporty Dzialalnosci Lokalno Fizycznej
    /// <summary>
    /// Raport Dzialalnosci Lokalno Fizycznej PKD
    /// </summary>
    public sealed record DzialalnosciLokalnejFizycznej() : Report("PublDaneRaportDzialalnosciLokalnejFizycznej", Messages.PublDaneRaportDzialalnosciLokalnejFizycznej);

    /// <summary>
    /// Raport Dzialalnosci Lokalno Fizycznej 
    /// </summary>
    public sealed record LokalnaFizycznej() : Report("PublDaneRaportLokalnaFizycznej", Messages.PublDaneRaportLokalnaFizycznej);
    #endregion


    #region Raporty Dzialalnosci Lokalno Prawnej
    /// <summary>
    /// Raport Dzialalnosci Lokalno Prawnej PKD
    /// </summary>
    public sealed record DzialalnosciLokalnejPrawnej() : Report("PublDaneRaportDzialalnosciLokalnejPrawnej", Messages.PublDaneRaportDzialalnosciLokalnejPrawnej);

    /// <summary>
    /// Raport Dzialalnosci Lokalno Prawnej
    /// </summary>
    public sealed record LokalnaPrawnej() : Report("PublDaneRaportLokalnaPrawnej", Messages.PublDaneRaportLokalnaPrawnej);
    #endregion
}

internal static class Reports
{
    public static readonly Report.TypJednostki DzialalnosTypJednostkiciPrawnej = new();


    #region Raporty Dzialalnosci Fizycznej
    public static readonly Report.FizycznaOsoba DzialalnosFizycznaOsobaciPrawnej = new();
    public static readonly Report.LokalneFizycznej LokalneFizycznej = new(); // List

    /// <summary>
    /// Raport Dzialalnosci Fizycznej PKD
    /// </summary>
    public static readonly Report.DzialalnosciFizycznej DzialalnosciFizycznej = new();

    /// <summary>
    /// Raport Dzialalnosci Fizycznej, SilosId 1
    /// </summary>
    public static readonly Report.DzialalnoscFizycznejCeidg DzialalnoscFizycznejCeidg = new();

    /// <summary>
    /// Raport Dzialalnosci Fizycznej, SilosId 2
    /// </summary>
    public static readonly Report.DzialalnoscFizycznejRolnicza DzialalnoscFizycznejRolnicza = new();

    /// <summary>
    /// Raport Dzialalnosci Fizycznej, SilosId 3
    /// </summary>
    public static readonly Report.DzialalnoscFizycznejPozostala DzialalnoscFizycznejPozostala = new();

    /// <summary>
    /// Raport Dzialalnosci Fizycznej, SilosId 4
    /// </summary>
    public static readonly Report.DzialalnoscFizycznejWKrupgn DzialalnoscFizycznejWKrupgn = new();
    #endregion


    #region  Raporty Dzialalnosci Lokalno Prawnej
    /// <summary>
    /// Raport Dzialalnosci Prawnej PKD
    /// </summary>
    public static readonly Report.DzialalnosciPrawnej DzialalnosciPrawnej = new();

    /// <summary>
    /// Raport Dzialalnosci Prawnej
    /// </summary>
    public static readonly Report.Prawna Prawna = new();
    public static readonly Report.LokalnePrawnej LokalnePrawnej = new();// List
    public static readonly Report.WspolnicyPrawnej WspolnicyPrawnej = new(); // Wspolnicy
    #endregion


    #region Raporty Dzialalnosci Lokalno Fizycznej
    /// <summary>
    /// Raport Dzialalnosci Lokalno Fizycznej PKD
    /// </summary>
    public static readonly Report.DzialalnosciLokalnejFizycznej DzialalnosciLokalnejFizycznej = new();

    /// <summary>
    /// Raport Dzialalnosci Lokalno Fizycznej 
    /// </summary>
    public static readonly Report.LokalnaFizycznej LokalnaFizycznej = new();
    #endregion


    #region Raporty Dzialalnosci Lokalno Prawnej
    /// <summary>
    /// Raport Dzialalnosci Lokalno Prawnej PKD
    /// </summary>
    public static readonly Report.DzialalnosciLokalnejPrawnej DzialalnosciLokalnejPrawnej = new();

    /// <summary>
    /// Raport Dzialalnosci Lokalno Prawnej
    /// </summary>
    public static readonly Report.LokalnaPrawnej LokalnaPrawnej = new();
    #endregion
}
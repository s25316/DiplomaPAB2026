namespace GUS.TERYT.Database.MsSql;

public static class DefaultValue
{
    public const string GUID = "newid()";
    public const string DATE = "CONVERT(date, GETDATE())";
    public const string DATE_TIME = "GETDATE()";

    public const int LENGTH_10 = 10;
    public const int LENGTH_100 = 100;
}

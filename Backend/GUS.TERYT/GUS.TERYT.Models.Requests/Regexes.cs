// Ignore Spelling: Regexes, Wojewodztwo, Powiat, Gmina, Miejscowosc, ULICA
using System.Text.RegularExpressions;

namespace GUS.TERYT.Models.Requests;

public static class Regexes
{
    private const string REGEX_WOJEWODZTWO = @"^[0-9]{2}$";
    private const string REGEX_POWIAT = @"^[0-9]{2}.[0-9]{2}$";
    private const string REGEX_GMINA = @"^[0-9]{2}.[0-9]{2}.[0-9]{3}$";
    private const string REGEX_GMINA_TYPE = @"^[0-9]{1}$";
    private const string REGEX_MIEJSCOWOSC = @"[0-9]{7}";
    private const string REGEX_MIEJSCOWOSC_TYPE = @"[0-9]{2}";
    private const string REGEX_ULICA = @"[0-9]{5}";
    private const string REGEX_MIEJSCOWOSC_ULICA = @"[0-9]{7}.[0-9]{5}";


    public static readonly Regex Wojewodztwo = new(REGEX_WOJEWODZTWO);
    public static readonly Regex Powiat = new(REGEX_POWIAT);
    public static readonly Regex Gmina = new(REGEX_GMINA);
    public static readonly Regex GminaType = new(REGEX_GMINA_TYPE);
    public static readonly Regex Miejscowosc = new(REGEX_MIEJSCOWOSC);
    public static readonly Regex MiejscowoscType = new(REGEX_MIEJSCOWOSC_TYPE);
    public static readonly Regex Ulica = new(REGEX_ULICA);
    public static readonly Regex MiejscowoscUlica = new(REGEX_MIEJSCOWOSC_ULICA);
}
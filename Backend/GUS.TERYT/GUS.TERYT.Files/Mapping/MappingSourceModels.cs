// Ignore Spelling: Terc, Simc, Ulic
using GUS.TERYT.Files.Models.Source;
using System.Globalization;

namespace GUS.TERYT.Files.Mapping;

public static class MappingSourceModels
{
    private const char RAW_SEPARATOR = ';';
    private const int TERC_COLUMN_COUNT = 7;
    private const int SIMC_COLUMN_COUNT = 10;
    private const int ULIC_COLUMN_COUNT = 10;


    public static Teryt.Terc MapStringToTerc(string row)
    {
        var array = SplitAndTrim(row);
        ColumnsChecker(TERC_COLUMN_COUNT, array.Length);
        return new Teryt.Terc(
            array[0] ?? throw new ArgumentException(nameof(Teryt.Terc.WojewodztwoCode)),
            array[1],
            array[2],
            array[3],
            array[4] ?? throw new ArgumentException(nameof(Teryt.Terc.Nazwa)),
            array[5] ?? throw new ArgumentException(nameof(Teryt.Terc.NazwaDod)),
            ParseDate(array[6]));
    }

    public static Teryt.Simc MapStringToSimc(string row)
    {
        var array = SplitAndTrim(row);
        ColumnsChecker(SIMC_COLUMN_COUNT, array.Length);
        return new Teryt.Simc(
            array[0] ?? throw new ArgumentException(nameof(Teryt.Simc.WojewodstwoCode)),
            array[1] ?? throw new ArgumentException(nameof(Teryt.Simc.PowiatCode)),
            array[2] ?? throw new ArgumentException(nameof(Teryt.Simc.GminaCode)),
            array[3] ?? throw new ArgumentException(nameof(Teryt.Simc.GminaRodzCode)),
            array[4] ?? throw new ArgumentException(nameof(Teryt.Simc.MiejscowoscRodzaj)),
            array[5] ?? throw new ArgumentException(nameof(Teryt.Simc.MiejscowoscZwyczajowa)),
            array[6] ?? throw new ArgumentException(nameof(Teryt.Simc.Nazwa)),
            array[7] ?? throw new ArgumentException(nameof(Teryt.Simc.MiejscowoscId)),
            array[8] ?? throw new ArgumentException(nameof(Teryt.Simc.ParentMiejscowoscId)),
            ParseDate(array[9]));
    }

    public static Teryt.Ulic MapStringToUlic(string row)
    {
        var array = SplitAndTrim(row);
        ColumnsChecker(ULIC_COLUMN_COUNT, array.Length);
        return new Teryt.Ulic(
            array[0] ?? throw new ArgumentException(nameof(Teryt.Ulic.WojewodstwoId)),
            array[1] ?? throw new ArgumentException(nameof(Teryt.Ulic.PowiatId)),
            array[2] ?? throw new ArgumentException(nameof(Teryt.Ulic.GminaId)),
            array[3] ?? throw new ArgumentException(nameof(Teryt.Ulic.GminaTypeId)),
            array[4] ?? throw new ArgumentException(nameof(Teryt.Ulic.MiejscowoscId)),
            array[5] ?? throw new ArgumentException(nameof(Teryt.Ulic.UlicaId)),
            array[6],
            array[7] ?? throw new ArgumentException(nameof(Teryt.Ulic.Nazwa1)),
            array[8],
            ParseDate(array[9]));
    }


    private static string?[] SplitAndTrim(string value)
    {
        string?[] parts = value.Split(RAW_SEPARATOR);
        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            parts[i] = !string.IsNullOrWhiteSpace(part)
                ? part.Trim()
                : null;
        }
        return parts;
    }

    private static void ColumnsChecker(int expected, int real)
    {
        if (expected != real)
        {
            throw new ArgumentException($"Invalid columns count: expected [{expected}] != real [{real}]");
        }
    }

    private static DateOnly ParseDate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"String value can not be null or empty for parsing on {nameof(DateOnly)}");
        }
        return DateOnly.Parse(value, CultureInfo.InvariantCulture);
    }
}

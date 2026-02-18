// Ignore Spelling: Teryt, Simc
// ignore Spelling: część, miejscowości, wieś, kolonia, przysiółek, osada, Lesna, leśna, osiedle, schronisko, turystyczne,
// ignore Spelling: dzielnica, Warszawy, miasto, delegatura, miasta, Czesc, Przysiolek, Wies
using System.Diagnostics.CodeAnalysis;
using SourceSimc = GUS.TERYT.Files.Models.Source.Teryt.Simc;

namespace GUS.TERYT.Files.Models.Adapted;

public abstract partial record Teryt
{
    public sealed record Simc(
        Terc.Gmina.Id GminaId,
        Simc.Type MiejscowoscRodzaj,
        string MiejscowoscZwyczajowa,
        string Nazwa,
        Simc.Id MiejscowoscId,
        Simc.Id? ParentMiejscowoscId,
        DateOnly Date) : Teryt
    {
        public sealed record Id(string Value)
        {
            public static implicit operator string(Id value) => value.Value;
            public static implicit operator Id(string value) => new(value);
        };

        /// <summary>
        /// Based on: https://eteryt.stat.gov.pl/eTeryt/rejestr_teryt/ogolna_charakterystyka_systemow_rejestru/ogolna_charakterystyka_systemow_rejestru.aspx
        /// </summary>
        public sealed record Type
        {
            public static Type Czesc { get; } = new("00", "część miejscowości");
            public static Type Wies { get; } = new("01", "wieś");
            public static Type Kolonia { get; } = new("02", "kolonia");
            public static Type Przysiolek { get; } = new("03", "przysiółek");
            public static Type Osada { get; } = new("04", "osada");
            public static Type OsadaLesna { get; } = new("05", "osada leśna");
            public static Type Osiedle { get; } = new("06", "osiedle");
            public static Type SchroniskoTurystyczne { get; } = new("07", "schronisko turystyczne");
            public static Type Dzielnica { get; } = new("95", "dzielnica m. st. Warszawy");
            public static Type Miasto { get; } = new("96", "miasto");
            public static Type Delegatura { get; } = new("98", "delegatura");
            public static Type CzescMiasta { get; } = new("99", "część miasta");

            private static IReadOnlyDictionary<string, Type> all = new Dictionary<string, Type>
            {
                { Czesc.Id, Czesc},
                { Wies.Id, Wies},
                { Kolonia.Id, Kolonia},
                { Przysiolek.Id, Przysiolek},
                { Osada.Id, Osada},
                { OsadaLesna.Id, OsadaLesna},
                { Osiedle.Id, Osiedle},
                { SchroniskoTurystyczne.Id, SchroniskoTurystyczne},
                { Dzielnica.Id, Dzielnica},
                { Miasto.Id, Miasto},
                { Delegatura.Id, Delegatura},
                { CzescMiasta.Id, CzescMiasta},
            };
            public static IReadOnlyDictionary<string, Type> All => all;

            public string Id { get; }
            public string Name { get; }


            private Type(string id, string name)
            {
                this.Id = id;
                this.Name = name;
            }


            public static implicit operator Type(string value) => Parse(value);

            public static bool TryGetValue(
                string id,
                [NotNullWhen(true)] out Type? value
            ) => all.TryGetValue(id.Trim(), out value);

            public static Type Parse(string value)
            {
                if (!TryGetValue(value, out var type))
                {
                    throw new ArgumentException($"Not existing  key for type {typeof(Simc.Type)}: {value}");
                }
                return type;
            }
        }


        public static Simc Parse(SourceSimc item) => new(
            new Terc.Gmina.Id(item.WojewodstwoCode, item.PowiatCode, item.GminaCode, item.GminaRodzCode),
            item.MiejscowoscRodzaj,
            item.MiejscowoscZwyczajowa,
            item.Nazwa,
            item.MiejscowoscId,
            item.ParentMiejscowoscId != item.MiejscowoscId
                ? (Simc.Id)item.ParentMiejscowoscId
                : null,
            item.Date);
    }
}
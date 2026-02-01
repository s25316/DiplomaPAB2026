// Ignore Spelling: Teryt, Terc, Wojewodstwo, Powiat, Gmina
// Ignore Spelling: Teryt, Plugin, Jednostka,  obszar, 
// Ignore Spelling: województwo, Wojewodztwo, Gmina, gminie, powiat, powiatu,
// Ignore Spelling: miejska, miejsko, miasto, wiejska, miasta, wiejskiej, wiejski
// Ignore Spelling: dzielnica, delegatura, Warszawa, na, prawach
using System.Diagnostics.CodeAnalysis;
using SourceTerc = GUS.TERYT.Files.Models.Source.Teryt.Terc;

namespace GUS.TERYT.Files.Models.Adapted;

public abstract partial record Teryt
{
    public abstract record Terc : Teryt
    {
        public interface ITercId;
        public abstract record TercItem<TId>(TId Id, string Nazwa, string NazwaDod, DateOnly Date) : Terc
            where TId : ITercId;


        public sealed record Wojewodstwo(
            Wojewodstwo.Id WojewodstwoId,
            string Nazwa,
            string NazwaDod,
            DateOnly Date) : TercItem<Wojewodstwo.Id>(WojewodstwoId, Nazwa, NazwaDod, Date)
        {
            public sealed record Id(string WojewodstwoCode) : ITercId;
        }

        public sealed record Powiat(
            Powiat.Id PowiatId,
            string Nazwa,
            string NazwaDod,
            DateOnly Date) : TercItem<Powiat.Id>(PowiatId, Nazwa, NazwaDod, Date)
        {

            public sealed record Id(string WojewodstwoCode, string PowiatCode) : ITercId;
        }
;

        public sealed record Gmina(
            Gmina.Id GminaId,
            string GminaRodzId,
            string Nazwa,
            string NazwaDod,
            DateOnly Date) : TercItem<Gmina.Id>(GminaId, Nazwa, NazwaDod, Date)
        {

            public sealed record Id(string WojewodstwoCode, string PowiatCode, string GminaCode, Gmina.Type GminaRodzCode) : ITercId;

            /// <summary>
            /// Based on: https://eteryt.stat.gov.pl/eTeryt/rejestr_teryt/ogolna_charakterystyka_systemow_rejestru/ogolna_charakterystyka_systemow_rejestru.aspx
            /// </summary>
            public sealed record Type
            {
                public static Type GminaMiejska { get; } = new("1", "gmina miejska");
                public static Type GminaWiejska { get; } = new("2", "gmina wiejska");
                public static Type GminaMiejskoWiejska { get; } = new("3", "gmina miejsko-wiejska");
                public static Type MiastoWMiejskoWiejskiej { get; } = new("4", "miasto w gminie miejsko-wiejskiej");
                public static Type ObszarWiejskiWMiejskoWiejskiej { get; } = new("5", "obszar wiejski w gminie miejsko-wiejskiej");
                public static Type Dzielnica { get; } = new("8", "dzielnica w m.st. Warszawa");
                public static Type Delegatura { get; } = new("9", "delegatura miasta");

                private static IReadOnlyDictionary<string, Type> all = new Dictionary<string, Type>()
                {
                    { GminaMiejska.Id, GminaMiejska},
                    { GminaWiejska.Id, GminaWiejska},
                    { GminaMiejskoWiejska.Id, GminaMiejskoWiejska},
                    { MiastoWMiejskoWiejskiej.Id, MiastoWMiejskoWiejskiej},
                    { ObszarWiejskiWMiejskoWiejskiej.Id, ObszarWiejskiWMiejskoWiejskiej},
                    { Dzielnica.Id, Dzielnica},
                    { Delegatura.Id, Delegatura},
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
                        throw new ArgumentException($"Not existing key for type {typeof(Gmina.Type)}: {value}");
                    }
                    return type;
                }
            }
        }


        public static Terc Parse(SourceTerc item)
        {
            if (!string.IsNullOrWhiteSpace(item.PowiatCode) &&
                !string.IsNullOrWhiteSpace(item.GminaCode) &&
                !string.IsNullOrWhiteSpace(item.GminaRodzCode))
            {
                return new Gmina(
                    new Gmina.Id(item.WojewodstwoCode, item.PowiatCode, item.GminaCode, item.GminaRodzCode),
                    item.GminaRodzCode,
                    item.Nazwa,
                    item.NazwaDod,
                    item.Date);
            }

            if (!string.IsNullOrWhiteSpace(item.PowiatCode))
            {
                return new Powiat(
                    new Powiat.Id(item.WojewodstwoCode, item.PowiatCode),
                    item.Nazwa,
                    item.NazwaDod,
                    item.Date);
            }

            return new Wojewodstwo(
                new Wojewodstwo.Id(item.WojewodstwoCode),
                    item.Nazwa,
                    item.NazwaDod,
                    item.Date);
        }
    }
}


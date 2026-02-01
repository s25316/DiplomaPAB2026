// Ignore Spelling: Teryt, Ulic
using SourceUlic = GUS.TERYT.Files.Models.Source.Teryt.Ulic;

namespace GUS.TERYT.Files.Models.Adapted;

public abstract partial record Teryt
{
    public sealed record UlicInfo(Ulic Ulica, SimcUlicIds Ids) : Teryt
    {
        public static UlicInfo Parse(SourceUlic item) => new(Ulic.Parse(item), SimcUlicIds.Parse(item));
    }

    public sealed record Ulic(Ulic.Id UlicId, Ulic.Type? UlicType, string Nazwa1, string? Nazwa2, DateOnly Date) : Teryt
    {
        public sealed record Id(string Value)
        {
            public static implicit operator string(Id value) => value.Value;
            public static implicit operator Id(string value) => new(value);
        }

        public sealed record Type(string Value)
        {
            public static implicit operator string(Type value) => value.Value;
            public static implicit operator Type?(string? value) => !string.IsNullOrWhiteSpace(value)
                ? new(value)
                : null;
        }


        public static Ulic Parse(SourceUlic item) => new(
            item.UlicaId,
            item.Ceha,
            item.Nazwa1,
            item.Nazwa2,
            item.Date);
    }
}

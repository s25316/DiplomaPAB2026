// Ignore Spelling: Regon, Plugin, Nazwa, Wojewodztwo, Powiat, Gmina, Miejscowosc
// Ignore Spelling: Ulica, Typ, Kod, Pocztowy Szukaj, dane
using GUS.REGON.Models.Responses.Enums;
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses;

public abstract partial record Response
{
    [XmlRoot(ElementName = "dane", Namespace = "http://CIS/BIR/PUBL/2014/07", IsNullable = false)]
    public sealed record DaneSzukaj : Response
    {
        [XmlElement("Regon")]
        public string Regon { get; init; } = null!;

        [XmlElement("Nazwa")]
        public string Nazwa { get; init; } = null!;

        [XmlElement("Wojewodztwo", IsNullable = true)]
        public string? Wojewodztwo { get; init; } = null;

        [XmlElement("Powiat", IsNullable = true)]
        public string? Powiat { get; init; } = null;

        [XmlElement("Gmina", IsNullable = true)]
        public string? Gmina { get; init; } = null;

        [XmlElement("Miejscowosc", IsNullable = true)]
        public string? Miejscowosc { get; init; } = null;

        [XmlElement("KodPocztowy", IsNullable = true)]
        public string? KodPocztowy { get; init; } = null;

        [XmlElement("Ulica", IsNullable = true)]
        public string? Ulica { get; init; } = null;

        [XmlElement("Typ")]
        public TypJednostki Typ { get; init; }

        [XmlElement("SilosID", IsNullable = true)]
        public int? SilosID { get; init; }
    }
}
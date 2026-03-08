// Ignore Spelling: Regon, Plugin, Raporty, Jednostki, dane
// Ignore Spelling: Raport, Nazwa, Skrocona
// Ignore Spelling: Kraj, Wojewodztwo, Powiat, Gmina, Kod, Pocztowy, Miejscowosc, Poczty
// Ignore Spelling: Ulica, Numer, Nieruchomosci, Lokalu, Nietypowe, Miejsce, Lokalizacji
// Ignore Spelling: Wewn, Wewnetrzny, Telefonu, Adres, Faksu
// Ignore Spelling: Powstania, Rozpoczecia, Wpisu, Zawieszenia, Wznowienia, Zmiany, Zakonczenia, Skreslenia
// Ignore Spelling: Rejestru, Ewidencji, Numerw, Rejestrze, Rejestrowy
// Ignore Spelling: Rodzaj, Forma, Finansowania, Podstawowa, Prawna, Szczegolna, Zalozycielski
// Ignore Spelling: Wlasnosci, Dzialalnosci
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Names;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Symbols;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Contacts;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Dates;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaFinansowania;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaPrawna.Podstawowa;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaPrawna.Szczegolna;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaWlasnosci;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.OrganRejestrowy;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.OrganZalozycielski;
using GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.RodzajRejestru;
using GUS.REGON.Models.Responses.Enums.RaportJednostki;
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses;

public abstract partial record Response
{
    [XmlRoot(ElementName = "dane", Namespace = "http://CIS/BIR/PUBL/2014/07", IsNullable = false)]
    public sealed record RaportJednostki : Response
    {
        [XmlChoiceIdentifier("RegonParameter")]
        [XmlElement(ElementName = "fiz_regon9")]
        [XmlElement(ElementName = "lokpraw_regon14")]
        [XmlElement(ElementName = "praw_regon14")]
        [XmlElement(ElementName = "lokfiz_regon14")]
        public string Regon { get; init; } = null!;
        public RegonParameter RegonParameter { get; init; }


        [XmlChoiceIdentifier("NazwaParameter")]
        [XmlElement(ElementName = "fiz_nazwa")]
        [XmlElement(ElementName = "lokpraw_nazwa")]
        [XmlElement(ElementName = "praw_nazwa")]
        [XmlElement(ElementName = "lokfiz_nazwa")]
        public string Nazwa { get; init; } = null!;
        public NazwaParameter NazwaParameter { get; init; }


        [XmlChoiceIdentifier("NipParameter")]
        [XmlElement(ElementName = "lokpraw_nip", IsNullable = true)]
        [XmlElement(ElementName = "praw_nip", IsNullable = true)]
        public string? Nip { get; init; } = null;
        public NipParameter NipParameter { get; init; }


        [XmlChoiceIdentifier("NazwaSkroconaParameter")]
        [XmlElement(ElementName = "fiz_nazwaSkrocona", IsNullable = true)]
        [XmlElement(ElementName = "praw_nazwaSkrocona", IsNullable = true)]
        public string? NazwaSkrocona { get; init; } = null;
        public NazwaSkroconaParameter NazwaSkroconaParameter { get; init; }


        [XmlChoiceIdentifier("KrajSymbolParameter")]
        [XmlElement(ElementName = "fiz_adSiedzKraj_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzKraj_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzKraj_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzKraj_Symbol", IsNullable = true)]
        public string? KrajSymbol { get; init; } = null;
        public KrajSymbolParameter KrajSymbolParameter { get; init; }


        [XmlChoiceIdentifier("WojewodztwoSymbolParameter")]
        [XmlElement(ElementName = "fiz_adSiedzWojewodztwo_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzWojewodztwo_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzWojewodztwo_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzWojewodztwo_Symbol", IsNullable = true)]
        public string? WojewodztwoSymbol { get; init; } = null;
        public WojewodztwoSymbolParameter WojewodztwoSymbolParameter { get; init; }


        [XmlChoiceIdentifier("PowiatSymbolParameter")]
        [XmlElement(ElementName = "fiz_adSiedzPowiat_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzPowiat_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzPowiat_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzPowiat_Symbol", IsNullable = true)]
        public string? PowiatSymbol { get; init; } = null;
        public PowiatSymbolParameter PowiatSymbolParameter { get; init; }


        [XmlChoiceIdentifier("GminaSymbolParameter")]
        [XmlElement(ElementName = "fiz_adSiedzGmina_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzGmina_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzGmina_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzGmina_Symbol", IsNullable = true)]
        public string? GminaSymbol { get; init; } = null;
        public GminaSymbolParameter GminaSymbolParameter { get; init; }


        [XmlChoiceIdentifier("KodPocztowyParameter")]
        [XmlElement(ElementName = "fiz_adSiedzKodPocztowy", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzKodPocztowy", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzKodPocztowy", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzKodPocztowy", IsNullable = true)]
        public string? KodPocztowy { get; init; } = null;
        public KodPocztowyParameter KodPocztowyParameter { get; init; }


        [XmlChoiceIdentifier("MiejscowoscPocztySymbolParameter")]
        [XmlElement(ElementName = "fiz_adSiedzMiejscowoscPoczty_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzMiejscowoscPoczty_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzMiejscowoscPoczty_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzMiejscowoscPoczty_Symbol", IsNullable = true)]
        public string? MiejscowoscPocztySymbol { get; init; } = null;
        public MiejscowoscPocztySymbolParameter MiejscowoscPocztySymbolParameter { get; init; }


        [XmlChoiceIdentifier("MiejscowoscSymbolParameter")]
        [XmlElement(ElementName = "fiz_adSiedzMiejscowosc_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzMiejscowosc_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzMiejscowosc_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzMiejscowosc_Symbol", IsNullable = true)]
        public string? MiejscowoscSymbol { get; init; } = null;
        public MiejscowoscSymbolParameter MiejscowoscSymbolParameter { get; init; }


        [XmlChoiceIdentifier("UlicaSymbolParameter")]
        [XmlElement(ElementName = "fiz_adSiedzUlica_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzUlica_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzUlica_Symbol", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzUlica_Symbol", IsNullable = true)]
        public string? UlicaSymbol { get; init; } = null;
        public UlicaSymbolParameter UlicaSymbolParameter { get; init; }


        [XmlChoiceIdentifier("NumerNieruchomosciParameter")]
        [XmlElement(ElementName = "fiz_adSiedzNumerNieruchomosci", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzNumerNieruchomosci", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzNumerNieruchomosci", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzNumerNieruchomosci", IsNullable = true)]
        public string? NumerNieruchomosci { get; init; } = null;
        public NumerNieruchomosciParameter NumerNieruchomosciParameter { get; init; }


        [XmlChoiceIdentifier("NumerLokaluParameter")]
        [XmlElement(ElementName = "fiz_adSiedzNumerLokalu", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzNumerLokalu", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzNumerLokalu", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzNumerLokalu", IsNullable = true)]
        public string? NumerLokalu { get; init; } = null;
        public NumerLokaluParameter NumerLokaluParameter { get; init; }


        [XmlChoiceIdentifier("NietypoweMiejsceLokalizacjiParameter")]
        [XmlElement(ElementName = "fiz_adSiedzNietypoweMiejsceLokalizacji", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzNietypoweMiejsceLokalizacji", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzNietypoweMiejsceLokalizacji", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzNietypoweMiejsceLokalizacji", IsNullable = true)]
        public string? NietypoweMiejsceLokalizacji { get; init; } = null;
        public NietypoweMiejsceLokalizacjiParameter NietypoweMiejsceLokalizacjiParameter { get; init; }


        [XmlChoiceIdentifier("KrajNazwaParameter")]
        [XmlElement(ElementName = "fiz_adSiedzKraj_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzKraj_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzKraj_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzKraj_Nazwa", IsNullable = true)]
        public string? KrajNazwa { get; init; } = null;
        public KrajNazwaParameter KrajNazwaParameter { get; init; }


        [XmlChoiceIdentifier("WojewodztwoNazwaParameter")]
        [XmlElement(ElementName = "fiz_adSiedzWojewodztwo_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzWojewodztwo_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzWojewodztwo_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzWojewodztwo_Nazwa", IsNullable = true)]
        public string? WojewodztwoNazwa { get; init; } = null;
        public WojewodztwoNazwaParameter WojewodztwoNazwaParameter { get; init; }


        [XmlChoiceIdentifier("PowiatNazwaParameter")]
        [XmlElement(ElementName = "fiz_adSiedzPowiat_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzPowiat_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzPowiat_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzPowiat_Nazwa", IsNullable = true)]
        public string? PowiatNazwa { get; init; } = null;
        public PowiatNazwaParameter PowiatNazwaParameter { get; init; }


        [XmlChoiceIdentifier("GminaNazwaParameter")]
        [XmlElement(ElementName = "fiz_adSiedzGmina_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzGmina_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzGmina_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzGmina_Nazwa", IsNullable = true)]
        public string? GminaNazwa { get; init; } = null;
        public GminaNazwaParameter GminaNazwaParameter { get; init; }


        [XmlChoiceIdentifier("MiejscowoscPocztyNazwaParameter")]
        [XmlElement(ElementName = "fiz_adSiedzMiejscowoscPoczty_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzMiejscowoscPoczty_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzMiejscowoscPoczty_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzMiejscowoscPoczty_Nazwa", IsNullable = true)]
        public string? MiejscowoscPocztyNazwa { get; init; } = null;
        public MiejscowoscPocztyNazwaParameter MiejscowoscPocztyNazwaParameter { get; init; }


        [XmlChoiceIdentifier("MiejscowoscNazwaParameter")]
        [XmlElement(ElementName = "fiz_adSiedzMiejscowosc_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzMiejscowosc_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzMiejscowosc_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzMiejscowosc_Nazwa", IsNullable = true)]
        public string? MiejscowoscNazwa { get; init; } = null;
        public MiejscowoscNazwaParameter MiejscowoscNazwaParameter { get; init; }


        [XmlChoiceIdentifier("UlicaNazwaParameter")]
        [XmlElement(ElementName = "fiz_adSiedzUlica_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_adSiedzUlica_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "praw_adSiedzUlica_Nazwa", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_adSiedzUlica_Nazwa", IsNullable = true)]
        public string? UlicaNazwa { get; init; } = null;
        public UlicaNazwaParameter UlicaNazwaParameter { get; init; }


        [XmlChoiceIdentifier("NumerTelefonuParameter")]
        [XmlElement(ElementName = "fiz_numerTelefonu", IsNullable = true)]
        [XmlElement(ElementName = "fizC_numerTelefonu", IsNullable = true)]
        [XmlElement(ElementName = "praw_numerTelefonu", IsNullable = true)]
        public string[] NumerTelefonu { get; init; } = Array.Empty<string>();
        public NumerTelefonuParameter[] NumerTelefonuParameter { get; init; } = Array.Empty<NumerTelefonuParameter>();


        [XmlChoiceIdentifier("NumerWewnTelefonuParameter")]
        [XmlElement(ElementName = "fiz_numerWewnetrznyTelefonu", IsNullable = true)]
        [XmlElement(ElementName = "fizC_numerWewnetrznyTelefonu", IsNullable = true)]
        [XmlElement(ElementName = "praw_numerWewnetrznyTelefonu", IsNullable = true)]
        public string[] NumerWewnetrznyTelefonu { get; init; } = Array.Empty<string>();
        public NumerWewnTelefonuParameter[] NumerWewnTelefonuParameter { get; init; } = Array.Empty<NumerWewnTelefonuParameter>();


        [XmlChoiceIdentifier("NumerFaksuParameter")]
        [XmlElement(ElementName = "fiz_numerFaksu", IsNullable = true)]
        [XmlElement(ElementName = "fizC_numerFaksu", IsNullable = true)]
        [XmlElement(ElementName = "praw_numerFaksu", IsNullable = true)]
        public string[] NumerFaksu { get; init; } = Array.Empty<string>();
        public NumerFaksuParameter[] NumerFaksuParameter { get; init; } = Array.Empty<NumerFaksuParameter>();


        [XmlChoiceIdentifier("AdresEmailParameter")]
        [XmlElement(ElementName = "fiz_adresEmail", IsNullable = true)]
        [XmlElement(ElementName = "fiz_adresEmail2", IsNullable = true)]
        [XmlElement(ElementName = "fizC_adresEmail", IsNullable = true)]
        [XmlElement(ElementName = "praw_adresEmail", IsNullable = true)]
        [XmlElement(ElementName = "praw_adresEmail2", IsNullable = true)]
        public string[] AdresEmail { get; init; } = Array.Empty<string>();
        public AdresEmailParameter[] AdresEmailParameter { get; init; } = Array.Empty<AdresEmailParameter>();


        [XmlChoiceIdentifier("WWWParameter")]
        [XmlElement(ElementName = "fiz_adresStronyinternetowej", IsNullable = true)]
        [XmlElement(ElementName = "fizC_adresStronyInternetowej", IsNullable = true)]
        [XmlElement(ElementName = "praw_adresStronyinternetowej", IsNullable = true)]
        public string[] WWW { get; init; } = Array.Empty<string>();
        public WWWParameter[] WWWParameter { get; init; } = Array.Empty<WWWParameter>();


        [XmlChoiceIdentifier("DataPowstaniaParameter")]
        [XmlElement(ElementName = "fiz_dataPowstania")]
        [XmlElement(ElementName = "lokpraw_dataPowstania")]
        [XmlElement(ElementName = "praw_dataPowstania")]
        [XmlElement(ElementName = "lokfiz_dataPowstania")]
        public string DataPowstania { get; init; } = null!;
        public DataPowstaniaParameter DataPowstaniaParameter { get; init; }


        [XmlChoiceIdentifier("DataRozpoczeciaParameter")]
        [XmlElement(ElementName = "fiz_dataRozpoczeciaDzialalnosci")]
        [XmlElement(ElementName = "lokpraw_dataRozpoczeciaDzialalnosci")]
        [XmlElement(ElementName = "praw_dataRozpoczeciaDzialalnosci")]
        [XmlElement(ElementName = "lokfiz_dataRozpoczeciaDzialalnosci")]
        public string DataRozpoczecia { get; init; } = null!;
        public DataRozpoczeciaParameter DataRozpoczeciaParameter { get; init; }


        [XmlChoiceIdentifier("DataWpisuParameter")]
        [XmlElement(ElementName = "fiz_dataWpisuDoREGONDzialalnosci")]
        [XmlElement(ElementName = "lokpraw_dataWpisuDoREGON")]
        [XmlElement(ElementName = "praw_dataWpisuDoREGON")]
        [XmlElement(ElementName = "lokfiz_dataWpisuDoREGON")]
        public string DataWpisu { get; init; } = null!;
        public DataWpisuParameter DataWpisuParameter { get; init; }


        [XmlChoiceIdentifier("DataZawieszeniaParameter")]
        [XmlElement(ElementName = "fiz_dataZawieszeniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_dataZawieszeniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "praw_dataZawieszeniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_dataZawieszeniaDzialalnosci", IsNullable = true)]
        public string? DataZawieszenia { get; init; } = null;
        public DataZawieszeniaParameter DataZawieszeniaParameter { get; init; }


        [XmlChoiceIdentifier("DataWznowieniaParameter")]
        [XmlElement(ElementName = "fiz_dataWznowieniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_dataWznowieniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "praw_dataWznowieniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_dataWznowieniaDzialalnosci", IsNullable = true)]
        public string? DataWznowienia { get; init; } = null;
        public DataWznowieniaParameter DataWznowieniaParameter { get; init; }


        [XmlChoiceIdentifier("DataZmianyParameter")]
        [XmlElement(ElementName = "fiz_dataZaistnieniaZmianyDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_dataZaistnieniaZmiany", IsNullable = true)]
        [XmlElement(ElementName = "praw_dataZaistnieniaZmiany", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_dataZaistnieniaZmiany", IsNullable = true)]
        public string? DataZmiany { get; init; } = null;
        public DataZmianyParameter DataZmianyParameter { get; init; }


        [XmlChoiceIdentifier("DataZakonczeniaParameter")]
        [XmlElement(ElementName = "fiz_dataZakonczeniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_dataZakonczeniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "praw_dataZakonczeniaDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_dataZakonczeniaDzialalnosci", IsNullable = true)]
        public string? DataZakonczenia { get; init; } = null;
        public DataZakonczeniaParameter DataZakonczeniaParameter { get; init; }


        [XmlChoiceIdentifier("DataSkresleniaParameter")]
        [XmlElement(ElementName = "fiz_dataSkresleniazRegonDzialalnosci", IsNullable = true)]
        [XmlElement(ElementName = "lokpraw_dataSkresleniazRegon", IsNullable = true)]
        [XmlElement(ElementName = "praw_dataSkresleniazRegon", IsNullable = true)]
        [XmlElement(ElementName = "lokfiz_dataSkresleniazRegon", IsNullable = true)]
        public string? DataSkreslenia { get; init; } = null;
        public DataSkresleniaParameter DataSkresleniaParameter { get; init; }


        [XmlChoiceIdentifier("DataWpisuDoRejestruEwidencjiParameter")]
        [XmlElement(ElementName = "fizC_dataWpisuDoRejestruEwidencji")]
        [XmlElement(ElementName = "fizP_dataWpisuDoRejestruEwidencji")]
        [XmlElement(ElementName = "lokpraw_dataWpisuDoRejestruEwidencji")]
        [XmlElement(ElementName = "lokfiz_dataWpisuDoRejestruEwidencji")]
        public string? DataWpisuDoRejestruEwidencji { get; init; } = null;
        public DataWpisuDoRejestruEwidencjiParameter DataWpisuDoRejestruEwidencjiParameter { get; init; }


        [XmlChoiceIdentifier("NumerwRejestrzeEwidencjiParameter")]
        [XmlElement(ElementName = "fizC_numerwRejestrzeEwidencji")]
        [XmlElement(ElementName = "fizP_numerwRejestrzeEwidencji")]
        [XmlElement(ElementName = "lokpraw_numerWrejestrzeEwidencji")]
        [XmlElement(ElementName = "praw_numerWrejestrzeEwidencji")]
        [XmlElement(ElementName = "lokfiz_numerwRejestrzeEwidencji")]
        public string? NumerwRejestrzeEwidencji { get; init; } = null;
        public NumerwRejestrzeEwidencjiParameter NumerwRejestrzeEwidencjiParameter { get; init; }


        [XmlChoiceIdentifier("OrganRejestrowySymbolParameter")]
        [XmlElement(ElementName = "fizC_OrganRejestrowy_Symbol")]
        [XmlElement(ElementName = "fizP_OrganRejestrowy_Symbol")]
        [XmlElement(ElementName = "lokpraw_organRejestrowy_Symbol")]
        [XmlElement(ElementName = "praw_organRejestrowy_Symbol")]
        [XmlElement(ElementName = "lokfiz_organRejestrowy_Symbol")]
        public string? OrganRejestrowySymbol { get; init; } = null;
        public OrganRejestrowySymbolParameter OrganRejestrowySymbolParameter { get; init; }


        [XmlChoiceIdentifier("OrganRejestrowyNazwaParameter")]
        [XmlElement(ElementName = "fizC_OrganRejestrowy_Nazwa")]
        [XmlElement(ElementName = "fizP_OrganRejestrowy_Nazwa")]
        [XmlElement(ElementName = "lokpraw_organRejestrowy_Nazwa")]
        [XmlElement(ElementName = "praw_organRejestrowy_Nazwa")]
        [XmlElement(ElementName = "lokfiz_organRejestrowy_Nazwa")]
        public string? OrganRejestrowyNazwa { get; init; } = null;
        public OrganRejestrowyNazwaParameter OrganRejestrowyNazwaParameter { get; init; }


        [XmlChoiceIdentifier("RodzajRejestruSymbolParameter")]
        [XmlElement(ElementName = "fizC_RodzajRejestru_Symbol")]
        [XmlElement(ElementName = "fizP_RodzajRejestru_Symbol")]
        [XmlElement(ElementName = "lokpraw_rodzajRejestruEwidencji_Symbol")]
        [XmlElement(ElementName = "praw_rodzajRejestruEwidencji_Symbol")]
        [XmlElement(ElementName = "lokfiz_rodzajRejestru_Symbol")]
        public string? RodzajRejestruSymbol { get; init; } = null;
        public RodzajRejestruSymbolParameter RodzajRejestruSymbolParameter { get; init; }


        [XmlChoiceIdentifier("RodzajRejestruNazwaParameter")]
        [XmlElement(ElementName = "fizC_RodzajRejestru_Nazwa")]
        [XmlElement(ElementName = "fizP_RodzajRejestru_Nazwa")]
        [XmlElement(ElementName = "lokpraw_rodzajRejestruEwidencji_Nazwa")]
        [XmlElement(ElementName = "praw_rodzajRejestruEwidencji_Nazwa")]
        [XmlElement(ElementName = "lokfiz_rodzajRejestru_Nazwa")]
        public string RodzajRejestruNazwa { get; init; } = null!;
        public RodzajRejestruNazwaParameter RodzajRejestruNazwaParameter { get; init; }

        [XmlChoiceIdentifier("FormaFinansowaniaSymbolParameter")]
        [XmlElement(ElementName = "praw_formaFinansowania_Symbol")]
        [XmlElement(ElementName = "lokfiz_formaFinansowania_Symbol")]
        [XmlElement(ElementName = "lokpraw_formaFinansowania_Symbol")]
        public string? FormaFinansowaniaSymbol { get; init; } = null;
        public FormaFinansowaniaSymbolParameter FormaFinansowaniaSymbolParameter { get; init; }


        [XmlChoiceIdentifier("FormaFinansowaniaNazwaParameter")]
        [XmlElement(ElementName = "praw_formaFinansowania_Nazwa")]
        [XmlElement(ElementName = "lokfiz_formaFinansowania_Nazwa")]
        [XmlElement(ElementName = "lokpraw_formaFinansowania_Nazwa")]
        public string? FormaFinansowaniaNazwa { get; init; } = null;
        public FormaFinansowaniaNazwaParameter FormaFinansowaniaNazwaParameter { get; init; }


        [XmlChoiceIdentifier("PodstawowaFormaPrawnaSymbolParameter")]
        [XmlElement(ElementName = "praw_podstawowaFormaPrawna_Symbol")]
        [XmlElement(ElementName = "lokpraw_podstawowaFormaPrawna_Symbol")]
        public string? PodstawowaFormaPrawnaSymbol { get; init; } = null;
        public PodstawowaFormaPrawnaSymbolParameter PodstawowaFormaPrawnaSymbolParameter { get; init; }


        [XmlChoiceIdentifier("PodstawowaFormaPrawnaNazwaParameter")]
        [XmlElement(ElementName = "praw_podstawowaFormaPrawna_Nazwa")]
        [XmlElement(ElementName = "lokpraw_podstawowaFormaPrawna_Nazwa")]
        public string? PodstawowaFormaPrawnaNazwa { get; init; } = null;
        public PodstawowaFormaPrawnaNazwaParameter PodstawowaFormaPrawnaNazwaParameter { get; init; }


        [XmlChoiceIdentifier("SzczegolnaFormaPrawnaSymbolParameter")]
        [XmlElement(ElementName = "praw_szczegolnaFormaPrawna_Symbol")]
        [XmlElement(ElementName = "lokpraw_szczegolnaFormaPrawna_Symbol")]
        public string? SzczegolnaFormaPrawnaSymbol { get; init; } = null;
        public SzczegolnaFormaPrawnaSymbolParameter SzczegolnaFormaPrawnaSymbolParameter { get; init; }


        [XmlChoiceIdentifier("SzczegolnaFormaPrawnaNazwaParameter")]
        [XmlElement(ElementName = "praw_szczegolnaFormaPrawna_Nazwa")]
        [XmlElement(ElementName = "lokpraw_szczegolnaFormaPrawna_Nazwa")]
        public string? SzczegolnaFormaPrawnaNazwa { get; init; } = null;
        public SzczegolnaFormaPrawnaNazwaParameter SzczegolnaFormaPrawnaNazwaParameter { get; init; }


        [XmlChoiceIdentifier("OrganZalozycielskiSymbolParameter")]
        [XmlElement(ElementName = "praw_organZalozycielski_Symbol")]
        [XmlElement(ElementName = "lokpraw_organZalozycielski_Symbol")]
        public string? OrganZalozycielskiSymbol { get; init; } = null;
        public OrganZalozycielskiSymbolParameter OrganZalozycielskiSymbolParameter { get; init; }


        [XmlChoiceIdentifier("OrganZalozycielskiNazwaParameter")]
        [XmlElement(ElementName = "praw_organZalozycielski_Nazwa")]
        [XmlElement(ElementName = "lokpraw_organZalozycielski_Nazwa")]
        public string? OrganZalozycielskiNazwa { get; init; } = null;
        public OrganZalozycielskiNazwaParameter OrganZalozycielskiNazwaParameter { get; init; }


        [XmlChoiceIdentifier("FormaWlasnosciSymbolParameter")]
        [XmlElement(ElementName = "praw_formaWlasnosci_Symbol")]
        [XmlElement(ElementName = "lokpraw_formaWlasnosci_Symbol")]
        public string? FormaWlasnosciSymbol { get; init; } = null;
        public FormaWlasnosciSymbolParameter FormaWlasnosciSymbolParameter { get; init; }


        [XmlChoiceIdentifier("FormaWlasnosciNazwaParameter")]
        [XmlElement(ElementName = "praw_formaWlasnosci_Nazwa")]
        [XmlElement(ElementName = "lokpraw_formaWlasnosci_Nazwa")]
        public string? FormaWlasnosciNazwa { get; init; } = null;
        public FormaWlasnosciNazwaParameter FormaWlasnosciNazwaParameter { get; init; }


        [XmlChoiceIdentifier("DzialalnosciParameter")]
        [XmlElement(ElementName = "praw_jednostekLokalnych")]
        [XmlElement(ElementName = "lokfiz_dzialalnosci")]
        [XmlElement(ElementName = "lokpraw_dzialalnosci")]
        public string? Dzialalnosci { get; init; } = null;
        public DzialalnosciParameter DzialalnosciParameter { get; init; }


        [XmlElement(ElementName = "lokfiz_silos_Symbol", IsNullable = true)]
        public string? SilosSymbol { get; init; } = null;


        [XmlElement(ElementName = "lokfiz_silos_Nazwa", IsNullable = true)]
        public string? SilosNazwa { get; init; } = null;
    }
}
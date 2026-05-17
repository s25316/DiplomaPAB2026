using System.ComponentModel.DataAnnotations;
using Response = GUS.REGON.Models.Descriptions.Response;

namespace GUS.REGON.Models.Responses;

public sealed partial class Report
{
    /// <include file='Response.xml' path='docs/members/member[@name="Institution"]/summary' />
    [Display(Name = nameof(Response.Institution), ResourceType = typeof(Response))]
    public sealed class Institution
    {
        /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address"]/summary' />
        [Display(Name = nameof(Response.Institution_Address), ResourceType = typeof(Response))]
        public sealed record Address
        {
            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_Kraj"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_Kraj), ResourceType = typeof(Response))]
            public required DictionaryItem Kraj { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_Wojewodztwo"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_Wojewodztwo), ResourceType = typeof(Response))]
            public required DictionaryItem Wojewodztwo { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_Powiat"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_Powiat), ResourceType = typeof(Response))]
            public required DictionaryItem Powiat { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_Gmina"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_Gmina), ResourceType = typeof(Response))]
            public required DictionaryItem Gmina { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_MiejscowoscPoczty"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_MiejscowoscPoczty), ResourceType = typeof(Response))]
            public required DictionaryItem MiejscowoscPoczty { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_Miejscowosc"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_Miejscowosc), ResourceType = typeof(Response))]
            public required DictionaryItem Miejscowosc { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_Ulica"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_Ulica), ResourceType = typeof(Response))]
            public required DictionaryItem? Ulica { get; init; } = null;

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_KodPocztowy"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_KodPocztowy), ResourceType = typeof(Response))]
            public required string KodPocztowy { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_NumerNieruchomosci"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_NumerNieruchomosci), ResourceType = typeof(Response))]
            public required string NumerNieruchomosci { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_NumerLokalu"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_NumerLokalu), ResourceType = typeof(Response))]
            public required string? NumerLokalu { get; init; } = null;

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Address_NietypoweMiejsceLokalizacji"]/summary' />
            [Display(Name = nameof(Response.Institution_Address_NietypoweMiejsceLokalizacji), ResourceType = typeof(Response))]
            public required string? NietypoweMiejsceLokalizacji { get; init; } = null;
        }


        /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates"]/summary' />
        [Display(Name = nameof(Response.Institution_Dates), ResourceType = typeof(Response))]
        public sealed record Dates
        {
            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataPowstania"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataPowstania), ResourceType = typeof(Response))]
            public required DateOnly DataPowstania { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataRozpoczecia"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataRozpoczecia), ResourceType = typeof(Response))]
            public required DateOnly DataRozpoczecia { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataWpisu"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataWpisu), ResourceType = typeof(Response))]
            public required DateOnly? DataWpisu { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataZawieszenia"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataZawieszenia), ResourceType = typeof(Response))]
            public required DateOnly? DataZawieszenia { get; init; } = null;

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataWznowienia"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataWznowienia), ResourceType = typeof(Response))]
            public required DateOnly? DataWznowienia { get; init; } = null;

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataZmiany"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataZmiany), ResourceType = typeof(Response))]
            public required DateOnly? DataZmiany { get; init; } = null;

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataZakonczenia"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataZakonczenia), ResourceType = typeof(Response))]
            public required DateOnly? DataZakonczenia { get; init; } = null;

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataSkreslenia"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataSkreslenia), ResourceType = typeof(Response))]
            public required DateOnly? DataSkreslenia { get; init; } = null;

            /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dates_DataWpisuDoRejestruEwidencji"]/summary' />
            [Display(Name = nameof(Response.Institution_Dates_DataWpisuDoRejestruEwidencji), ResourceType = typeof(Response))]
            public required DateOnly? DataWpisuDoRejestruEwidencji { get; init; } = null;
        }


        /// <include file='Response.xml' path='docs/members/member[@name="Institution_Regon"]/summary' />
        [Display(Name = nameof(Response.Institution_Regon), ResourceType = typeof(Response))]
        public required string Regon { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_Nazwa"]/summary' />
        [Display(Name = nameof(Response.Institution_Nazwa), ResourceType = typeof(Response))]
        public required string Nazwa { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_NazwaSkrocona"]/summary' />
        [Display(Name = nameof(Response.Institution_NazwaSkrocona), ResourceType = typeof(Response))]
        public required string? NazwaSkrocona { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_NumerwRejestrzeEwidencji"]/summary' />
        [Display(Name = nameof(Response.Institution_NumerwRejestrzeEwidencji), ResourceType = typeof(Response))]
        public required string? NumerwRejestrzeEwidencji { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_Dzialalnosci"]/summary' />
        [Display(Name = nameof(Response.Institution_Dzialalnosci), ResourceType = typeof(Response))]
        public required string? Dzialalnosci { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_Adres"]/summary' />
        [Display(Name = nameof(Response.Institution_Adres), ResourceType = typeof(Response))]
        public required Address? Adres { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_Daty"]/summary' />
        [Display(Name = nameof(Response.Institution_Daty), ResourceType = typeof(Response))]
        public required Dates Daty { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_OrganRejestrowy"]/summary' />
        [Display(Name = nameof(Response.Institution_OrganRejestrowy), ResourceType = typeof(Response))]
        public required DictionaryItem? OrganRejestrowy { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_RodzajRejestru"]/summary' />
        [Display(Name = nameof(Response.Institution_RodzajRejestru), ResourceType = typeof(Response))]
        public required DictionaryItem? RodzajRejestru { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_FormaFinansowania"]/summary' />
        [Display(Name = nameof(Response.Institution_FormaFinansowania), ResourceType = typeof(Response))]
        public required DictionaryItem? FormaFinansowania { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_PodstawowaFormaPrawna"]/summary' />
        [Display(Name = nameof(Response.Institution_PodstawowaFormaPrawna), ResourceType = typeof(Response))]
        public required DictionaryItem? PodstawowaFormaPrawna { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_SzczegolnaFormaPrawna"]/summary' />
        [Display(Name = nameof(Response.Institution_SzczegolnaFormaPrawna), ResourceType = typeof(Response))]
        public required DictionaryItem? SzczegolnaFormaPrawna { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_OrganZalozycielski"]/summary' />
        [Display(Name = nameof(Response.Institution_OrganZalozycielski), ResourceType = typeof(Response))]
        public required DictionaryItem? OrganZalozycielski { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_FormaWlasnosci"]/summary' />
        [Display(Name = nameof(Response.Institution_FormaWlasnosci), ResourceType = typeof(Response))]
        public required DictionaryItem? FormaWlasnosci { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_TypJednostki"]/summary' />
        [Display(Name = nameof(Response.Institution_TypJednostki), ResourceType = typeof(Response))]
        public required DictionaryItem TypJednostki { get; init; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RADON.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="institution_address_data"]/summary' />
[Display(Name = nameof(Response.institution_address_data), ResourceType = typeof(Response))]
public class AddressData
{
    /// <include file='Response.xml' path='docs/members/member[@name="institution_address_country"]/summary' />
    [Display(Name = nameof(Response.institution_address_country), ResourceType = typeof(Response))]
    [JsonPropertyName("country")]
    public string? Country { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_address_voivodeship"]/summary' />
    [Display(Name = nameof(Response.institution_address_voivodeship), ResourceType = typeof(Response))]
    [JsonPropertyName("voivodeship")]
    public string? Voivodeship { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_address_city"]/summary' />
    [Display(Name = nameof(Response.institution_address_city), ResourceType = typeof(Response))]
    [JsonPropertyName("city")]
    public string? City { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_address_postal_cd"]/summary' />
    [Display(Name = nameof(Response.institution_address_postal_cd), ResourceType = typeof(Response))]
    [JsonPropertyName("postalCd")]
    public string? PostalCd { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_address_street"]/summary' />
    [Display(Name = nameof(Response.institution_address_street), ResourceType = typeof(Response))]
    [JsonPropertyName("street")]
    public string? Street { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_address_b_number"]/summary' />
    [Display(Name = nameof(Response.institution_address_b_number), ResourceType = typeof(Response))]
    [JsonPropertyName("bNumber")]
    public string? BNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_address_l_number"]/summary' />
    [Display(Name = nameof(Response.institution_address_l_number), ResourceType = typeof(Response))]
    [JsonPropertyName("lNumber")]
    public string? LNumber { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="institution_address_date_from"]/summary' />
    [Display(Name = nameof(Response.institution_address_date_from), ResourceType = typeof(Response))]
    [JsonPropertyName("dateFrom")]
    public string? DateFrom { get; init; } = null;
}
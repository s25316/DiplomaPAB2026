using RADON.Models.Dictionaries.Responses;
using System.ComponentModel.DataAnnotations;
using Response = RADON.Models.Descriptions.Institutions.Response;

namespace RADON.Models.Institutions.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="Institution"]/summary' />
[Display(Name = nameof(Response.Institution), ResourceType = typeof(Response))]
public record Institution
{
    /// <include file='Response.xml' path='docs/members/member[@name="NameSnapshot"]/summary' />
    [Display(Name = nameof(Response.NameSnapshot), ResourceType = typeof(Response))]
    public record NameSnapshot
    {
        /// <include file='Response.xml' path='docs/members/member[@name="NameSnapshot_Name"]/summary' />
        [Display(Name = nameof(Response.NameSnapshot_Name), ResourceType = typeof(Response))]
        public required string Name { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="NameSnapshot_Date"]/summary' />
        [Display(Name = nameof(Response.NameSnapshot_Date), ResourceType = typeof(Response))]
        public required DateOnly Date { get; init; }
    }

    /// <include file='Response.xml' path='docs/members/member[@name="TypeSnapshot"]/summary' />
    [Display(Name = nameof(Response.TypeSnapshot), ResourceType = typeof(Response))]
    public record TypeSnapshot
    {
        /// <include file='Response.xml' path='docs/members/member[@name="TypeSnapshot_Type"]/summary' />
        [Display(Name = nameof(Response.TypeSnapshot_Type), ResourceType = typeof(Response))]
        public required DictionaryItem Type { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="TypeSnapshot_Date"]/summary' />
        [Display(Name = nameof(Response.TypeSnapshot_Date), ResourceType = typeof(Response))]
        public required DateOnly Date { get; init; }
    }

    /// <include file='Response.xml' path='docs/members/member[@name="StatusSnapshot"]/summary' />
    [Display(Name = nameof(Response.StatusSnapshot), ResourceType = typeof(Response))]
    public record StatusSnapshot
    {
        /// <include file='Response.xml' path='docs/members/member[@name="StatusSnapshot_Status"]/summary' />
        [Display(Name = nameof(Response.StatusSnapshot_Status), ResourceType = typeof(Response))]
        public required DictionaryItem Status { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="StatusSnapshot_Date"]/summary' />
        [Display(Name = nameof(Response.StatusSnapshot_Date), ResourceType = typeof(Response))]
        public required DateOnly Date { get; init; }
    }


    /// <include file='Response.xml' path='docs/members/member[@name="Institution_InstitutionUuid"]/summary' />
    [Display(Name = nameof(Response.Institution_InstitutionUuid), ResourceType = typeof(Response))]
    public required Guid InstitutionUuid { get; init; }


    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Regon"]/summary' />
    [Display(Name = nameof(Response.Institution_Regon), ResourceType = typeof(Response))]
    public required string? Regon { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Nip"]/summary' />
    [Display(Name = nameof(Response.Institution_Nip), ResourceType = typeof(Response))]
    public required string? Nip { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Krs"]/summary' />
    [Display(Name = nameof(Response.Institution_Krs), ResourceType = typeof(Response))]
    public required string? Krs { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="Institution_StartDate"]/summary' />
    [Display(Name = nameof(Response.Institution_StartDate), ResourceType = typeof(Response))]
    public required DateOnly StartDate { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_LiquidationStartDate"]/summary' />
    [Display(Name = nameof(Response.Institution_LiquidationStartDate), ResourceType = typeof(Response))]
    public required DateOnly? LiquidationStartDate { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_LiquidationDate"]/summary' />
    [Display(Name = nameof(Response.Institution_LiquidationDate), ResourceType = typeof(Response))]
    public required DateOnly? LiquidationDate { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Www"]/summary' />
    [Display(Name = nameof(Response.Institution_Www), ResourceType = typeof(Response))]
    public required string? Www { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Email"]/summary' />
    [Display(Name = nameof(Response.Institution_Email), ResourceType = typeof(Response))]
    public required string? Email { get; init; } = null;

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Phone"]/summary' />
    [Display(Name = nameof(Response.Institution_Phone), ResourceType = typeof(Response))]
    public required string? Phone { get; init; } = null;


    /// <include file='Response.xml' path='docs/members/member[@name="Institution_InstitutionKind"]/summary' />
    [Display(Name = nameof(Response.Institution_InstitutionKind), ResourceType = typeof(Response))]
    public required DictionaryItem InstitutionKind { get; init; } = null!;

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Names"]/summary' />
    [Display(Name = nameof(Response.Institution_Names), ResourceType = typeof(Response))]
    public required ICollection<NameSnapshot> Names { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Types"]/summary' />
    [Display(Name = nameof(Response.Institution_Types), ResourceType = typeof(Response))]
    public required ICollection<TypeSnapshot> Types { get; init; } = [];

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_Statuses"]/summary' />
    [Display(Name = nameof(Response.Institution_Statuses), ResourceType = typeof(Response))]
    public required ICollection<StatusSnapshot> Statuses { get; init; } = [];


    /// <include file='Response.xml' path='docs/members/member[@name="Institution_LastRefresh"]/summary' />
    [Display(Name = nameof(Response.Institution_LastRefresh), ResourceType = typeof(Response))]
    public required DateTimeOffset LastRefresh { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_SourceLastRefresh"]/summary' />
    [Display(Name = nameof(Response.Institution_SourceLastRefresh), ResourceType = typeof(Response))]
    public required DateTimeOffset SourceLastRefresh { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="Institution_DataSource"]/summary' />
    [Display(Name = nameof(Response.Institution_DataSource), ResourceType = typeof(Response))]
    public required string DataSource { get; init; } = null!;
}
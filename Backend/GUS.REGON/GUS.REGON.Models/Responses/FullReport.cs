using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Response = GUS.REGON.Models.Descriptions.Response;

namespace GUS.REGON.Models.Responses;

public sealed partial class Report
{
    /// <include file='Response.xml' path='docs/members/member[@name="Full"]/summary' />
    [Display(Name = nameof(Response.Full), ResourceType = typeof(Response))]
    public sealed class Full
    {
        /// <include file='Response.xml' path='docs/members/member[@name="Full_HasValue"]/summary' />
        [Display(Name = nameof(Response.Full_HasValue), ResourceType = typeof(Response))]
        [MemberNotNullWhen(true, nameof(Institution), nameof(Pkd))]
        [JsonIgnore]
        public bool HasValue => Status == Status.Istneje;

        /// <include file='Response.xml' path='docs/members/member[@name="Institution_Regon"]/summary' />
        [Display(Name = nameof(Response.Institution_Regon), ResourceType = typeof(Response))]
        public required string Regon { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="Full_Status"]/summary' />
        [Display(Name = nameof(Response.Full_Status), ResourceType = typeof(Response))]
        public required Status Status { get; init; }

        /// <include file='Response.xml' path='docs/members/member[@name="Full_Institution"]/summary' />
        [Display(Name = nameof(Response.Full_Institution), ResourceType = typeof(Response))]
        public Institution? Institution { get; init; } = null;

        /// <include file='Response.xml' path='docs/members/member[@name="Full_Pkd"]/summary' />
        [Display(Name = nameof(Response.Full_Pkd), ResourceType = typeof(Response))]
        public Pkd? Pkd { get; init; } = null;
    }
}
using System.ComponentModel.DataAnnotations;
using Response = GUS.REGON.Models.Descriptions.Response;

namespace GUS.REGON.Models.Responses;

public sealed partial class Report
{
    /// <include file='Response.xml' path='docs/members/member[@name="Pkd"]/summary' />
    [Display(Name = nameof(Response.Pkd), ResourceType = typeof(Response))]
    public sealed record Pkd
    {
        /// <include file='Response.xml' path='docs/members/member[@name="Pkd_Item"]/summary' />
        [Display(Name = nameof(Response.Pkd_Item), ResourceType = typeof(Response))]
        public sealed record Item
        {
            /// <include file='Response.xml' path='docs/members/member[@name="Pkd_Item_Pkd"]/summary' />
            [Display(Name = nameof(Response.Pkd_Item_Pkd), ResourceType = typeof(Response))]
            public required DictionaryItem Pkd { get; init; }

            /// <include file='Response.xml' path='docs/members/member[@name="Pkd_Item_IsMain"]/summary' />
            [Display(Name = nameof(Response.Pkd_Item_IsMain), ResourceType = typeof(Response))]
            public required bool IsMain { get; init; }
        }


        /// <include file='Response.xml' path='docs/members/member[@name="Pkd_Items"]/summary' />
        [Display(Name = nameof(Response.Pkd_Items), ResourceType = typeof(Response))]
        public required IEnumerable<Item> Items { get; init; } = [];
    }
}
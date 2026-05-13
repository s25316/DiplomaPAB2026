using System.ComponentModel.DataAnnotations;
using Response = GUS.REGON.Models.Descriptions.Response;

namespace GUS.REGON.Models.Responses;

public sealed partial class Report
{
    public enum Status
    {
        /// <include file='Response.xml' path='docs/members/member[@name="Status_Istneje"]/summary' />
        [Display(Name = nameof(Response.Status_Istneje), ResourceType = typeof(Response))]
        Istneje = 1,

        /// <include file='Response.xml' path='docs/members/member[@name="Status_NieIstneje"]/summary' />
        [Display(Name = nameof(Response.Status_NieIstneje), ResourceType = typeof(Response))]
        NieIstneje = 2,

        /// <include file='Response.xml' path='docs/members/member[@name="Status_BrakUprawnien"]/summary' />
        [Display(Name = nameof(Response.Status_BrakUprawnien), ResourceType = typeof(Response))]
        BrakUprawnien = 3,
    }
}
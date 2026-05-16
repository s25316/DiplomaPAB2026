using System.ComponentModel.DataAnnotations;
using Response = GUS.REGON.Models.Descriptions.Response;

namespace GUS.REGON.Models.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="ServerTimeResponse"]/summary' />
[Display(Name = nameof(Response.ServerTimeResponse), ResourceType = typeof(Response))]
public sealed record ServerTimeResponse
{
    /// <include file='Response.xml' path='docs/members/member[@name="ServerTimeResponse_CurrentDateTime"]/summary' />
    [Display(Name = nameof(Response.ServerTimeResponse_CurrentDateTime), ResourceType = typeof(Response))]
    public DateTimeOffset CurrentDateTime { get; init; } = DateTimeOffset.Now;
}
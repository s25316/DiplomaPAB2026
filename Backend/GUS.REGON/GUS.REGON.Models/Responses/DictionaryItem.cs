using System.ComponentModel.DataAnnotations;
using Response = GUS.REGON.Models.Descriptions.Response;

namespace GUS.REGON.Models.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="DictionaryItem"]/summary' />
[Display(Name = nameof(Response.DictionaryItem), ResourceType = typeof(Response))]
public sealed class DictionaryItem
{
    /// <include file='Response.xml' path='docs/members/member[@name="DictionaryItem_Code"]/summary' />
    [Display(Name = nameof(Response.DictionaryItem_Code), ResourceType = typeof(Response))]
    public required string Code { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="DictionaryItem_Nazwa"]/summary' />
    [Display(Name = nameof(Response.DictionaryItem_Nazwa), ResourceType = typeof(Response))]
    public required string Nazwa { get; init; }
}
using System.ComponentModel.DataAnnotations;
using Response = RADON.Models.Descriptions.Dictionaries.Response;

namespace RADON.Models.Dictionaries.Responses;

/// <include file='Response.xml' path='docs/members/member[@name="DictionaryItem"]/summary' />
[Display(Name = nameof(Response.DictionaryItem), ResourceType = typeof(Response))]
public sealed record DictionaryItem
{
    /// <include file='Response.xml' path='docs/members/member[@name="DictionaryItem_Code"]/summary' />
    [Display(Name = nameof(Response.DictionaryItem_Code), ResourceType = typeof(Response))]
    public required string Code { get; init; }

    /// <include file='Response.xml' path='docs/members/member[@name="DictionaryItem_Name"]/summary' />
    [Display(Name = nameof(Response.DictionaryItem_Name), ResourceType = typeof(Response))]
    public required string Name { get; init; }
};
using System.ComponentModel.DataAnnotations;

namespace Base.Models.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class PageAttribute : RangeAttribute
{
    public PageAttribute() : base(1, int.MaxValue)
    {
        ErrorMessage = Messages.ErrorMessagePage;
    }
}

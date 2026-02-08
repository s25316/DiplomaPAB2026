using System.ComponentModel.DataAnnotations;

namespace Base.Models.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ItemsPerPageAttribute : RangeAttribute
{
    public ItemsPerPageAttribute() : base(1, int.MaxValue)
    {
        ErrorMessage = Messages.ErrorMessageItemsPerPage;
    }
}

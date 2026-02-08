// Ignore spelling: Miejscowosc
using GUS.TERYT.Models.Requests.Parameters;
using System.ComponentModel.DataAnnotations;

namespace GUS.TERYT.Models.Requests.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
internal class MiejscowoscTypeIdAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        if (value is MiejscowoscTypeId item)
        {
            if (!Regexes.MiejscowoscType.IsMatch(item.Value))
            {
                return new ValidationResult($"{Messages.ErrorMessageAttributeMiejscowoscTypeId}: {item.Value}");
            }
            return ValidationResult.Success;
        }
        return new ValidationResult($"{Messages.ErrorMessageAttributeUnknownFormat}: {value.GetType().Name}");
    }
}
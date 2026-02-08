// Ignore Spelling: Powiat
using GUS.TERYT.Models.Requests.Parameters;
using System.ComponentModel.DataAnnotations;

namespace GUS.TERYT.Models.Requests.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
public class PowiatIdAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        if (value is PowiatId item)
        {
            var stringValue = item.ToString();
            if (!Regexes.Powiat.IsMatch(stringValue))
            {
                return new ValidationResult($"{Messages.ErrorMessageAttributePowiatId}: {stringValue}");
            }
            return ValidationResult.Success;
        }
        return new ValidationResult($"{Messages.ErrorMessageAttributeUnknownFormat}: {value.GetType().Name}");
    }
}
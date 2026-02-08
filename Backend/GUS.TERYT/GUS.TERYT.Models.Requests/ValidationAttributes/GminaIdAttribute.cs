// Ignore Spelling:  Gmina
using GUS.TERYT.Models.Requests.Parameters;
using System.ComponentModel.DataAnnotations;

namespace GUS.TERYT.Models.Requests.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
public class GminaIdAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        if (value is GminaId item)
        {
            var stringValue = item.ToString();
            if (!Regexes.Gmina.IsMatch(stringValue))
            {
                return new ValidationResult($"{Messages.ErrorMessageAttributeGminaId}: {stringValue}");
            }
            return ValidationResult.Success;
        }
        return new ValidationResult($"{Messages.ErrorMessageAttributeUnknownFormat}: {value.GetType().Name}");
    }
}
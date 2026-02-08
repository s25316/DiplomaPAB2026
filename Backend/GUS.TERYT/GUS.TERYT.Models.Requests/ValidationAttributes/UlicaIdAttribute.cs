// Ignore Spelling: Ulica
using GUS.TERYT.Models.Requests.Parameters;
using System.ComponentModel.DataAnnotations;

namespace GUS.TERYT.Models.Requests.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
public class UlicaIdAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        if (value is UlicaId item)
        {
            if (!Regexes.Ulica.IsMatch(item.Value))
            {
                return new ValidationResult($"{Messages.ErrorMessageAttributeUlicaId}: {item.Value}");
            }
            return ValidationResult.Success;
        }
        return new ValidationResult($"{Messages.ErrorMessageAttributeUnknownFormat}: {value.GetType().Name}");
    }
}
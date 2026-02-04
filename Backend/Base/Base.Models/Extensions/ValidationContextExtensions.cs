using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Base.Models.Extensions;

public static class ValidationContextExtensions
{
    public static void SetValue(this ValidationContext validationContext, object? value)
    {
        ArgumentNullException.ThrowIfNull(validationContext);

        string propertyName = validationContext.MemberName
            ?? throw new InvalidOperationException("MemberName is null. SetValue can only be used when validating a specific property.");

        var property = validationContext.ObjectType.GetProperty(
            propertyName,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
            ?? throw new ArgumentException($"Property '{propertyName}' was not found on type '{validationContext.ObjectType.Name}'.");

        if (!property.CanWrite)
        {
            throw new InvalidOperationException($"Property '{propertyName}' on type '{validationContext.ObjectType.Name}' does not have a setter.");
        }

        try
        {
            property.SetValue(validationContext.ObjectInstance, value);
        }
        catch (TargetInvocationException ex)
        {
            throw new InvalidOperationException($"An error occurred while setting the value for property '{propertyName}'.", ex.InnerException);
        }
    }

    public static ValidationResult? PrepareResult<T>(
        this ValidationContext validationContext,
        Func<T> prepareItemFunc)
    {
        try
        {
            var item = prepareItemFunc();
            validationContext.SetValue(item);
            return ValidationResult.Success;
        }
        catch (Exception ex)
        {
            return new ValidationResult(ex.Message);
        }
    }
}

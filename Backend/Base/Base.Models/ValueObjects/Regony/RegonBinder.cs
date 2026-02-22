// Ignore Spelling: Regony, Regon
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Base.Models.ValueObjects.Regony;

public sealed class RegonBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(value)) return Task.CompletedTask;

        if (Regon.TryParse(value, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, Regon.PrepareErrorMessage(value));
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
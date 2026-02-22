// Ignore Spelling: Krsy, Krs
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Base.Models.ValueObjects.Krsy;

public sealed class KrsBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(value)) return Task.CompletedTask;

        if (Krs.TryParse(value, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, Krs.PrepareErrorMessage(value));
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
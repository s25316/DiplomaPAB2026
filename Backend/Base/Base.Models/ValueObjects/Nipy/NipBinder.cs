using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Base.Models.ValueObjects.Nipy;

public sealed class NipBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(value)) return Task.CompletedTask;

        if (Nip.TryParse(value, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, Nip.PrepareErrorMessage(value));
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
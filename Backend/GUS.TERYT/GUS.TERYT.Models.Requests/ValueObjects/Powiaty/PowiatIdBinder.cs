// Ignore Spelling: Powiaty, Powiat, Wojewodztwo
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GUS.TERYT.Models.Requests.ValueObjects.Powiaty;

public sealed class PowiatIdBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(value)) return Task.CompletedTask;

        if (PowiatId.TryParse(value, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, PowiatId.PrepareErrorMessage(value));
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
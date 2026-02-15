// Ignore Spelling: Miejscowosci, Miejscowosc
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;

public sealed class MiejscowoscIdBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(value)) return Task.CompletedTask;

        if (MiejscowoscId.TryParse(value, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, MiejscowoscId.PrepareErrorMessage(value));
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
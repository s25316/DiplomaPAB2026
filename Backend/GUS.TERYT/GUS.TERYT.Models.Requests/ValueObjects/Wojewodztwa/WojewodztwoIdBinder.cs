// Ignore Spelling: Wojewodztwo, Wojewodztwa
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;

public sealed class WojewodztwoIdBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(value)) return Task.CompletedTask;

        if (WojewodztwoId.TryParse(value, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, WojewodztwoId.PrepareErrorMessage(value));
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
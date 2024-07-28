using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Text.Json;
using System.Threading.Tasks;

public class GenericModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        try
        {
            var result = JsonSerializer.Deserialize(value, bindingContext.ModelType);
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        catch (JsonException ex)
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Invalid JSON: {ex.Message}");
        }

        return Task.CompletedTask;
    }
}

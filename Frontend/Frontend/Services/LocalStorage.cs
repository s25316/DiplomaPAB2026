using Microsoft.JSInterop;
using System.Text.Json;

namespace Frontend.Services;

public interface ILocalStorage
{
    Task<T?> GetItemAsync<T>(string key)
        where T : class;
    Task SetItemAsync<T>(string key, T value);
    Task RemoveItemAsync(string key);
}

public class LocalStorage(
    IJSRuntime jsRuntime
    ) : ILocalStorage
{
    public async Task<T?> GetItemAsync<T>(string key)
        where T : class
    {
        var json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        if (string.IsNullOrWhiteSpace(json))
            return null;

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        await RemoveItemAsync(key);
        var json = JsonSerializer.Serialize(value);
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
    }

    public async Task RemoveItemAsync(string key)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}
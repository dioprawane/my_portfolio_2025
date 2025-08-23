using System.Text.Json;
using Microsoft.JSInterop;

namespace BlazorPortfolio.Services
{
    public class LocalCache
    {
        private readonly IJSRuntime _js;
        public LocalCache(IJSRuntime js) => _js = js;

        public async Task SetJsonAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await _js.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        public async Task<T?> GetJsonAsync<T>(string key)
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", key);
            return string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<T>(json);
        }

        public Task RemoveAsync(string key) => _js.InvokeVoidAsync("localStorage.removeItem", key).AsTask();
    }
}

using System.Text;
using System.Text.Json;

namespace Core.Http;

public static class DriverClient
{
    public static readonly HttpClient Http = new()
    {
        BaseAddress = new Uri("http://127.0.0.1:4444"),
    };

    public static async Task<T?> GetAsync<T>(string requestUri) where T : class
    {
        var response = await Http.GetAsync(requestUri);
        var rawResponse = await response.Content.ReadAsStringAsync();
        
        var data = JsonSerializer.Deserialize<WebDriverReponse<T>>(rawResponse);
        return data?.Value;
    }

    public static Task<T?> PostAsync<T>(string? requestUri, object body) where T : class
    {
        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        return PostAsync<T>(requestUri, content);
    }
    
    public static async Task<T?> PostAsync<T>(string? requestUri, HttpContent content) where T : class
    {
        var response = await Http.PostAsync(requestUri, content);
        var rawResponse = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<WebDriverReponse<T>>(rawResponse);

        return data?.Value;
    }
}
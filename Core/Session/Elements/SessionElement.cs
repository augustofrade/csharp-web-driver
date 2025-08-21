using System.Text.Json.Serialization;
using Core.Http;
using Core.Http.Dto.Session.Elements;

namespace Core.Session.Elements;

public class SessionElement(string sessionId, string identifier) : ElementSelector(sessionId, $"/session/{sessionId}/element/{identifier}")
{
    public async Task SendKeys(string keys, bool simulateTyping = true)
    {
        var url = $"{BaseEndpoint}/value";
        if (!simulateTyping)
        {
            await DriverClient.PostAsync<object>(url, new
            {
                text = keys
            });
            return;
        }

        foreach (var key in keys)
        {
            await DriverClient.PostAsync<object>(url, new
            {
                text = key
            });
        }
    }

    public async Task<bool> Click()
    {
        var url = $"{BaseEndpoint}/click";
        var response = await DriverClient.PostAsync<object?>(url);
        return response == null;
    }

    public string TextContent => GetTextAsync().GetAwaiter().GetResult();

    public async Task<string> GetTextAsync()
    {
        var url = $"{BaseEndpoint}/text";
        var response = await DriverClient.GetAsync<string>(url);
        return response!;
    }

    public IEnumerable<SessionElement> Children => GetChildrenAsync().GetAwaiter().GetResult();

    public async Task<IEnumerable<SessionElement>> GetChildrenAsync()
    {
        var body = new
        {
            script = "return arguments[0].children;",
            args = new List<object>
            {
                new { ELEMENT = identifier }
            }
        };
        var url = $"/session/{SessionId}/execute/sync";

        try
        {
            var response = await DriverClient.PostAsync<IEnumerable<FindElementResponse>>(url, body);
            return response == null ? [] : response.Select(el => new SessionElement(SessionId, el.ElementIdentifier));
        } catch (Exception ex)
        {
            return [];
        }
    }

    public string? Id => GetAttribute("id");
    
    public string? ClassName => GetAttribute("class");
    
    public IEnumerable<string> ClassList => GetClassListAsync().GetAwaiter().GetResult();

    public async Task<IEnumerable<string>> GetClassListAsync()
    {
        var classList = await GetAttributeAsync("class");
        return classList?.Split(" ") ?? [];
    }
    
    public string? GetAttribute(string attributeName) => GetAttributeAsync(attributeName).GetAwaiter().GetResult();

    public Task<string?> GetAttributeAsync(string attributeName)
    {
        var url = $"{BaseEndpoint}/attribute/{attributeName}";
        return DriverClient.GetAsync<string?>(url);
    }
    
    public T? GetProperty<T>(string propertyName) => GetPropertyAsync<T>(propertyName).GetAwaiter().GetResult();

    public Task<T?> GetPropertyAsync<T>(string propertyName)
    {
        var url = $"{BaseEndpoint}/property/{propertyName}";
        return DriverClient.GetAsync<T>(url);
    }

    public override string ToString()
    {
        return identifier;
    }
}
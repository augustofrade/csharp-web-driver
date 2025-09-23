using System.Text.Json.Serialization;
using Core.Http;
using Core.Session.Elements.Http;
using Core.Session.Elements.Options;

namespace Core.Session.Elements;

public class SessionElement(string sessionId, string identifier) : ElementSelector(sessionId, $"/session/{sessionId}/element/{identifier}")
{
    public void SendKeys(string keys, bool simulateTyping = true)
    {
        var url = $"{BaseEndpoint}/value";
        if (!simulateTyping)
        {
            DriverClient.PostAsync<object>(url, new
            {
                text = keys
            }).GetAwaiter().GetResult();
            return;
        }

        foreach (var key in keys)
        {
            DriverClient.PostAsync<object>(url, new
            {
                text = key
            }).GetAwaiter().GetResult();
        }
    }

    public bool Click()
    {
        var url = $"{BaseEndpoint}/click";
        var response = DriverClient.PostAsync<object?>(url).GetAwaiter().GetResult();
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
        try
        {
            var response = await ExecuteJs<IEnumerable<FindElementResponse>>("return arguments[0].children;");
            return response == null ? [] : response.Select(el => new SessionElement(SessionId, el.ElementIdentifier));
        } catch (Exception ex)
        {
            return [];
        }
    }
    
    public string TagName => ExecuteJs<string>("return arguments[0].tagName;").GetAwaiter().GetResult()?.ToLower() ?? "";

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

    public Task<T?> ExecuteJs<T>(string script, params object[] args) where T : class
    {
        var scriptArgs = new List<object>
        {
            new { ELEMENT = identifier },
        };
        
        scriptArgs.AddRange(args);
        
        var body = new
        {
            script = script,
            args = scriptArgs
        };
        var url = $"/session/{SessionId}/execute/sync";

        return DriverClient.PostAsync<T>(url, body);
    }
    
    public void ScrollIntoView(ElementAlignToTopOptions? options = null)
    {
        options ??= new ElementAlignToTopOptions();
        var behavior = options.Behavior.ToString().ToLower();
        var alignment = options.Alignment.ToString().ToLower();
        const string script = "arguments[0].scrollIntoView({ behavior: arguments[1], block: arguments[2] });";
        
        ExecuteJs<object>(script, behavior, alignment)
            .GetAwaiter().GetResult();
    }

    public override string ToString()
    {
        return identifier;
    }
}
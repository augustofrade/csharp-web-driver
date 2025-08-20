using System.Text.Json.Serialization;
using Core.Http;
using Core.Http.Dto.Session.Elements;

namespace Core.Session.Elements;

public class SessionElement(string sessionId, string identifier) : ElementSelector(sessionId, $"/session/{sessionId}/element/{identifier}")
{

    public async Task SendKeys(string keys, bool simulateTyping = true)
    {
        var url = $"/session/{sessionId}/element/{identifier}/value";
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
        var url = $"/session/{sessionId}/element/{identifier}/click";
        var response = await DriverClient.PostAsync<string?>(url);
        return response == null;
    }

    public string TextContent => GetTextAsync().GetAwaiter().GetResult();

    public async Task<string> GetTextAsync()
    {
        var url = $"/session/{sessionId}/element/{identifier}/text";
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
        var url = $"/session/{sessionId}/execute/sync";

        try
        {
            var response = await DriverClient.PostAsync<IEnumerable<FindElementResponse>>(url, body);
            return response == null ? [] : response.Select(el => new SessionElement(sessionId, el.ElementIdentifier));
        } catch (Exception ex)
        {
            return [];
        }
    }
}
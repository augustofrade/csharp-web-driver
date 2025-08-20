using System.Text.Json.Serialization;
using Core.Http;
using Core.Http.Dto.Session.Elements;

namespace Core.Session.Elements;

public class SessionElement(string sessionId, string identifier) : ElementSelector(sessionId, $"/session/{sessionId}/element/{identifier}")
{
    private readonly string _sessionId = sessionId;
    private readonly string _identifier = identifier;

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

    public async Task<string> GetText()
    {
        var url = $"/session/{sessionId}/element/{identifier}/text";
        var response = await DriverClient.GetAsync<string>(url);
        return response!;
    }
}
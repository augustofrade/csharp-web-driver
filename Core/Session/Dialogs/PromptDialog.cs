using Core.Http;

namespace Core.Session.Dialogs;

public class PromptDialog(string sessionId) : SimpleDialog(sessionId)
{
    public Task SendText(string text)
    {
        var url = $"{BaseEndpoint}/text";
        return DriverClient.PostAsync<object?>(url, new
        {
            text = text
        });
    }
}
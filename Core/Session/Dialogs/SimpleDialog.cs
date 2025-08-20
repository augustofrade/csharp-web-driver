using Core.Http;

namespace Core.Session.Dialogs;

public class SimpleDialog : ISimpleDialog
{
    protected readonly string SessionId;
    protected readonly string BaseEndpoint;
    
    public SimpleDialog(string sessionId)
    {
        SessionId = sessionId;
        BaseEndpoint = $"/session/{sessionId}/alert";
    }

    public Task Accept()
    {
        var url = $"{BaseEndpoint}/accept";
        return DriverClient.PostAsync<object?>(url);
    }

    public Task Dismiss()
    {
        var url = $"{BaseEndpoint}/dismiss";
        return DriverClient.PostAsync<object?>(url);
    }

    public async Task<string> GetText()
    {
        var url = $"{BaseEndpoint}/text";
        var result = await DriverClient.GetAsync<string>(url);
        return result ?? string.Empty;
    }
}
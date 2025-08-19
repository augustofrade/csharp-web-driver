using Core.Http;

namespace Core.Session;

public class SessionLocation(string sessionId)
{
    private string SessionId = sessionId;
    
    public async Task NavigateTo(string url)
    {
        var response = await DriverClient.PostAsync<object>($"/session/{SessionId}/url", new
        {
            url = url,
        });
    }

    public async Task<string> CurrentUrl()
    {
        var response = await DriverClient.GetAsync<string>($"/session/{SessionId}/url");
        return response!;
    }
    
    public async Task<bool> Refresh()
    {
        var response = await DriverClient.PostAsync<string?>($"/session/{SessionId}/refresh", new {});
        return response == null;
    }
}
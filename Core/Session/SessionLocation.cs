using Core.Http;

namespace Core.Session;

public class SessionLocation(string sessionId)
{
    private readonly string _sessionId = sessionId;
    
    public async Task NavigateTo(string url)
    {
        var response = await DriverClient.PostAsync<object>($"/session/{_sessionId}/url", new
        {
            url = url,
        });
    }
    
    public string Title => GetTitleAsync().GetAwaiter().GetResult();
    
    public async Task<string> GetTitleAsync()
    {
        var response = await DriverClient.GetAsync<string>($"/session/{_sessionId}/title");
        return response!;
    }

    public async Task<string> CurrentUrl()
    {
        var response = await DriverClient.GetAsync<string>($"/session/{_sessionId}/url");
        return response!;
    }
    
    public async Task<bool> Refresh()
    {
        var response = await DriverClient.PostAsync<string?>($"/session/{_sessionId}/refresh");
        return response == null;
    }
    
    public async Task<bool> Back()
    {
        var response = await DriverClient.PostAsync<string?>($"/session/{_sessionId}/back");
        return response == null;
    }
    
    public async Task<bool> Forward()
    {
        var response = await DriverClient.PostAsync<string?>($"/session/{_sessionId}/forward");
        return response == null;
    }
}
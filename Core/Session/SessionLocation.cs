using Core.Http;

namespace Core.Session;

/// <summary>
/// Location related operations in the Session.
/// </summary>
public class SessionLocation(string sessionId)
{
    private readonly string _sessionId = sessionId;
    
    /// <summary>
    /// Navigates to the passed URL.
    /// </summary>
    public async Task NavigateTo(string url)
    {
        await DriverClient.PostAsync<object>($"/session/{_sessionId}/url", new
        {
            url = url,
        });
    }
    
    /// <summary>
    /// Gets the title of the current page of the current top-level browsing context (tab or window) of the Session.
    /// </summary>
    public string Title => GetTitleAsync().GetAwaiter().GetResult();
    
    /// <summary>
    /// Asynchronously gets the title of the current page of the current top-level browsing context (tab or window)
    /// of the Session.
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetTitleAsync()
    {
        var response = await DriverClient.GetAsync<string>($"/session/{_sessionId}/title");
        return response!;
    }

    /// <summary>
    /// Asynchronously gets the current URL of the current page of the current top-level browsing context (tab or window)
    /// of the Session
    /// </summary>
    /// <returns></returns>
    public async Task<string> CurrentUrl()
    {
        var response = await DriverClient.GetAsync<string>($"/session/{_sessionId}/url");
        return response!;
    }
    
    /// <summary>
    /// Refreshes the current page of the current top-level browsing context (tab or window) of the Session
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Refresh()
    {
        var response = await DriverClient.PostAsync<string?>($"/session/{_sessionId}/refresh");
        return response == null;
    }
    
    /// <summary>
    /// Goes back in the History of the current top-level browsing context (tab or window) of the Session
    /// </summary>
    public async Task<bool> Back()
    {
        var response = await DriverClient.PostAsync<string?>($"/session/{_sessionId}/back");
        return response == null;
    }
    
    /// <summary>
    /// Moves forward in the History of the current top-level browsing context (tab or window) of the Session.
    /// <br/>
    /// Only works if the History has gone back at least one time.
    /// </summary>
    public async Task<bool> Forward()
    {
        var response = await DriverClient.PostAsync<string?>($"/session/{_sessionId}/forward");
        return response == null;
    }
}
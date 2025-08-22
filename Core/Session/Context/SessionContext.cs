using Core.Http;
using Core.Session.Context;
using Core.Session.Elements;

namespace Core.Session;

public class SessionContext(string sessionId)
{
    public SessionContextRectManager Rect = new(sessionId);
    
    public async Task SwitchToParentFrame()
    {
        var url = $"/session/{sessionId}/frame/parent";
        await DriverClient.PostAsync<string?>(url);
    }
    
    public Task SwitchToFrame(SessionElement frame) => _SwitchToFrame(frame.ToString());
    
    public Task Reset() => _SwitchToFrame(null);

    private async Task _SwitchToFrame(string? frameIdentifier)
    {
        var body = new
        {
            id = frameIdentifier == null ? null : new Dictionary<string, string>()
            {
                { "element-6066-11e4-a52e-4f735466cecf", frameIdentifier }
                
            }
        };
        var url = $"/session/{sessionId}/frame";
        await DriverClient.PostAsync<string?>(url, body);
    }
}
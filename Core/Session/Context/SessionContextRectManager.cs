using Core.Http;

namespace Core.Session.Context;

public class SessionContextRectManager(string sessionId)
{
    public SessionContextRect Get()
    {
        return DriverClient.GetAsync<SessionContextRect>($"/session/{sessionId}/window/rect").GetAwaiter().GetResult()!;
    }

    public SessionContextRect Set(SessionContextRect rect)
    {
        return Set(rect.x, rect.y, rect.width, rect.height);
    }
    
    public SessionContextRect SetPosition(int x, int y)
    {
        return _Set(x, y, null, null);
    }
    
    public SessionContextRect SetSize(int width, int height)
    {
        return _Set(null, null, width, height);
    }
    
    public SessionContextRect Set(int x, int y, int width, int height)
    {
        return _Set(x, y, width, height);
    }

    private SessionContextRect _Set(int? x, int? y, int? width, int? height)
    {
        var url = $"/session/{sessionId}/window/rect";
        var request = DriverClient.PostAsync<SessionContextRect>(url, new
        {
            x = x,
            y = y,
            width = width,
            height = height
        });
        
        return request.GetAwaiter().GetResult()!;
    }
}
using Core.Http;
using Core.Session;

namespace Core;

public class WebDriverSession(string id)
{
    public SessionLocation Location = new SessionLocation(id);
    public SessionElementSelector Get = new SessionElementSelector(id);
    
    private string Id { get; init; } = id;
}
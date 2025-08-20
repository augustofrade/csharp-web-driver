using Core.Http;
using Core.Session;
using Core.Session.Elements;

namespace Core;

public class WebDriverSession(string id)
{
    public SessionLocation Location = new SessionLocation(id);
    public IElementSelector Dom = new SessionElementSelector(id);
    
    private string Id { get; init; } = id;
}
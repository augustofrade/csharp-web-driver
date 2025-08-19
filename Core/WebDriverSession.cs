using Core.Http;
using Core.Session;

namespace Core;

public class WebDriverSession(string id)
{
    public SessionLocation Location = new SessionLocation(id);
    
    private string Id { get; init; } = id;
}
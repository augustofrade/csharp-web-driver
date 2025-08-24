using Core.Http;
using Core.Session.Elements.Http;

namespace Core.Session.Elements;

public abstract class ElementSelector : IElementSelector
{
    protected readonly string SessionId;
    protected readonly string BaseEndpoint;

    protected ElementSelector(string sessionId, string baseEndpoint)
    {
        SessionId = sessionId;
        BaseEndpoint = baseEndpoint;
    }
    
    public SessionElement? QuerySelector(string query)
    {
        return GetElement("css selector", query);
    }
    
    public IEnumerable<SessionElement> QuerySelectorAll(string query)
    {
        return GetElements("css selector", query);
    }
    
    public SessionElement? GetElementById(string elementId)
    {
        return GetElement("css selector", $"#{elementId}");
    }
    
    public SessionElement? GetElementByTagName(string tag)
    {
        return GetElement("tag name", tag);
    }
    
    public IEnumerable<SessionElement> GetElementsByTagName(string tag)
    {
        return GetElements("tag name", tag);
    }
    
    public SessionElement? GetElementByXPath(string xpath)
    {
        return GetElement("xpath", xpath);
    }
    
    public IEnumerable<SessionElement> GetElementsByXPath(string xpath)
    {
        return GetElements("xpath", xpath);
    }
    
    public IEnumerable<SessionElement> GetElementsByClassName(string className)
    {
        return GetElements("css selector", $".{className}");
    }
    
    public SessionElement? GetElementByClassName(string className)
    {
        return GetElement("css selector", $".{className}");
    }

    private SessionElement? GetElement(string selector, string value)
    {
        var body = new Dictionary<string, string>
        {
            { "using", selector },
            { "value", value }
        };
        var url = $"{BaseEndpoint}/element";
        
        try
        {

            var response = DriverClient.PostAsync<FindElementResponse?>(url, body).GetAwaiter().GetResult();
            return response == null ? null : new SessionElement(SessionId, response.ElementIdentifier);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private IEnumerable<SessionElement> GetElements(string selector, string value)
    {
        var body = new Dictionary<string, string>
        {
            { "using", selector },
            { "value", value }
        };
        var url = $"{BaseEndpoint}/elements";
        
        try
        {
            var response = DriverClient.PostAsync<IEnumerable<FindElementResponse>?>(url, body).GetAwaiter().GetResult();
            return response == null ? [] : response.Select(el => new SessionElement(SessionId, el.ElementIdentifier));
        }
        catch (Exception ex)
        {
            return [];
        }
    }
}
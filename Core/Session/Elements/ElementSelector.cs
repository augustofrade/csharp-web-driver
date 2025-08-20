using Core.Http;
using Core.Http.Dto.Session.Elements;

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
    
    public Task<SessionElement?> QuerySelector(string query)
    {
        return GetElement("css selector", query);
    }
    
    public Task<IEnumerable<SessionElement>> QuerySelectorAll(string query)
    {
        return GetElements("css selector", query);
    }
    
    public Task<SessionElement?> GetElementById(string elementId)
    {
        return GetElement("css selector", $"#{elementId}");
    }
    
    public Task<SessionElement?> GetElementByTagName(string tag)
    {
        return GetElement("tag name", tag);
    }
    
    public Task<IEnumerable<SessionElement>> GetElementsByTagName(string tag)
    {
        return GetElements("tag name", tag);
    }
    
    public Task<SessionElement?> GetElementByXPath(string xpath)
    {
        return GetElement("xpath", xpath);
    }
    
    public Task<IEnumerable<SessionElement>> GetElementsByXPath(string xpath)
    {
        return GetElements("xpath", xpath);
    }
    
    public Task<IEnumerable<SessionElement>> GetElementsByClassName(string className)
    {
        return GetElements("css selector", $".{className}");
    }
    
    public Task<SessionElement?> GetElementByClassName(string className)
    {
        return GetElement("css selector", $".{className}");
    }

    private async Task<SessionElement?> GetElement(string selector, string value)
    {
        var body = new Dictionary<string, string>
        {
            { "using", selector },
            { "value", value }
        };
        var url = $"{BaseEndpoint}/element";
        
        try
        {

            var response = await DriverClient.PostAsync<FindElementResponse?>(url, body);
            return response == null ? null : new SessionElement(SessionId, response.ElementIdentifier);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private async Task<IEnumerable<SessionElement>> GetElements(string selector, string value)
    {
        var body = new Dictionary<string, string>
        {
            { "using", selector },
            { "value", value }
        };
        var url = $"{BaseEndpoint}/elements";
        
        try
        {
            var response = await DriverClient.PostAsync<IEnumerable<FindElementResponse>?>(url, body);
            return response == null ? [] : response.Select(el => new SessionElement(SessionId, el.ElementIdentifier));
        }
        catch (Exception ex)
        {
            return [];
        }
    }
}
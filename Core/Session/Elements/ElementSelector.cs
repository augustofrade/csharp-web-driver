using Core.Http;
using Core.Session.Elements.Http;

namespace Core.Session.Elements;

/// <summary>
/// Concrete implementation of IElementSelector
/// <br/><br/>
/// Has the same syntax as JavaScript with some helper methods.
/// </summary>
public abstract class ElementSelector : IElementSelector
{
    /// <summary>
    /// ID of the Session
    /// </summary>
    protected readonly string SessionId;
    /// <summary>
    /// Base endpoint to be used to fetch elements from the page.
    /// <br/>
    /// The only difference of finding elements between the Document root and within another element
    /// is the endpoint.
    /// <br/>
    /// <a href="https://www.w3.org/TR/webdriver1/#find-element">Find Element</a>
    /// <br/>
    /// <a href="https://www.w3.org/TR/webdriver1/#find-element-from-element">Find Element From Element</a>
    /// </summary>
    protected readonly string BaseEndpoint;

    protected ElementSelector(string sessionId, string baseEndpoint)
    {
        SessionId = sessionId;
        BaseEndpoint = baseEndpoint;
    }
    
    /// <summary>
    /// Finds elements based on the provided CSS selector
    /// </summary>
    /// <example>
    /// var titleTextElement = QuerySelector(".submission .title span");
    /// </example>
    /// <returns><see cref="SessionElement"/> if element is found, otherwise null</returns>
    public SessionElement? QuerySelector(string cssSelector)
    {
        return GetElement("css selector", cssSelector);
    }
    
    /// <summary>
    /// Finds all elements based on the provided CSS selector
    /// </summary>
    /// <example>
    /// var posts = QuerySelectorAll(".submission");
    /// </example>
    /// <returns>Collection of <see cref="SessionElement"/>s if any element is found, otherwise null</returns>
    public IEnumerable<SessionElement> QuerySelectorAll(string cssSelector)
    {
        return GetElements("css selector", cssSelector);
    }
    
    /// <summary>
    /// Finds an element by its ID attribute
    /// </summary>
    /// <param name="elementId">ID of the element</param>
    /// <example>
    /// var postFeed = GetElementById("#feed");
    /// </example>
    /// <returns><see cref="SessionElement"/> if element is found, otherwise null</returns>
    public SessionElement? GetElementById(string elementId)
    {
        return GetElement("css selector", $"#{elementId}");
    }
    
    /// <summary>
    /// Finds an element by its tag name
    /// </summary>
    /// <param name="tag">Tag name of the element</param>
    /// <example>
    /// var title = GetElementByTagName("h1");
    /// </example>
    /// <returns><see cref="SessionElement"/> if element is found, otherwise null</returns>
    public SessionElement? GetElementByTagName(string tag)
    {
        return GetElement("tag name", tag);
    }
    
    /// <summary>
    /// Finds all elements that have the specified tag name
    /// </summary>
    /// <param name="tag">Tag name of the elements</param>
    /// <example>
    /// var titles = GetElementByTagName("h2");
    /// </example>
    /// <returns>Collection of <see cref="SessionElement"/>s if any element is found, otherwise null</returns>
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
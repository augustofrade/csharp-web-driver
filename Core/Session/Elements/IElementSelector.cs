namespace Core.Session.Elements;

public interface IElementSelector {
    Task<SessionElement?> QuerySelector(string query);
    Task<IEnumerable<SessionElement>> QuerySelectorAll(string query);
    Task<SessionElement?> GetElementById(string elementId);
    Task<SessionElement?> GetElementByTagName(string tag);
    Task<IEnumerable<SessionElement>> GetElementsByTagName(string tag);
    Task<SessionElement?> GetElementByXPath(string xpath);
    Task<IEnumerable<SessionElement>> GetElementsByXPath(string xpath);
    Task<SessionElement?> GetElementByClassName(string className);
    Task<IEnumerable<SessionElement>> GetElementsByClassName(string className);
} 
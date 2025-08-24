namespace Core.Session.Elements;

public interface IElementSelector {
    SessionElement? QuerySelector(string query);
    IEnumerable<SessionElement> QuerySelectorAll(string query);
    SessionElement? GetElementById(string elementId);
    SessionElement? GetElementByTagName(string tag);
    IEnumerable<SessionElement> GetElementsByTagName(string tag);
    SessionElement? GetElementByXPath(string xpath);
    IEnumerable<SessionElement> GetElementsByXPath(string xpath);
    SessionElement? GetElementByClassName(string className);
    IEnumerable<SessionElement> GetElementsByClassName(string className);
} 
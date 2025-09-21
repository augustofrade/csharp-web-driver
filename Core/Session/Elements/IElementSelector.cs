namespace Core.Session.Elements;

/// <summary>
/// DOM related operations
/// <br/><br/>
/// Has the same syntax as JavaScript with some helper methods.
/// </summary>
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
namespace Core.Session.Elements;

/// <summary>
/// Dom related operations that originate from the Document of the current browsing context.
/// <br/>
/// This means that if used in the top-level browsing context, it will fetch elements in the Document root of the page,
/// and if used in an iFrame context, fetch based on its Document root.
/// <br/><br/>
/// Has the same syntax as JavaScript with some helper methods.
/// </summary>
public class SessionElementSelector(string sessionId) : ElementSelector(sessionId, $"/session/{sessionId}");
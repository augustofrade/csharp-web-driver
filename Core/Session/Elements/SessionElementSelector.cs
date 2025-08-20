namespace Core.Session.Elements;

public class SessionElementSelector(string sessionId) : ElementSelector(sessionId, $"/session/{sessionId}");